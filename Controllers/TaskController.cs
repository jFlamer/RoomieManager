using Microsoft.AspNetCore.Mvc;
using RoomieManager.Models;
using RoomieManager.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
            var taskTypes = _context.TaskTypes.ToList();
            var roomies = _context.Roomies.ToList();
            var taskPageViewModel = new TaskPageViewModel
            {
                Tasks = tasks,
                TaskTypes = taskTypes,
                Roomies = roomies
            };
            return View(taskPageViewModel);
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

        [HttpGet]
        public IActionResult ClaimTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.taskID == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }


        [HttpPost]
        public IActionResult ClaimTask(int id, DateTime plannedStartDateTime, DateTime plannedFinishDateTime)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.taskID == id);
            if (task == null)
            {
                return NotFound();
            }

            var userClaim = User.FindFirst("UserId")?.Value;
            if (userClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userClaim);
            var roomie = _context.Roomies.FirstOrDefault(r => r.userId == userId);
            if (roomie == null)
            {
                return NotFound();
            }

            task.roomieID = roomie.roomieId;
            task.plannedStartDateTime = plannedStartDateTime;
            task.plannedFinishDateTime = plannedFinishDateTime;
            _context.SaveChanges();

            return RedirectToAction("Index", "Task");

        }

        [HttpGet]
        public IActionResult ReviewTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.taskID == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }
        [HttpPost]
        public IActionResult ReviewTask(int id, DateTime reviewDateTime, string reviewNote)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.taskID == id);
            if (task == null)
            {
                return NotFound();
            }

            var userClaim = User.FindFirst("UserId")?.Value;
            if (userClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userClaim);
            var roomie = _context.Roomies.FirstOrDefault(r => r.userId == userId);
            if (roomie == null)
            {
                return NotFound();
            }

            if (task.roomieID == null || task.roomieID == roomie.roomieId)
            {
                ViewBag.Error = "You cannot review your own task.";
                return View(task);
            }

            task.reviewDateTime = reviewDateTime;
            task.reviewNote = reviewNote;
            task.reviewRoomieID = roomie.roomieId;
            _context.SaveChanges();
            return RedirectToAction("Index", "Task");
        }
    }
}