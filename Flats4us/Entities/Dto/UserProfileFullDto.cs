using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class UserProfileFullDto
    {

        [Required]
        public UserType UserType { get; set; }

        // User

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime AccountCreationDate { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        // OwnerStudent

        public FileDto? ProfilePicture { get; set; }

        public FileDto? Document { get; set; }

        public DateTime? DocumentExpireDate { get; set; }

        public List<UserOpinionDto> UserOpinions { get; set; }

        // Student

        public DateTime? BirthDate { get; set; }

        public string? StudentNumber { get; set; }

        public string? University { get; set; }

        public List<string>? Links { get; set; }

        public SurveyStudentDto? SurveyStudent { get; set; }

        public List<InterestDto>? Interests { get; set; }

        // Owner

        public string? BankAccount { get; set; }

        public string? DocumentNumber { get; set; }
    }
}