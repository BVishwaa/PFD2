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
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace EldenGuide.DAL
{
    public class AuthDAL
    {

        FirestoreDb db;
        public AuthDAL() //constructor that helps with connecting to the database
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
        public async Task<bool> AddUser(User user)
        {
            //Reference to collection
            CollectionReference collectionReference = db.Collection("users");

            // Get a snapshot of the documents in the collection
            QuerySnapshot querySnapshot = await collectionReference.GetSnapshotAsync();

            // Count the number of documents
            int numberOfDocuments = querySnapshot.Documents.Count;
            Console.WriteLine($"Number of documents in users: {numberOfDocuments}");

            try
            {
                DocumentReference docRef = db.Collection("users").Document(Convert.ToString(numberOfDocuments + 1));

                Dictionary<string, object> NewUser = new Dictionary<string, object>
                {
                    {"Username", user.Username},
                    {"Email", user.Email},
                    {"Password", user.Password }
                };

                await docRef.SetAsync(NewUser);

                Console.WriteLine("User successfully added to Firestore.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding User to Firestore: {ex.Message}");
                return false;
            }
        }
        public async Task<List<User>> GetUser()
        {
            int id = 1;
            List<User> userList = new List<User>();

            while (true)
            {
                DocumentReference docRef = db.Collection("users").Document(Convert.ToString(id));
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();

                if (documentSnapshot.Exists)
                {
                    User data = documentSnapshot.ConvertTo<User>();

                    userList.Add(new User
                    {
                        Username = data.Username,
                        Email = data.Email,
                        Password = data.Password

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

            return userList;
        }

        //public async Task<bool> CheckEmailExists(string email)
        //{
        //    CollectionReference usersCollection = db.Collection("users");
        //    Query query = usersCollection.WhereEqualTo("Email", email);
        //    QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

        //    return querySnapshot.Documents.Count > 0; // Returns true if the email exists
        //}
    }


    
}
