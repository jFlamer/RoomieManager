using Microsoft.AspNetCore.Mvc;
using RoomieManager.Models;
using RoomieManager.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;

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
    public IActionResult Login(string login, string password){

        var hashedPassword = GenerateMD5Hash(password);
        var user = _context.Users.FirstOrDefault(u => u.userName == login && u.password == hashedPassword);

        if (user != null)
        {
            // User is authenticated, redirect to the main page or dashboard
            HttpContext.Session.SetString("UserName", login);
            HttpContext.Session.SetString("Logged", true.ToString());
            return RedirectToAction("Index", "Home");
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
    public IActionResult Register(string login, string password, string confirmPassword)
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

        // Registration successful, redirect to the login page or show a success message
        return RedirectToAction("Index", "Home");
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