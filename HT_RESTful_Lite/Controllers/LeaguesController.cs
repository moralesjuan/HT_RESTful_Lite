using HT_RESTful_Lite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HT_RESTful_Lite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public LeaguesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Leagues.OrderBy(l => l.Level)
                                        .ToListAsync();
            return Ok(list);
        }

    }
}
