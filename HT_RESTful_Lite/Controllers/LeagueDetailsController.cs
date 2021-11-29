using HT_RESTful_Lite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HT_RESTful_Lite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public LeagueDetailsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.LeagueDetails.OrderBy(l => l.League.Level)
                                              .ThenBy(l => l.League.Series)
                                              .Include(l => l.League)
                                              .Include(l => l.Team)
                                              .ToListAsync();
            return Ok(list);
        }

        [HttpGet("{LeagueId:int}")]
        public async Task<IActionResult> GetBy(int LeagueId)
        {
            var list = await _db.LeagueDetails.Where(l => l.LeagueId == LeagueId)
                                              .Include(l => l.League)
                                              .Include(l => l.Team)
                                              .OrderByDescending(l => l.Points)
                                              .ThenByDescending(l => (l.For - l.Against))
                                              .ThenByDescending(l => l.For)
                                              .ToListAsync();
            if (list.Count == 0)
            {
                return NotFound();
            }
            return Ok(list);
        }
    }
}
