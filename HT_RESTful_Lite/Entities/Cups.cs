using System.ComponentModel.DataAnnotations;

namespace HT_RESTful_Lite.Entities
{
    public class Cups
    {
        [Key]
        public int CupId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Size { get; set; }
    }
}