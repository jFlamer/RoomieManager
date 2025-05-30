using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomieManager.Data;
using RoomieManager.Models;

namespace RoomieManager.Controllers
{
    [Authorize]
    public class RankingController : Controller
    {
        private readonly RoomieManagerDBContext _context;

        public RankingController(RoomieManagerDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var roomies = _context.Roomies.ToList();
            return View(roomies);
        }

        [HttpGet]
        public IActionResult Ranking(string sortBy = "points")
        {
            var ranking = _context.Roomies.Select(r => new
            {
                r.roomieId,
                r.name,
                r.photoURL,
                totalEP = _context.Tasks.Where(t => t.roomieID == r.roomieId && t.reviewRoomieID != null).Sum(t => (int?)t.taskType.effortPoints) ?? 0,
                totalTasks = _context.Tasks.Count(t => t.roomieID == r.roomieId && t.reviewRoomieID != null)
            });

            if (sortBy == "tasks")
            {
                ranking = ranking.OrderByDescending(r => r.totalTasks);
            }
            else
            {
                ranking = ranking.OrderByDescending(r => r.totalEP);
            }

            var rankedView = ranking.Select(r => new RankedRoomiesViewModel
            {
                RoomieId = r.roomieId,
                Name = r.name,
                PhotoURL = r.photoURL,
                totalEffortPoints = r.totalEP,
                totalTasks = r.totalTasks
            }).ToList();

            ViewBag.SortBy = sortBy;

            return View(rankedView);
        }
       
    }
}