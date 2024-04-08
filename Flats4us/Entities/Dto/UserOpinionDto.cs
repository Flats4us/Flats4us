using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class UserOpinionDto
    {
        [Required]
        public int UserOpinionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        public bool Helpful { get; set; }

        [Required]
        public bool Cooperative { get; set; }

        [Required]
        public bool Tidy { get; set; }

        [Required]
        public bool Friendly { get; set; }

        [Required]
        public bool RespectingPrivacy { get; set; }

        [Required]
        public bool Communicative { get; set; }

        [Required]
        public bool Unfair { get; set; }

        [Required]
        public bool LackOfHygiene { get; set; }

        [Required]
        public bool Untidy { get; set; }

        [Required]
        public bool Conflicting { get; set; }

        [Required]
        public bool Noisy { get; set; }

        [Required]
        public bool NotFollowingTheArrangements { get; set; }

        public string? Description { get; set; }

        [Required]
        public int SourceUserId { get; set; }

        [Required]
        public string SourceUserName { get; set; }

        [Required]
        public FileDto SourceUserProfilePicture { get; set; }
    }
}
