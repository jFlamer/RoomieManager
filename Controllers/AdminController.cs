using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomieManager.Data;

namespace RoomieManager.Controllers
{
    [Authorize(Policy = "AdminOnly")]
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
            var users = _context.Users.ToList();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.userId == userId);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("AdminPanel");
        }

        [HttpGet]
        public IActionResult EditUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.userId == userId);
            if (user == null) return NotFound();

            return View(user); // Pass a single user, not a list
        }
        
        [HttpPost]
        public IActionResult EditUser(int userId, string _userName, string _password, string _confirmPassword, bool _isAdmin)
        {
            var user = _context.Users.FirstOrDefault(u => u.userId == userId);
            if (user == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(_password))
            {
                if (_password != _confirmPassword)
                {
                    ViewBag.ErrorMessage = "Passwords do not match.";
                    return View(user); // Return original model to re-show form
                }
            }

            if (_password != null && _confirmPassword != null)
            {
                user.password = GenerateMD5Hash(_password); // Or leave unchanged if empty
            }

            if (_password != _confirmPassword)
            {
                ViewBag.ErrorMessage = "Passwords do not match.";
                return View(user); // Stay on edit page
            }

            if (_userName != null)
            {
                user.userName = _userName;
            }
            
            user.isAdmin = _isAdmin;

            _context.Users.Update(user);
            _context.SaveChanges();

            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        public IActionResult AddUser(string _userName, string _password, string _confirmPassword, bool _isAdmin)
        {
            if (_context.Users.Any(u => u.userName == _userName))
            {
                ViewBag.ErrorMessage = "Username is already taken.";
                return RedirectToAction("AdminPanel");
            }
            if (_password != _confirmPassword)
            {
                ViewBag.ErrorMessage = "Passwords do not match.";
                return RedirectToAction("AdminPanel");
            }
            var hashedPassword = GenerateMD5Hash(_password);
            var user = new Models.UserModel
            {
                userName = _userName,
                password = hashedPassword,
                isAdmin = _isAdmin
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var roomie = new Models.RoomieModel
            {
                userId = user.userId,
                name = _userName,
                photoURL = "/profile_pictures/default-user.png"
            };

            _context.Roomies.Add(roomie);
            _context.SaveChanges();
            return RedirectToAction("AdminPanel");
        }

        private string GenerateMD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }

    }

}