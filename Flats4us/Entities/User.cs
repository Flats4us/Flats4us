using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("User")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,50}$")]
        public string PasswordHash { get; set; }
    }
}
