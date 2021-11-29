using HT_RESTful_Lite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HT_RESTful_Lite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public TeamsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Teams.OrderBy(t => t.TeamId)
                                      .ToListAsync();
            return Ok(list);
        }
    }
}
