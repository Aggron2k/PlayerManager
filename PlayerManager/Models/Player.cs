using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerManager.Models
{
    public enum Position
    {
        Goalkeeper,
        Defender,
        Midfielder,
        Attacker
    }

    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 10, ErrorMessage = "Ez egy kötelező mező.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Expected format: asd@asd.com")]
        public string Name { get; set; }
        [Required]
        [Range(18, 100)]
        public int Age { get; set; }
        [Required]
        public Position Position { get; set; }

        [Required]
        public bool Retired { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Contract Expired")]
        public DateTime ContractExp { get; set; }

        [ForeignKey("ReferencedTeam")]
        [Display(Name = "Team")]
        public int TeamId { get; set; }

        [Display(Name = "Team")]
        public virtual Team? ReferencedTeam { get; set; }
    }
}
