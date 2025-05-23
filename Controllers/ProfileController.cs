using Microsoft.AspNetCore.Mvc;
using RoomieManager.Models;
using RoomieManager.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace RoomieManager.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly RoomieManagerDBContext _context;

        public ProfileController(RoomieManagerDBContext context)
        {
            _context = context;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult RoomieProfile()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var user = _context.Users.FirstOrDefault(u => u.userName == userName);
            var roomie = _context.Roomies.FirstOrDefault(r => r.userId == user.userId);

            ViewBag.PicturePath = roomie?.photoURL;
            ViewBag.Name = roomie?.name;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> RoomieProfile(RoomieModel roomieModel, IFormFile file)
        {
            var userName = HttpContext.Session.GetString("UserName");
            var user = _context.Users.FirstOrDefault(u => u.userName == userName);
            var roomie = _context.Roomies.FirstOrDefault(r => r.userId == user.userId);

            if (roomie == null)
            {
                return NotFound();
            }

            if (!String.IsNullOrWhiteSpace(roomieModel.name))
            {
                roomie.name = roomieModel.name;
                _context.SaveChanges();
            }

            if (file != null && file.Length > 0)
                {
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile_pictures");
                    Directory.CreateDirectory(uploadPath);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}"; //using GUID to avoid name collisions
                    var filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    roomie.photoURL = $"/profile_pictures/{fileName}";
                    _context.SaveChanges();
                }
            return RedirectToAction("Profile");
        
    }
    }
}