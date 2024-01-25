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
    public class ForumDAL
    {
        FirestoreDb db;
        public ForumDAL() //constructor that helps with connecting to the database
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

        public async Task<Threads> ExtractThreads()
        {
            // [START fs_get_all]
            CollectionReference usersRef = db.Collection("Threads");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync(); //Once connected to the database, this calls out specifally for the documents inside the Guides collection
            Threads thread = new Threads();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                thread.ThreadId = document.Id;
                Dictionary<string, object> documentDictionary = document.ToDictionary();

                thread.Title = documentDictionary["Title"].ToString();

                thread.Username = "guest";

                thread.Category = documentDictionary["Category"].ToString();

                thread.Description = documentDictionary["Description"].ToString();

                thread.DatePosted = DateTime.Now;

            }
            return thread;
        }

        public async Task<Threads> ExtractThreadID(string threadid)
        {
            // [START fs_get_all]
            CollectionReference usersRef = db.Collection("Threads");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync(); //Once connected to the database, this calls out specifally for the documents inside the Guides collection
            Threads thread = new Threads();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Id == threadid)
                {
                    thread.ThreadId = document.Id;
                    Dictionary<string, object> documentDictionary = document.ToDictionary();

                    thread.Title = documentDictionary["Title"].ToString();

                    thread.Username = "guest";

                    thread.Category = documentDictionary["Category"].ToString();

                    thread.Description = documentDictionary["Description"].ToString();

                    thread.DatePosted = DateTime.Now;
                }
            }
            return thread;
        }


    }
}