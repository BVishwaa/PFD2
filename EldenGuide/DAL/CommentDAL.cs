using EldenGuide.Models;
using Google.Cloud.Firestore;

namespace EldenGuide.DAL
{
    public class CommentDAL
    {
        FirestoreDb db;
        public CommentDAL() //constructor that helps with connecting to the database
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

        public async Task<bool> InsertComment(Comment comments)
        {
            //Reference to collection
            CollectionReference collectionReference = db.Collection("Comments");

            // Get a snapshot of the documents in the collection
            QuerySnapshot querySnapshot = await collectionReference.GetSnapshotAsync();

            // Count the number of documents
            int numberOfDocuments = querySnapshot.Documents.Count;
            //Console.WriteLine($"Number of documents in Threads: {numberOfDocuments}");

            try
            {
                DocumentReference docRef = db.Collection("Comments").Document(Convert.ToString(numberOfDocuments + 1));

                Dictionary<string, object> newComment = new Dictionary<string, object>
                {
                    {"ThreadID", comments.ThreadID },
                    {"CommentText", comments.CommentText},
                    {"DateCommented", Convert.ToString(DateTime.Now) },
                    {"Username", "nagi" }
                };

                await docRef.SetAsync(newComment);

                Console.WriteLine("Comment successfully added to Firestore.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding thread to Firestore: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Comment>> GetComments(int threadId)
        {
            int id = 1;
            List<Comment> commentList = new List<Comment>();

            while (true)
            {
                DocumentReference docRef = db.Collection("Comments").Document(Convert.ToString(id));
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();

                if (documentSnapshot.Exists)
                {
                    Comment data = documentSnapshot.ConvertTo<Comment>();
                    if(data.ThreadID == threadId)
                    {
                        commentList.Add(new Comment
                        {
                            CommentText = data.CommentText,
                            DateCommented = data.DateCommented,
                            Username = data.Username,
                        });
                    }
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

            return commentList;
        }
    }
}
