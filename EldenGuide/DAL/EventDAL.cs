using EldenGuide.Models;
using Firebase.Storage;
using Google.Cloud.Firestore;

namespace EldenGuide.DAL
{
    public class EventDAL
    {
        FirestoreDb db;
        public EventDAL() //constructor that helps with connecting to the database
        {
            string jsonPath = "./DAL/Config/elden-guide-firebase-adminsdk-r87mn-80a4f4580d.json";
            string projectId = "elden-guide";
            using StreamReader r = new StreamReader(jsonPath);
            string json = r.ReadToEnd();


            db = new FirestoreDbBuilder
            {
                ProjectId = projectId,
                JsonCredentials = json
            }.Build();
        }
        public async Task<bool> InsertEvent(Event events)
        {
            //Reference to collection
            CollectionReference collectionReference = db.Collection("Events");

            // Get a snapshot of the documents in the collection
            QuerySnapshot querySnapshot = await collectionReference.GetSnapshotAsync();

            // Count the number of documents
            int numberOfDocuments = querySnapshot.Documents.Count;
            //Console.WriteLine($"Number of documents in Threads: {numberOfDocuments}");
            try
            {
                DocumentReference docRef = db.Collection("Events").Document(Convert.ToString(numberOfDocuments + 1)); // Firestore generates ID

                Dictionary<string, object> newEvent = new Dictionary<string, object>
                {
                    {"EventID" , numberOfDocuments + 1 },
                    {"EventName", events.EventName},
                    {"Details", events.Details },
                    {"EventPhoto",  events.EventPhoto }
                };

                await docRef.SetAsync(newEvent);

                Console.WriteLine("Event successfully added to Firestore.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding event to Firestore: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Event>> GetEvents()
        {
            int id = 1;
            List<Event> eventList = new List<Event>();
            

            while (true)
            {
                DocumentReference docRef = db.Collection("Events").Document(Convert.ToString(id));
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();

                if (documentSnapshot.Exists)
                {
                    Event data = documentSnapshot.ConvertTo<Event>();

                    eventList.Add(new Event
                    {
                        EventID = data.EventID,
                        EventName = data.EventName,
                        Details = data.Details,
                        EventPhoto = Path.Combine("~/images/EventImg/", data.EventPhoto)
                    });
                }
                else
                {
                    // Exit the loop if the document with the current ID doesn't exist
                    break;
                }

                // Increment the ID for the next iteration
                id++;
                Console.WriteLine("done");
            }

            return eventList;
        }
        public async Task<int> GetTotal()
        {
            CollectionReference collectionRef = db.Collection("Events");
            QuerySnapshot snapshot = await collectionRef.GetSnapshotAsync();
            int total = snapshot.Documents.Count;
            return total;
        }

        public async Task<Event> ExtractEventByID(string eid)
        {
            CollectionReference eventRef = db.Collection("Events");
            QuerySnapshot snapshot = await eventRef.GetSnapshotAsync(); //Once connected to the database, this calls out specifally for the documents inside the Events collection
            Event @event = new Event();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Id == eid)
                {
                    int eventId;
                    if (int.TryParse(document.Id, out eventId))
                    {
                        @event.EventID = eventId;

                    }

                    Dictionary<string, dynamic> documentDictionary = document.ToDictionary();

                    @event.EventName = documentDictionary["EventName"].ToString();
                    @event.Details = documentDictionary["Details"].ToString();
                    @event.EventPhoto = documentDictionary["EventPhoto"].ToString();

                }

                /*if (document.Id == gid)
                {
                    guide.GuideId = document.Id;
                    guide.TOC = document.GetValue<string[]>("TOC");
                }*/
            }
            return @event;
        }
        public async Task<Boolean> DeleteEvent(string EventID)
        {
            DocumentReference docRef = db.Collection("Events").Document(EventID);

            await docRef.DeleteAsync();

            return true;
        }


    }
}
