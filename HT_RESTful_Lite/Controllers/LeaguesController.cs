using HT_RESTful_Lite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HT_RESTful_Lite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LeaguesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Leagues.OrderBy(l => l.Level)
                                        .ToListAsync();
            return Ok(list);
        }

        [HttpGet("{leagueId:int}")]
        public async Task<IActionResult> GetByLeagueId(int leagueId)
        {
            var list = await _context.Leagues.Where(l => l.LeagueId == leagueId)
                                        .OrderBy(l => l.Level)
                                        .FirstOrDefaultAsync();
            return Ok(list);
        }

        [HttpGet("Level/{level:int}")]
        public async Task<IActionResult> GetByLevel(int level)
        {
            var list = await _context.Leagues.Where(l => l.Level == level)
                                        .OrderBy(l => l.Level)
                                        .ToListAsync();
            return Ok(list);
        }

    }
}
