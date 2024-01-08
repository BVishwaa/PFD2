using EldenGuide.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EldenGuide.Models;

namespace EldenGuide.Controllers
{
    public class GuideController : Controller
    {
        // GET: GuideController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GuideController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GuideController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GuideController/Create
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

        // GET: GuideController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GuideController/Edit/5
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

        // GET: GuideController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GuideController/Delete/5
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
        
        public async Task<ActionResult> GuideTemplate(string guideId)
        {
            GuideDAL guideDAL = new GuideDAL();
            Guide GuideTemplate = new Guide();
            GuideTemplate = await guideDAL.ExtractGuideID(guideId);
            return View(GuideTemplate);
        }

        public async Task<ActionResult> POSB_Digibank()
        {
            GuideDAL guideDAL = new GuideDAL();
            Guide POSB_Digibank = new Guide();
            POSB_Digibank = await guideDAL.ExtractGuide();
            return View(POSB_Digibank);
        }

        public ActionResult OCBC_Digital()
        {
            return View();
        }

        public ActionResult DBS_PayLah()
        {
            return View();
        }

        public ActionResult Health_Hub()
        {
            return View();
        }

        public ActionResult Healthy_365()
        {
            return View();
        }

        public ActionResult Health_Buddy()
        {
            return View();
        }

        public ActionResult WhatsApp()
        {
            return View();
        }

        public ActionResult Telegram()
        {
            return View();
        }

        public ActionResult WeChat()
        {
            return View();
        }

        public ActionResult GrabFood()
        {
            return View();
        }

        public ActionResult Food_Panda()
        {
            return View();
        }

        public ActionResult Deliveroo()
        {
            return View();
        }

        public ActionResult GrabTransport()
        {
            return View();
        }

        public ActionResult GoJek()
        {
            return View();
        }

        public ActionResult Singabus()
        {
            return View();
        }
    }
}
