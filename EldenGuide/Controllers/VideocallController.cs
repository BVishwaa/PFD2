using EldenGuide.DAL;
using EldenGuide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading;

namespace EldenGuide.Controllers
{
    public class VideocallController : Controller
    {
        // GET: VideocallController
        public IActionResult VideoCall()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: VideocallController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VideocallController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VideocallController/Create
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

        // GET: VideocallController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VideocallController/Edit/5
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

        // GET: VideocallController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VideocallController/Delete/5
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

        public async Task<ActionResult> AddURL()
        {
            Videocall vc = new Videocall();
            return View(vc);
        }

        [HttpPost]
        public async Task<ActionResult> NewURL(IFormCollection form)
        {
            Videocall vc = new Videocall();
            VideocallDAL videocallDAL = new VideocallDAL();

            vc.Title = form["Title"];
            vc.URL = form["URL"];      //Call out the form in the WriteNewGuide View page to instantiate the properties in the model object created
            vc.DateCreated = Convert.ToString(DateTime.Now);

            await videocallDAL.InsertURL(vc);


            Debug.WriteLine("Debug statement: Something happened!");
            return View("Index","Event");
        }
    }
}
