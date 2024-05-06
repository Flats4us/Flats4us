using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class TokenDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public long ExpiresAt { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }
    }
}
