using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HT_RESTful_Lite.Entities
{
    public class LeagueDetails
    {
        [Key, Column(Order = 0)]
        public int LeagueId  { get; set; }

        [Key, Column(Order = 1)]
        public int TeamId  { get; set; }

        [ForeignKey("LeagueId")]
        [Required]
        public Leagues League { get; set; }

        [ForeignKey("TeamId")]
        [Required]
        public Teams Team { get; set; }

        [Required]
        public int Games { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int For { get; set; }

        [Required]
        public int Against { get; set; }

        [NotMapped]
        public double Difference
        {
            get
            {
                return For - Against;
            }
        }
    }
}
