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
        private readonly ApplicationDbContext _context;

        public LeagueDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.LeagueDetails.OrderBy(l => l.League.Level)
                                              .ThenBy(l => l.League.Series)
                                              .Include(l => l.League)
                                              .Include(l => l.Team)
                                              .ToListAsync();
            return Ok(list);
        }

        [HttpGet("{leagueId:int}")]
        public async Task<IActionResult> GetBy(int leagueId)
        {
            var list = await _context.LeagueDetails.Where(l => l.LeagueId == leagueId)
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
