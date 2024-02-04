using EldenGuide.Models;
using Google.Cloud.Firestore;

namespace EldenGuide.DAL
{
    public class VideocallDAL
    {
        FirestoreDb db;
        public VideocallDAL() //constructor that helps with connecting to the database
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
        public async Task<bool> InsertURL(Videocall vc)
        {
            //Reference to collection
            CollectionReference collectionReference = db.Collection("MeetingURL");

            // Get a snapshot of the documents in the collection
            QuerySnapshot querySnapshot = await collectionReference.GetSnapshotAsync();

            // Count the number of documents
            int numberOfDocuments = querySnapshot.Documents.Count;
            //Console.WriteLine($"Number of documents in Threads: {numberOfDocuments}");
            try
            {
                DocumentReference docRef = db.Collection("MeetingURL").Document(Convert.ToString(numberOfDocuments + 1)); // Firestore generates ID

                Dictionary<string, object> newVc = new Dictionary<string, object>
                {
                    {"URL", vc.URL},
                    {"Title", vc.Title },
                    {"DateCreated", Convert.ToString(DateTime.Now) }
                };

                await docRef.SetAsync(newVc);

                Console.WriteLine("Event successfully added to Firestore.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding event to Firestore: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Videocall>> GetURLs()
        {
            int id = 1;
            List<Videocall> vcList = new List<Videocall>();


            while (true)
            {
                DocumentReference docRef = db.Collection("MeetingURL").Document(Convert.ToString(id));
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();

                if (documentSnapshot.Exists)
                {
                    Videocall data = documentSnapshot.ConvertTo<Videocall>();

                    vcList.Add(new Videocall
                    {
                        URL = data.URL,
                        Title = data.Title,
                        DateCreated = data.DateCreated
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

            return vcList;
        }
    }
}
