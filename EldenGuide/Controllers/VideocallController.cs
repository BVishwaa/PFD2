using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
