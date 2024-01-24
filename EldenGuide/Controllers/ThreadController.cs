using EldenGuide.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EldenGuide.Models;
using System.Data.Common;
using Microsoft.AspNetCore.Html;
using System.Diagnostics;
using Google.Cloud.Firestore;

namespace EldenGuide.Controllers
{
    public class ThreadController : Controller
    {
        private ThreadDAL threadContext = new ThreadDAL();
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

        public async Task<ActionResult> Index()
        {
            List<Threads> threadList = await threadContext.GetThreads();
            Console.WriteLine("Controller get threads");
            return View(threadList);

        }

        public async Task<ActionResult> WriteNewThread()
        {
            Threads thread = new Threads();
            return View(thread);
        }

        [HttpPost]
        public async Task<ActionResult> NewThread(IFormCollection form)
        {
            Threads thread = new Threads();
            ThreadDAL threadDAL = new ThreadDAL();

            thread.Category = form["Category"];      //Call out the form in the WriteNewGuide View page to instantiate the properties in the model object created
            thread.Description = form["Desc"];
            thread.Title = form["Title"];

            await threadDAL.AddThread(thread);


            Debug.WriteLine("Debug statement: Something happened!");
            return View("WriteNewThread");
        }

    }
}
