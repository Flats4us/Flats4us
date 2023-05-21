using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Moderator : User
    {
        [Required]
        public DateTime HireDate { get; set; }
    }
}
