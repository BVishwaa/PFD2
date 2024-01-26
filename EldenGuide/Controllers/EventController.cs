using EldenGuide.DAL;
using EldenGuide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EldenGuide.Controllers
{
    public class EventController : Controller
    {
        private EventDAL eventContext = new EventDAL();
        public IActionResult Event()
        {
            return View();
        }
        // GET: EventController

        // GET: EventController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventController/Create
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

        // GET: EventController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventController/Edit/5
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

        // GET: EventController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventController/Delete/5
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
            List<Event> eventList = await eventContext.GetEvents();
            Console.WriteLine("Controller get events");
            return View(eventList);

        }

        //writenewthread
        public async Task<ActionResult> AddEvent()
        {
            Event events = new Event();
            return View(events);
        }

        //newthread
        [HttpPost]
        public async Task<ActionResult> NewEvent(IFormCollection form)
        {
            Event events = new Event();
            EventDAL eventDAL = new EventDAL();

            events.EventName = form["Name"];      //Call out the form in the WriteNewGuide View page to instantiate the properties in the model object created
            events.Details = form["Details"];
            events.EventPhoto = form["Photo"];

            await eventDAL.InsertEvent(events);


            Console.WriteLine("event added");
            return View();
        }
    }
}
