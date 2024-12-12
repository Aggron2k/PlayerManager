using System.ComponentModel.DataAnnotations;

namespace PlayerManager.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string TeamName { get; set; }
        [Required]
        public string Country { get; set; }
    
    }
}
