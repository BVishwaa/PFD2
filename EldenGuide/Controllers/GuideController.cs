using EldenGuide.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EldenGuide.Models;
using System.Data.Common;
using Microsoft.AspNetCore.Html;

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

        

        public async Task<ActionResult> StaffGuideList(string cat)
        {
            //List<string> categories = new List<string>();
            
            CategoryDAL catDAL = new CategoryDAL();
            List<Guide> output = new List<Guide>();
            if (cat == "")
            {

            }
            else
            {
                output = await catDAL.ExtractGuideIDByCat(cat);   //Extracts all the guides of the particular category parameter
            }
            


            return View(output);
        }

        public async Task<ActionResult> WriteNewGuide()
        {
            Guide guide = new Guide();
            return View(guide);
        }


        [HttpPost]
        public async Task<ActionResult> NewGuide(IFormCollection form)
        {
            Guide guide = new Guide();

            guide.Category = form["Category"];      //Call out the form in the WriteNewGuide View page to instantiate the properties in the model object created
            guide.AppName = form["AppName"];
            guide.AppLogo = form["AppLogo"];
            guide.Content = Convert.ToString(form["Content"]);
            
            GuideDAL guideDAL = new GuideDAL();
            guideDAL.AddGuide(guide);

            return View();
        }
    }
}
