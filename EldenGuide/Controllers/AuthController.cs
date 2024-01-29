using EldenGuide.DAL;
using EldenGuide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public async Task<ActionResult> LogInUser()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> LogInUser(IFormCollection form)
        {
            User user = new User();
            AuthDAL userDAL = new AuthDAL();

            /*user.Username = form["Username"];*/      //Call out the form in the Auth View page to instantiate the properties in the model object created
            user.Email = form["Email"];
            user.Password = form["Password"];

            await userDAL.AddUser(user);


            Debug.WriteLine("Debug statement: Something happened!");
            Debug.WriteLine("Nice statement: Something happened!");
            return View();
        }

        public async Task<ActionResult> AddNewUser()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> NewUser(IFormCollection form)
        {
            User user = new User();
            AuthDAL userDAL = new AuthDAL();

            user.Username = form["Username"];      //Call out the form in the Auth View page to instantiate the properties in the model object created
            user.Email = form["Email"];
            user.Password = form["Password"];

            await userDAL.AddUser(user);


            Debug.WriteLine("Debug statement: Something happened!");
            return View("AddNewUser");
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
