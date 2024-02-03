using EldenGuide.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EldenGuide.Models;
using System.Data.Common;
using Microsoft.AspNetCore.Html;
using System.Diagnostics;
using Google.Cloud.Firestore;
using System.Dynamic;
using Google.Api;

namespace EldenGuide.Controllers
{
    public class ThreadController : Controller
    {
        private ThreadDAL threadContext = new ThreadDAL();
        private EventDAL eventContext = new EventDAL();
        // GET: ThreadController
        public IActionResult Thread()
        {
            return View();
        }
        
        // GET: ThreadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ThreadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThreadController/Create
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

        // GET: ThreadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ThreadController/Edit/5
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

        // GET: ThreadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ThreadController/Delete/5
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

        public async Task<ActionResult> Index(string? category)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Threads = await threadContext.GetThreads();
            mymodel.Event = await eventContext.GetEvents();
            ViewData["totalevents"] = await eventContext.GetTotal();
            //mymodel.FilteredThreads = await threadContext.FilterThreads(category);
            mymodel.FilteredThreads = await threadContext.GetBankingThreads();
            Console.WriteLine("Controller get threads");
            return View(mymodel);

        }

        public async Task<ActionResult> WriteNewThread()
        {
            if (HttpContext.Session.GetString("userEmail") != null || HttpContext.Session.GetString("staffEmail") != null)
            {
                Threads thread = new Threads();
                return View(thread);
            }
            else
            {
                return RedirectToAction("Auth", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> NewThread(IFormCollection form)
        {
            Threads thread = new Threads();
            ThreadDAL threadDAL = new ThreadDAL();

            thread.Username = Convert.ToString(TempData["Username"]);
            thread.Category = form["Category"];      //Call out the form in the WriteNewGuide View page to instantiate the properties in the model object created
            thread.Description = form["Desc"];
            thread.Title = form["Title"];

            await threadDAL.AddThread(thread);


            Debug.WriteLine("Debug statement: Something happened!");
            return View("WriteNewThread");
        }

        public async Task<ActionResult> GetFilteredThreads(string category)
        {
            var threads = await threadContext.FilterThreads(category);
            Console.WriteLine("Filtered");
            return PartialView("_ViewThreads", threads); // Assuming "_ThreadsPartial" is the name of your partial view
        }

        [HttpGet]
        public async Task<ActionResult> BankingThreads()
        {
            var bankingThreads = await threadContext.GetBankingThreads();
            return Json(bankingThreads);
        }

    }
}
