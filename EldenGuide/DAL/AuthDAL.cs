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
//using FirebaseAdmin.Auth.Hash;   //uncomment later
//using Firebase.Auth;

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

        //Get User Email
        public async Task<User> GetUserByEmail(string email)
        {
            CollectionReference usersRef = db.Collection("users");

            // Create a query against the collection.
            Query query = usersRef.WhereEqualTo("Email", email);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                // Assuming email is unique, there should only be one matching document.
                DocumentSnapshot documentSnapshot = querySnapshot.Documents[0];
                if (documentSnapshot.Exists)
                {
                    User user = documentSnapshot.ConvertTo<User>();
                    return user;
                }
            }

            // Return null if no user is found
            return null;
        }

        //Get Staff Email
        public async Task<Staff> GetStaffByEmail(string email)
        {
            CollectionReference staffRef = db.Collection("Staff");

            // Create a query against the collection.
            Query query = staffRef.WhereEqualTo("Email", email);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count > 0)
            {
                // Assuming email is unique, there should only be one matching document.
                DocumentSnapshot documentSnapshot = querySnapshot.Documents[0];
                if (documentSnapshot.Exists)
                {
                    Staff staff = documentSnapshot.ConvertTo<Staff>();
                    return staff;
                }
            }

            // Return null if no user is found
            return null;
        }

        public async Task<bool> AddUser(User user)
        {
            // Hash the password before saving to the database
            //user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
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
        public async Task<bool> AddStaff(Staff staff)
        {
            //Reference to a collection
            CollectionReference collectionReference = db.Collection("Staff");

            // Get a snapshot of the documents in the collection
            QuerySnapshot querySnapshot = await collectionReference.GetSnapshotAsync();

            // Count the number of Staff
            int numberOfStaff = querySnapshot.Documents.Count;
            Console.WriteLine($"Number of documents in users: {numberOfStaff}");

            try
            {
                DocumentReference docRef = db.Collection("Staff").Document(Convert.ToString(numberOfStaff + 1));

                Dictionary<string, object> NewStaff = new Dictionary<string, object>
                {
                    {"Username", staff.Username},
                    {"Email", staff.Email},
                    {"Password", staff.Password }
                };

                await docRef.SetAsync(NewStaff);

                Console.WriteLine("Staff successfully added to Firestore.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Staff to Firestore: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Staff>> GetStaff()
        {
            int id = 1;
            List<Staff> userList = new List<Staff>();

            while (true)
            {
                DocumentReference docRef = db.Collection("Staff").Document(Convert.ToString(id));
                DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();

                if (documentSnapshot.Exists)
                {
                    Staff data = documentSnapshot.ConvertTo<Staff>();

                    userList.Add(new Staff
                    {
                        Username = data.Username,
                        Email = data.Email,
                        Password = data.Password

                    });
                }
                else
                {
                    // Exit the loop if the document with the current ID doesn't exist
                    Console.WriteLine("Error");
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
