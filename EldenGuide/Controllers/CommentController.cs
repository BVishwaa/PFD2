using EldenGuide.DAL;
using EldenGuide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EldenGuide.Controllers
{
    public class CommentController : Controller
    {
        private CommentDAL commentContext = new CommentDAL();
        private ThreadDAL threadContext  = new ThreadDAL();

        public IActionResult Comment()
        {
            return View();
        }

        // GET: CommentController
        public async Task<ActionResult> Index(int threadId)
        {
            TempData["ThreadID"] = threadId;

            Console.WriteLine(threadId);
            ViewBag.ThreadId = Convert.ToInt32(threadId);
            Console.WriteLine(ViewBag.ThreadId);
            //CommentViewModel mymodel = new CommentViewModel();
            var viewModel = new CommentViewModel
            {
                Threads = await threadContext.GetThreads(), // Make sure this method returns a non-null collection
                Comments = await commentContext.GetComments(threadId)
            };
            viewModel.Comments = await commentContext.GetComments(threadId);
            viewModel.SingleComment = new Comment();
            return View(viewModel);
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentController/Create
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

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
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

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentController/Delete/5
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

        public async Task<ActionResult> AddComment()
        {
            Comment comments = new Comment();
            return View(comments);
        }

        [HttpPost]
        public async Task<ActionResult> NewComment(IFormCollection form)
        {
            Comment comments = new Comment();
            CommentDAL commentDAL = new CommentDAL();

            comments.ThreadID = Convert.ToInt32(TempData["ThreadID"]);

            //comments.ThreadID = Convert.ToInt32(ViewBag.ThreadId);

            comments.CommentText = form["CommentText"];
            //comments.ThreadID = Convert.ToInt32(ViewBag.threadid);

            // Retrieve threadId from form
            //int threadId = Convert.ToInt32(ViewBag.ThreadId);
            int threadId = Convert.ToInt32(TempData["ThreadID"]);
            Console.WriteLine(TempData["ThreadID"]);

            await commentDAL.InsertComment(comments);

            // Fetch the updated list of comments
            var updatedComments = await commentDAL.GetComments(threadId);

            // Construct the CommentViewModel
            CommentViewModel viewModel = new CommentViewModel
            {
                Comments = updatedComments,
                SingleComment = comments // or null, depending on your use case
            };

            Console.WriteLine("comment added");
            return View("Index", viewModel);
        }


    }
}
