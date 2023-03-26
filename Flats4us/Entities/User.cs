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
        public string Username { get; set; }
        [Required]
        
        public string PasswordHash { get; set; }
    }
}
