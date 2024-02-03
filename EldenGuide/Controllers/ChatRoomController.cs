using Microsoft.AspNetCore.Mvc;

namespace EldenGuide.Controllers
{
    public class ChatRoomController : Controller
    {
        public IActionResult Index()
        {
            var roomId = Guid.NewGuid().ToString();
            ViewBag.RoomId = roomId; 

            return View();
        }
    }
}
