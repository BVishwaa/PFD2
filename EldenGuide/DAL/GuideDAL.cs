using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using EldenGuide.Models;
using Google.Cloud.Firestore.V1;
using Google.Cloud.Storage.V1;
using Grpc.Auth;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;

namespace EldenGuide.DAL
{
    public class GuideDAL
    {
        
        FirestoreDb db;
        public GuideDAL() //constructor that helps with connecting to the database
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

            public async Task<Guide> ExtractGuide()
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
                    
                    guide.Content = documentDictionary["Content"].ToString();
                
                }
                return guide;
            }

            public async Task<Guide> ExtractGuideID(string gid)
            {
            // [START fs_get_all]
            CollectionReference usersRef = db.Collection("Guides");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync(); //Once connected to the database, this calls out specifally for the documents inside the Guides collection
            Guide guide = new Guide();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if(document.Id == gid)
                {
                    guide.GuideId = document.Id;
                    Dictionary<string, object> documentDictionary = document.ToDictionary();

                    guide.AppName = documentDictionary["AppName"].ToString();
                    //guide.Category = documentDictionary["Category"].ToString();

                    guide.Content = documentDictionary["Content"].ToString();
                }
            }
            return guide;
            }

            public async Task<Boolean> AddGuide(Guide guide)
            {
                DocumentReference docRef = db.Collection("Guides").Document(guide.AppName);

                Dictionary<string, object> NewGuide = new Dictionary<string, object>
                {
                    {"AppName",guide.AppName},
                    {"Category",guide.Category },
                    {"Content", guide.Content },
                    {"AppLogo",guide.AppLogo }

                };

                await docRef.SetAsync(NewGuide);


                return true;
                
            }

       
    }
}
