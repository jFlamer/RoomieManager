using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomieManager.Data;
using RoomieManager.Models;

namespace RoomieManager.Controllers;

public class HomeController : Controller
{
    private readonly RoomieManagerDBContext _context;

    public HomeController(RoomieManagerDBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var roomies = _context.Roomies.ToList();
        return View(roomies);
    }

    // private readonly ILogger<HomeController> _logger;



    // public HomeController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }

    // public IActionResult Index()
    // {
    //     return View();
    // }

    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
