using Microsoft.AspNetCore.Mvc;
using RoomieManager.Models;
using RoomieManager.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace RoomieManager.Controllers{

[Authorize]
public class IOController : Controller
{
    private readonly RoomieManagerDBContext _context;

    public IOController(RoomieManagerDBContext context)
    {
        _context = context;
    }

    [HttpGet, AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Login(string login, string password){

        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        {
            ViewBag.Error = "Login and password cannot be empty.";
            return View();
        }

        var hashedPassword = GenerateMD5Hash(password);
        var user = _context.Users.FirstOrDefault(u => u.userName == login && u.password == hashedPassword);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.userName),
                    new Claim("UserId", user.userId.ToString()),
                    new Claim("isAdmin", user.isAdmin.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // This will keep the user logged in across sessions
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Set cookie expiration time
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
                
                return RedirectToAction("RoomieProfile", "Profile");

                // User is authenticated, redirect to the main page or dashboard
                // HttpContext.Session.SetString("UserName", login);
                // HttpContext.Session.SetString("Logged", true.ToString());
                // return RedirectToAction("Index", "Home");
            }
        
        // Authentication failed, show an error message
        ViewBag.ErrorMessage = "Invalid username or password.";
        return View();
    }

    [HttpGet, AllowAnonymous]
    public IActionResult Register()
    {
            return View();
    }
    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Register(string login, string password, string confirmPassword)
    {
        // Check if the username is taken
        if (_context.Users.Any(u => u.userName == login))
        {
            ViewBag.ErrorMessage = "Username is already taken.";
            return View();
        }
        // Check if the passwords match
        if (password != confirmPassword)
        {
            ViewBag.ErrorMessage = "Passwords do not match.";
            return View();
        }
        var hashedPassword = GenerateMD5Hash(password);
        var user = new UserModel
        {
            userName = login,
            password = hashedPassword,
            isAdmin = false
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        
            var roomie = new RoomieModel
            {
                userId = user.userId,
                name = login,
                photoURL = "/profile_pictures/default-user.png"
            };
        _context.Roomies.Add(roomie);

        _context.SaveChanges();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.userName),
            new Claim("UserId", user.userId.ToString()),
            new Claim("isAdmin", user.isAdmin.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // This will keep the user logged in across sessions
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Set cookie expiration time
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        
        return RedirectToAction("RoomieProfile", "Profile");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        // HttpContext.Session.Clear();
        // Response.Cookies.Delete(".AspNetCore.Session");
        return RedirectToAction("Login");
    }


    private string GenerateMD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }

    // public IActionResult Index()
    // {
    //     return View();
    // }

    // [HttpPost]
    // public IActionResult ImportData()
    // {
    //     // Logic to import data from a file or other source
    //     // This is just a placeholder for the actual implementation
    //     return RedirectToAction("Index");
    // }

    // [HttpPost]
    // public IActionResult ExportData()
    // {
    //     // Logic to export data to a file or other destination
    //     // This is just a placeholder for the actual implementation
    //     return RedirectToAction("Index");
    // }


}
}