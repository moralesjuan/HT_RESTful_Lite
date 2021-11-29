using System.ComponentModel.DataAnnotations;

namespace HT_RESTful_Lite.Entities
{
    public class Leagues
    {
        [Key]
        public int LeagueId { get; set; }
        [Required]
        public string Series { get; set; }
        [Required]
        public int Level { get; set; }
    }
}
