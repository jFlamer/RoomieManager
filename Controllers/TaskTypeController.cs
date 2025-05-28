using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomieManager.Data;

namespace RoomieManager.Controllers
{
    [Authorize]
    public class TaskTypeController : Controller
    {
        private readonly RoomieManagerDBContext _context;
        public TaskTypeController(RoomieManagerDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var TaskTypes = _context.TaskTypes.ToList();
            return View(TaskTypes);
        }


        [HttpGet]
        public IActionResult CreateTaskType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTaskType(string name, string description, int duration, int effortPoints)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description) || duration <= 0 || effortPoints <= 0)
            {
                ViewBag.Error = "All fields are required and duration and effort points must be greater than zero.";
                return View();
            }

            var taskType = new Models.TaskTypeModel
            {
                name = name,
                description = description,
                duration = duration,
                effortPoints = effortPoints
            };

            _context.TaskTypes.Add(taskType);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditTaskType(int id)
        {
            var taskType = _context.TaskTypes.FirstOrDefault(t => t.taskTypeId == id);
            if (taskType == null)
            {
                return NotFound();
            }

            return View(taskType);
        }

        [HttpPost]
        public IActionResult EditTaskType(int id, string name, string description, int duration, int effortPoints)
        {
            var taskType = _context.TaskTypes.FirstOrDefault(t => t.taskTypeId == id);
            if (taskType == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                taskType.name = name;
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                taskType.description = description;
            }

            if (duration > 0)
            {
                taskType.duration = duration;
            }

            if (effortPoints > 0)
            {
                taskType.effortPoints = effortPoints;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteTaskType()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeleteTaskType(int id)
        {
            var taskType = _context.TaskTypes.FirstOrDefault(t => t.taskTypeId == id);
            if (taskType == null)
            {
                return NotFound();
            }

            _context.TaskTypes.Remove(taskType);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}