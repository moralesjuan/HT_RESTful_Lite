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

        //[HttpGet("Details/{cupId:int}")]
        //public async Task<IActionResult> GetCupDetails(int cupId)
        //{
        //    var list = await _context.CupDetails.Where(c => c.CupId == cupId)
        //                                        .Include(c => c.Cup)
        //                                        .ToListAsync();
        //    return Ok(list);
        //}

        [HttpGet("Details/{cupId:int}")]
        public IActionResult GetCupDetails(int cupId)
        {
            string query = $"EXECUTE dbo.CupFullDetails {cupId}";

            List<CupDetailsResult> list = new List<CupDetailsResult>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cupDetailsResult = new CupDetailsResult();
                        cupDetailsResult.CupId = (int)reader.GetValue(0);
                        cupDetailsResult.CupName = (string)reader.GetValue(1);
                        cupDetailsResult.CupType = (string)reader.GetValue(2);
                        cupDetailsResult.CupSize = (int)reader.GetValue(3);
                        cupDetailsResult.Round = (int)reader.GetValue(4);
                        cupDetailsResult.Local = (string)reader.GetValue(5);
                        cupDetailsResult.RankingLocal = (int)reader.GetInt64(6);
                        cupDetailsResult.Visitor = (string)reader.GetValue(7);
                        cupDetailsResult.RankingVisitor =(int)reader.GetInt64(8);
                        list.Add(cupDetailsResult);
                    }
                }
            }
            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateCup(Cups cups)
        {
            string query = "";

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
            Cups CupResult = new Cups();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CupResult.CupId = (int)reader.GetValue(0);
                        CupResult.Name = (string)reader.GetValue(1);
                        CupResult.Type = (string)reader.GetValue(2);
                        CupResult.Size = (int)reader.GetValue(3);
                    }
                }
            }
            return Ok(CupResult);
        }

        private class CupDetailsResult
        {
            public int CupId { get; set; }  
            public string CupName { get; set; }
            public string CupType { get; set; }
            public int CupSize { get; set; }
            public int Round { get; set; }
            public string Local { get; set; }
            public int RankingLocal { get; set; }
            public string Visitor { get; set; }
            public int RankingVisitor { get; set; }

        }
    }
}
