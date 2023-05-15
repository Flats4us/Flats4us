using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("User")]
    public class User //abstract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public DateTime AccountCreationDate { get; set; }

        [Required]
        public DateTime LastLoginDate { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
