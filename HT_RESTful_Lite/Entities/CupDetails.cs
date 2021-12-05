using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HT_RESTful_Lite.Entities
{
    public class CupDetails
    {
        [Key, Column(Order = 0)]
        public int CupId { get; set; }

        [Key, Column(Order = 1)]
        public int Round { get; set; }

        [Key, Column(Order = 2)]
        public int LocalTeamId { get; set; }

        [Key, Column(Order = 3)]
        public int VisitorTeamId { get; set; }

        public int Winner { get; set; }

        [ForeignKey("CupId")]
        [Required]
        public Cups Cup { get; set; }
       
    }
}