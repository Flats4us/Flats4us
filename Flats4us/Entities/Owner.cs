using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Owner : OwnerStudent
    {
        [Required]
        public string? BankAccount { get; set; }

        [Required]
        public string? TitleDeedPath { get; set; }
    }
}
