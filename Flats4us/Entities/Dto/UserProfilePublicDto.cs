using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class UserProfilePublicDto
    {
        [Required]
        public UserType UserType { get; set; }

        // User

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime AccountCreationDate { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        // OwnerStudent

        public FileDto? ProfilePicture { get; set; }

        // Student

        public int? Age { get; set; }

        public string? University { get; set; }

        public List<string>? Links { get; set; }

        public SurveyStudentDto? SurveyStudent { get; set; }

        public List<InterestDto>? Interests { get; set; }
    }
}
