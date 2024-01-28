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
using EldenGuide.Controllers;
using System.Collections;
using System.ComponentModel;
using Google.Api;
using Microsoft.AspNetCore.Mvc;

namespace EldenGuide.DAL
{
    public class ThreadDAL
    {
        FirestoreDb db;
        public ThreadDAL() //constructor that helps with connecting to the database
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
        public async Task<bool> AddThread(Threads thread)
        {
            //Reference to collection
            CollectionReference collectionReference = db.Collection("Threads");

            // Get a snapshot of the documents in the collection
            QuerySnapshot querySnapshot = await collectionReference.GetSnapshotAsync();

            // Count the number of documents
            int numberOfDocuments = querySnapshot.Documents.Count;
            //Console.WriteLine($"Number of documents in Threads: {numberOfDocuments}");

            try
            {
                DocumentReference docRef = db.Collection("Threads").Document(Convert.ToString(numberOfDocuments + 1));

                Dictionary<string, object> NewThread = new Dictionary<string, object>
                {
                    {"ThreadID", numberOfDocuments + 1 },
                    {"Title", thread.Title},
                    {"Category", thread.Category },
                    {"Description", thread.Description },
                    {"Username", "guest" },
                    {"DatePosted", "19/01/2024" }
                };

                await docRef.SetAsync(NewThread);

                Console.WriteLine("Thread successfully added to Firestore.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding thread to Firestore: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Threads>> GetThreads()
        {
            int id = 1;
            List<Threads> threadList = new List<Threads>();

            while (true)
            {
                DocumentReference docRef = db.Collection("Threads").Document(Convert.ToString(id));
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();

                if (documentSnapshot.Exists)
                {
                    Threads data = documentSnapshot.ConvertTo<Threads>();

                    threadList.Add(new Threads
                    {
                        ThreadID = data.ThreadID,
                        Title = data.Title,
                        Category = data.Category,
                        Description = data.Description,
                        Username = data.Username,
                        DatePosted = data.DatePosted
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

            return threadList;
        }


        public async Task<List<Threads>> FilterThreads(string? category)
        {
            try
            {
                List<Threads> filterList = new List<Threads>();

                Query query;
                if (string.IsNullOrEmpty(category))
                {
                    // If no category is specified, get all threads
                    query = db.Collection("Threads");
                }
                else
                {
                    // If a specific category is specified, filter by that category
                    query = db.Collection("Threads").WhereEqualTo("Category", category);
                }

                // Execute the query
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Threads data = documentSnapshot.ConvertTo<Threads>();
                        filterList.Add(new Threads
                        {
                            ThreadID = data.ThreadID,
                            Title = data.Title,
                            Category = data.Category,
                            Description = data.Description,
                            Username = data.Username,
                            DatePosted = data.DatePosted
                        });
                    }
                }

                return filterList;
            }
            catch (Exception ex)
            {
                // Log the exception
                // Handle the error appropriately
                throw; // or return a default value
            }

        }

        public async Task<List<Threads>> GetBankingThreads()
        {
            List<Threads> bankingThreads = new List<Threads>();
            Query query = db.Collection("Threads").WhereEqualTo("Category", "Banking");
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Threads thread = documentSnapshot.ConvertTo<Threads>();
                    bankingThreads.Add(thread);
                }
            }
            return bankingThreads;
        }

        /*
        
        public async List<Threads> GetThreads()
        {
            int id = 1;
            List<Thread> threadList = new List<Thread>();
            Query allThreadsQuery = db.Collection("Threads");
            QuerySnapshot querySnapshot = await allThreadsQuery.GetSnapshotAsync();
            DocumentReference docRef = db.Collection("Threads").Document(Convert.ToString(id));
            DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();
            foreach (documentSnapshot in querySnapshot.Documents)
            {
                if(documentSnapshot.Exists)
                {
                    Threads data = documentSnapshot.ConvertTo<Threads>();
                    //IDictionary<string, object> data = documentSnapshot.ToDictionary();
                    threadList.Add(
                        new Threads
                        {
                            ThreadID = data.ThreadID,
                            Title = data.Title,
                            Category = data.Category,
                            Description = data.Description,
                            Username = data.Username,
                            DatePosted = data.DatePosted
                        });
                }
                //loop till snapshot of this id can't be found
                id += 1;    
                Console.WriteLine("done");
            }
            return threadList;
        }*/

    }
}
