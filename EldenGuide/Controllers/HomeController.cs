using EldenGuide.DAL;
using EldenGuide.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EldenGuide.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Guide> guideList = new List<Guide>();
            CategoryDAL categoryDAL = new CategoryDAL();
            guideList = await categoryDAL.OrganizeAllGuides();
            return View(guideList);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Forum()
        {
            return View();
        }

        public IActionResult Auth()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

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
                /*var staffData = new
                {
                    staff.Username,
                    staff.Email
                    // Add other required details but exclude sensitive information like password
                };
                string staffJson = System.Text.Json.JsonSerializer.Serialize(staffData);

                // Set the staff session
                HttpContext.Session.SetString("StaffSession", staffJson);*/

                HttpContext.Session.SetString("staffUsername", staff.Username);
                HttpContext.Session.SetString("staffEmail", staff.Email);


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
    }
}