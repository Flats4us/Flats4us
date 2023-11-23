using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class UserForVerificationDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public UserType UserType { get;}

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string ImagesPath { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        [Required]
        public DateTime DocumentExpireDate { get; set; }

        public string? StudentNumber { get; set; }

        public string? University { get; set; }

        public string? DocumentNumber { get; set; }
    }
}
