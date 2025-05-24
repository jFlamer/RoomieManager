using Microsoft.AspNetCore.Mvc;
using RoomieManager.Models;
using RoomieManager.Data;
using Microsoft.AspNetCore.Authorization;

namespace RoomieManager.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly RoomieManagerDBContext _context;
        public TaskController(RoomieManagerDBContext context)
        {
            _context = context;
        }

        // [HttpPost]
        // public IActionResult CreateTask(TaskModel taskModel)
        // {

        // }
    }
}