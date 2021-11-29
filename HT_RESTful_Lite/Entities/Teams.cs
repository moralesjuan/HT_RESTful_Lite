using System.ComponentModel.DataAnnotations;

namespace HT_RESTful_Lite.Entities
{
    public class Teams
    {
        [Key]
        public int TeamId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
