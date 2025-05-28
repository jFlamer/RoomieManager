using Microsoft.AspNetCore.Mvc;
using RoomieManager.Data;
using RoomieManager.Models;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace RoomieManager.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly RoomieManagerDBContext _context;
        private static ConcurrentDictionary<string, int> tokens = new();

        public ApiController(RoomieManagerDBContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string userName, [FromForm] string password)
        {
            var hashed = GenerateMD5Hash(password);
            var user = _context.Users.FirstOrDefault(u => u.userName == userName && u.password == hashed);
            if (user == null) return Unauthorized("Invalid credentials");

            var token = Guid.NewGuid().ToString();
            tokens[token] = user.userId;
            return Ok(new { token });
        }

        [HttpPost("users")]
        public IActionResult CreateUser([FromForm] string userName, [FromForm] string password, [FromForm] bool isAdmin, [FromHeader] string token)
        {
            if (!IsAuthenticated(token)) return Unauthorized();

            if (_context.Users.Any(u => u.userName == userName))
                return BadRequest("Username already exists");

            var user = new UserModel
            {
                userName = userName,
                password = GenerateMD5Hash(password),
                isAdmin = isAdmin
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var roomie = new RoomieModel
            {
                userId = user.userId,
                name = userName,
                photoURL = "/profile_pictures/default-user.png"
            };

            _context.Roomies.Add(roomie);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpDelete("users/{id}")]
        public IActionResult DeleteUser(int id, [FromHeader] string token)
        {
            return Forbid("Deleting users is not allowed via API");
        }

        [HttpPost("tasks/{taskId}/assign")]
        public IActionResult AssignSelfToTask(int taskId, [FromHeader] string token)
        {
            if (!IsAuthenticated(token)) return Unauthorized();
            var userId = tokens[token];

            var task = _context.Tasks.FirstOrDefault(t => t.taskID == taskId);
            if (task == null) return NotFound("Task not found");

            task.roomieID = userId;
            _context.SaveChanges();
            return Ok("Assigned yourself to task");
        }

        [HttpPost("tasks")]
        public IActionResult CreateTask([FromForm] int typeId, [FromHeader] string token)
        {
            if (!IsAuthenticated(token)) return Unauthorized();

            var task = new TaskModel
            {
                typeID = typeId
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();
            return Ok(task);
        }

        [HttpDelete("tasks/{id}")]
        public IActionResult DeleteTask(int id, [FromHeader] string token)
        {
            if (!IsAuthenticated(token)) return Unauthorized();

            var task = _context.Tasks.FirstOrDefault(t => t.taskID == id);
            if (task == null) return NotFound("Task not found");

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok("Task deleted");
        }

        private bool IsAuthenticated(string token)
        {
            return tokens.ContainsKey(token);
        }

        private string GenerateMD5Hash(string input)
        {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
