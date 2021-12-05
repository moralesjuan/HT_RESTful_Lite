using HT_RESTful_Lite.Entities;
using HT_RESTful_Lite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HT_RESTful_Lite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Cups.OrderBy(c => c.CupId)
                                        .ToListAsync();
            return Ok(list);
        }

        [HttpGet("{cupId:int}")]
        public async Task<IActionResult> GetByCupId(int cupId)
        {
            var cup = await _context.Cups.Where(c => c.CupId == cupId)
                                        .FirstOrDefaultAsync();
            return Ok(cup);
        }

        [HttpGet("Details/{cupId:int}")]
        public async Task<IActionResult> GetCupDetails(int cupId)
        {
            var list = await _context.CupDetails.Where(c => c.CupId == cupId)
                                                .Include(c => c.Cup)
                                                .ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateCup(Cups cups)
        {
            string query = "";
            var result = 0;

            if (cups.Type == "HighLow")
            {
                query = $"EXECUTE dbo.CreateHighLowCup '{cups.Name}', {cups.Size}";
            }
            if (cups.Type == "HighLowRandom")
            {
                query = $"EXECUTE dbo.CreateHighLowRandomCup '{cups.Name}', {cups.Size}";
            }
            if (cups.Type == "Random")
            {
                query = $"EXECUTE dbo.CreateRandomCup '{cups.Name}', {cups.Size}";
            }

            //var newCup = await _context.Database.ExecuteSqlRawAsync(query).
            //            .FromSqlRaw(query)
            //            .FirstOrDefaultAsync();

            ////_context.Database.ExecuteSqlInterpolated() .ExecuteSqlCommand("CreateStudents @p0, @p1", parameters: new[] { "Bill", "Gates" });
            //return Ok(newCup);


            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = (int)reader.GetValue(0);
                    }
                }
            }
            return Ok(result);
        }
    }
}
