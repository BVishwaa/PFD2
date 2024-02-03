using Microsoft.AspNetCore.Mvc;

namespace EldenGuide.Controllers
{
    public class ChatRoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
