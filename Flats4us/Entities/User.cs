using Flats4us.Helpers.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("User")]
    public abstract class User
    {
        public const int MinPasswordLenght = 6;
        public const int MaxPasswordeLenght = 30;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime AccountCreationDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public string? PasswordResetToken { get; set; }

        public DateTime? PasswordResetTokenExpireDate { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        public bool PushChatConsent { get; set; } = true;
        public bool EmailChatConsent { get; set; } = true;
        public bool PushPropertyConsent { get; set; } = true;
        public bool EmailPropertyConsent { get; set; } = true;
        public virtual ICollection<UserGroupChat> UserGroupChats { get; set; }

        public virtual ICollection<TechnicalProblem> TechnicalProblems { get; set; }
        public string? FcmToken { get; set; }
    }
}
