using EldenGuide.DAL;
using EldenGuide.Models;
//using FirebaseAdmin.Auth.Hash;    //Uncomment later
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System.Diagnostics;
using System.Dynamic;

namespace EldenGuide.Controllers
{
    public class AuthController : Controller
    {
        private AuthDAL AuthContext = new AuthDAL();

        public IActionResult Home()
        {
            return View();
        }


        // GET: AuthController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewUser()
        {
            return View("~/Views/Home/NewUser.cshtml");
        }

        public ActionResult Staff()
        {
            return View("~/Views/Home/StaffLogin.cshtml");
        }

        // GET: AuthController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult SignIn(IFormCollection forData)
        {
            
            return View("Auth");

        }

        

            //User Login

            public async Task<ActionResult> LogInUser()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> LogInUser(IFormCollection form)
        {

            // Initialize your Data Access Layer
            AuthDAL userDAL = new AuthDAL();

            // Retrieve the user from the database by the email
            // Assuming you have a method like GetUserByEmail in your AuthDAL
            User user = await userDAL.GetUserByEmail(form["Email"]);

            if (user != null)
            {
                // Replace this with a hash comparison if you implement hashed passwords
                if (user.Password == form["Password"])
                {
                    //var userData = new { user.Username, user.Email };
                    //string userJson = System.Text.Json.JsonSerializer.Serialize(userData);
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return RedirectToAction("Auth","Home");

            //// Check if the user exists and the password matches
            //if (user != null && user.Password == form["Password"])
            //{
            //    // If the password matches, sign in the user
            //    // Convert user details to JSON (excluding password for security)
            //    var userData = new
            //    {
            //        user.Username,
            //        user.Email
            //    };
            //    string userJson = System.Text.Json.JsonSerializer.Serialize(userData);

            //    // Set the user session
            //    HttpContext.Session.SetString("UserSession", userJson);

            //    // Redirect to a secure page after login
            //    return RedirectToAction("Index", "Home"); 
            //}
            //else
            //{
            //    // If the user doesn't exist or the password is wrong, show an error message
            //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //    return View(); // Return the login view again for the user to try again
            //}
            //User user = new User();
            //AuthDAL userDAL = new AuthDAL();

            ///*user.Username = form["Username"];*/      //Call out the form in the Auth View page to instantiate the properties in the model object created
            //user.Email = form["Email"];
            //user.Password = form["Password"];

            //await userDAL.AddUser(user);


            //Debug.WriteLine("Debug statement: Something happened!");
            //Debug.WriteLine("Nice statement: User Login!");
            //return View();
        }

        //Staff Login
        public async Task<ActionResult> StaffLogin()
        {
            Staff staff = new Staff();
            return View(staff);
        }

        [HttpPost]
        public async Task<ActionResult> StaffLogin(IFormCollection form)
        {
            AuthDAL staffDAL = new AuthDAL();
            Staff staff = await staffDAL.GetStaffByEmail(form["Email"]);

            if (staff != null && staff.Password == form["Password"])
            {
                // Logic for successful login
                // Convert staff details to JSON (excluding password for security)
                var staffData = new
                {
                    staff.Username,
                    staff.Email
                    // Add other required details but exclude sensitive information like password
                };
                string staffJson = System.Text.Json.JsonSerializer.Serialize(staffData);

                // Set the staff session
                HttpContext.Session.SetString("StaffSession", staffJson);

                // Redirect to a secure staff page after login
                return RedirectToAction("StaffGuideList", "Guide"); 
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(); // Return to staff login view
            }

            //Staff staff = new Staff();
            //AuthDAL staffDAL = new AuthDAL();

            ///*user.Username = form["Username"];*/      //Call out the form in the Auth View page to instantiate the properties in the model object created
            //staff.Email = form["Email"];
            //staff.Password = form["Password"];

            //await staffDAL.AddStaff(staff);


            //Debug.WriteLine("Debug statement: Something happened!");
            //Debug.WriteLine("Nice statement:Staff Login!");
            //return View();
        }


        //User Sign U

        public async Task<ActionResult> AddNewUser()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> NewUser(IFormCollection form)
        {
            // Initialize your Data Access Layer
            AuthDAL userDAL = new AuthDAL();

            // Instantiate a new User object with form data
            User user = new User
            {
                Username = form["Username"], // Bind the username from the form
                Email = form["Email"], // Bind the email from the form
                Password = form["Password"] // Bind the password from the form
            };

            // Check if the model state is valid based on the validation attributes on your User model
            if (!TryValidateModel(user))
            {
                // The model is not valid, return the same view with the user object to show validation messages
                return View(user);
            }

            // Hash the password before saving to the database
            //user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // The model is valid, proceed to hash the password and save the user to Firestore
            bool addUserResult = await AuthContext.AddUser(user);

            // Check if the user was successfully added
            if (addUserResult)
            {
                // Redirect to Index Page ie. Main page
                Console.WriteLine("Success");
                return RedirectToAction("Auth","Home"); //Success
            }
            else
            {
                // If there was a problem saving the user, redirect back to current page
                Console.WriteLine("Error");
                return View("NewUser"); // Error
            }

            //User user = new User();
            //AuthDAL userDAL = new AuthDAL();

            //user.Username = form["Username"];      //Call out the form in the Auth View page to instantiate the properties in the model object created
            //user.Email = form["Email"];
            //user.Password = form["Password"];

            //await userDAL.AddUser(user);


            //Debug.WriteLine("Debug statement: Something happened!");
            //Debug.WriteLine("Nice statement: User created!");
            //return View("AddNewUser");
        }
        
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear(); // Clear user session
            return RedirectToAction("Index", "Home"); // Redirect to the login page or home page
        }

        public ActionResult StaffLogOut()
        {
            HttpContext.Session.Clear(); // Clear staff session
            return RedirectToAction("StaffLogin", "Home"); // Redirect to the staff login page or home page
        }







        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> NewUser(User user)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        // Check if the user already exists
        //        var existingUser = await AuthContext.GetUserByEmail(user.Email);
        //        if (existingUser != null)
        //        {
        //            ModelState.AddModelError("", "User already exists with the given email.");
        //            return View(user);
        //        }

        //        // Hash the password here before saving
        //        user.Password = HashPassword(user.Password);

        //        await AuthContext.AddUser(user);
        //        // Redirect to a confirmation page or login page after successful registration
        //        return RedirectToAction("Index"); // or another view as needed
        //    }

        //    // If model state is not valid, return the same view for corrections
        //    return View(user);
        //}


    }
}
