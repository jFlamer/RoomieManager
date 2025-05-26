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

        public IActionResult Index()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            var taskTypes = _context.TaskTypes.ToList();
            ViewBag.TaskTypes = taskTypes;
            return View();
        }

        [HttpPost]
        public IActionResult CreateTask(int taskTypeID)
        {
            TaskTypeModel tasktype = _context.TaskTypes.FirstOrDefault(t => t.taskTypeId == taskTypeID);
            if (tasktype == null)
            {
                ViewBag.Error = "Task type not found.";
                return View();
            }

            var task = new TaskModel
            {
                typeID = taskTypeID,
                taskType = tasktype
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();
            return RedirectToAction("Index", "Task");
        }

        [HttpGet]
        public IActionResult DeleteTask()
        {
            return View();

        }

        [HttpPost]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.taskID == id);
            if (task == null)
            {
                return NotFound();

            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("Index", "Task");
        }
    }
}