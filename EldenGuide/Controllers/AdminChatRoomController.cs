using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class AdminChatRoomController : Controller
    {
        public IActionResult Index()
        {
            return View(); 
        }
    }
}
