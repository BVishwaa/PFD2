using EldenGuide.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EldenGuide.Models;
using System.Data.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.CodeAnalysis.Differencing;

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
        public async Task<ActionResult> WriteNewGuide(Guide guide, IFormCollection form, IFormFile AppLogo)
        { 
            string StoreTextbox = form["tb"];
            guide.TOC = StoreTextbox.Split(",");

            guide.AppLogo = AppLogo.FileName;

            GuideDAL guideDAL = new GuideDAL();
            await guideDAL.AddGuide(guide);
            
            await saveImage(AppLogo);

            return RedirectToAction("StaffGuideList", "Guide");
        }

        public async Task<ActionResult> EditGuide(string guideId)
        {
            GuideDAL guideDAL = new GuideDAL();
            Guide GuideToEdit = new Guide();
            GuideToEdit = await guideDAL.ExtractGuideID(guideId);

            return View(GuideToEdit);
        }

        [HttpPost]
        public async Task<ActionResult> EditGuide(Guide guide, IFormCollection form, IFormFile AppLogo)
        {
            string StoreTextbox = form["tb"];
            guide.TOC = StoreTextbox.Split(",");

            guide.AppLogo = AppLogo.FileName;

            GuideDAL guideDAL = new GuideDAL();
            await guideDAL.EditGuide(guide);

            await saveImage(AppLogo);
            return RedirectToAction("StaffGuideList", "Guide");
        }

        public async Task<Boolean> saveImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/For-Logos/", image.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                
            }
            return true;
        }
    }
}
