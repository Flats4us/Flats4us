using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class PasswordResetDto
    {
        [Required]
        public string NewPassword { get; set; }
    }
}
