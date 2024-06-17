using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public abstract class BaseUserProfileDto
    {
        [Required]
        public UserType UserType { get; set; }

        [Required]
        public int UserId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime AccountCreationDate { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        public FileDto? ProfilePicture { get; set; }

        public decimal? AvgRating { get; set; }

        public int? SumHelpful { get; set; }

        public int? SumCooperative { get; set; }

        public int? SumTidy { get; set; }

        public int? SumFriendly { get; set; }

        public int? SumRespectingPrivacy { get; set; }

        public int? SumCommunicative { get; set; }

        public int? SumUnfair { get; set; }

        public int? SumLackOfHygiene { get; set; }

        public int? SumUntidy { get; set; }

        public int? SumConflicting { get; set; }

        public int? SumNoisy { get; set; }

        public int? SumNotFollowingTheArrangements { get; set; }

        public List<UserOpinionDto>? UserOpinions { get; set; }

        public string? University { get; set; }

        public List<string>? Links { get; set; }

        public SurveyStudentDto? SurveyStudent { get; set; }

        public List<InterestDto>? Interests { get; set; }
    }

    public class UserProfilePublicDto : BaseUserProfileDto
    {
        public int? Age { get; set; }
    }

    public class UserProfileFullDto : BaseUserProfileDto
    {
        [Required]
        public string Surname { get; set; }

        [Required]
        public string Address { get; set; }

        public FileDto? Document { get; set; }

        public DateTime? DocumentExpireDate { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? StudentNumber { get; set; }

        // Właściwości specyficzne dla właściciela
        public string? BankAccount { get; set; }

        public string? DocumentNumber { get; set; }
    }
}
