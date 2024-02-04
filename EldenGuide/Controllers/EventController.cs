using EldenGuide.DAL;
using EldenGuide.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Firebase.Storage;
using Google.Cloud.Firestore.V1;
using System.Dynamic;

namespace EldenGuide.Controllers
{
    public class EventController : Controller
    {
        private EventDAL eventContext = new EventDAL();
        private VideocallDAL videocallContext = new VideocallDAL();

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

        public async Task<ActionResult> Index(int eventId)
        {
            TempData["EventID"] = eventId;
            TempData.Keep("EventID");
            dynamic mymodel = new ExpandoObject();
            mymodel.Videocall = await videocallContext.GetURLs();
            mymodel.Event = await eventContext.GetEvents();
            //List<Event> eventList = await eventContext.GetEvents();
            Console.WriteLine("Controller get events");
            return View(mymodel);

        }

        public async Task<ActionResult> DetailedIndex(int eventId)
        {
            TempData["EventID"] = eventId;
            List<Event> eventList = await eventContext.GetEvents();
            Console.WriteLine(TempData["EventID"]);
            return View(eventList);
        }

        //writenewthread
        public async Task<ActionResult> AddEvent()
        {
            if (HttpContext.Session.GetString("staffEmail") != null)
            {
                Event events = new Event();
                return View(events);
            }
            else
            {
                return RedirectToAction("StaffLogin", "Home");
            }
        }

        //newthread
        [HttpPost]
        public async Task<ActionResult> NewEvent(IFormCollection form, IFormFile Photo)
        {
            Event events = new Event();
            EventDAL eventDAL = new EventDAL();


            events.EventName = form["Name"];
            events.Details = form["Details"];
            //events.EventPhoto = Photo.FileName;

            events.EventPhoto = Photo.FileName;
            await saveImage(Photo);

            await eventDAL.InsertEvent(events);

            await saveImage(Photo);

            Console.WriteLine("Event added");
            return View("AddEvent"); 
        }


        public async Task<Boolean> saveImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/EventImg/", image.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

            }
            return true;
        }

        [HttpPost]
        public async Task<ActionResult> DeleteEvent(IFormCollection form)
        {
            EventDAL eventDAL = new EventDAL();
            Event EventToDelete = new Event();

            int eventId;
            if (int.TryParse(form["hId"], out eventId))
            {
                EventToDelete.EventID = eventId;
                await eventDAL.DeleteEvent(EventToDelete.EventID.ToString());
                return RedirectToAction("Index", "Event");
            }
            else
            {
                // Handle invalid ID here (e.g., return an error message)
                Console.WriteLine(form["hId"]);
                return BadRequest("Invalid event ID");
                
            }
        }
    }
}
