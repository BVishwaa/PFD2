using Google.Cloud.Firestore;
using EldenGuide.Models;

namespace EldenGuide.DAL
{
    public class CategoryDAL
    {
        FirestoreDb db;

        public CategoryDAL() //constructor that helps with connecting to the database
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

        public async Task<Guide> ExtractCatGuide()
        {
            // [START fs_get_all]
            CollectionReference usersRef = db.Collection("Guides");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync(); //Once connected to the database, this calls out specifally for the documents inside the Guides collection
            Guide guide = new Guide();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                guide.GuideId = document.Id;
                Dictionary<string, object> documentDictionary = document.ToDictionary();

                guide.AppName = documentDictionary["AppName"].ToString();

                guide.Content = documentDictionary["Category"].ToString();

            }
            return guide;
        }

        public async Task<List<Guide>> ExtractGuideIDByCat(string category)
        {
            // [START fs_get_all]
            CollectionReference usersRef = db.Collection("Guides");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync(); //Once connected to the database, this calls out specifally for the documents inside the Guides collection
            Guide guide = new Guide();
            List<Guide> CategoryList = new List<Guide>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
               
                
                Dictionary<string, object> documentDictionary = document.ToDictionary();

                 if(documentDictionary["Category"].ToString() == category) 
                 { 
                    guide.GuideId = document.Id;
                    guide.AppName = documentDictionary["AppName"].ToString();

                    

                    CategoryList.Add(guide);
                 }
            }
            return CategoryList;
        }

    }
}
