using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomieManager.Data;

namespace RoomieManager.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly RoomieManagerDBContext _context;

        public AdminController(RoomieManagerDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AdminPanel()
        {
            return View();
        }
    }

}