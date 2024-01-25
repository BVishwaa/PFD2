using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EldenGuide.DAL;
using EldenGuide.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldenGuide.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult CreatePost()
        {
            return View();
        }
        // GET: ForumController
        public ActionResult Index()
        {
			return View();
		}

        // GET: ForumController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ForumController/Create
        public ActionResult Create()
        {
            ViewData["CategoryList"] = GetCategory();

			return View();
        }

		private List<SelectListItem> GetCategory()
		{
			List<SelectListItem> category = new List<SelectListItem>();
			category.Add(new SelectListItem
			{
				Value = "Banking",
				Text = "Banking"
			});
			category.Add(new SelectListItem
			{
				Value = "Health",
				Text = "Health"
			});
			category.Add(new SelectListItem
			{
				Value = "Telecommunication",
				Text = "Telecommunication"
			});
			category.Add(new SelectListItem
			{
				Value = "Food Delivery",
				Text = "Food Delivery"
			});
			category.Add(new SelectListItem
			{
				Value = "Transport",
				Text = "Transport"
			});

			return category;
		}

		// POST: ForumController/Create
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

        // GET: ForumController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ForumController/Edit/5
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

        // GET: ForumController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ForumController/Delete/5
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
        public async Task<ActionResult> InsertThread(string threadId)
        {
            ForumDAL forumDAL = new ForumDAL();
            Threads thread = new Threads();
            thread = await forumDAL.ExtractThreadID(threadId);
            return View(thread);
        }
    }
}
