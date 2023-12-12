using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
