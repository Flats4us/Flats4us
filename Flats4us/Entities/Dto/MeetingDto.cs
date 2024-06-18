using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class MeetingDto
    {
        [Required]
        public int MeetingId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public int OfferId { get; set; }

        public DateTime? StudentAcceptDate { get; set; }

        public DateTime? OwnerAcceptDate { get; set; }

        public bool? NeedsAction { get; set; }

        [Required]
        public UserInfoDto OtherUser { get; set; }
    }

    public class MeetingWithStudentDto : MeetingDto { }

    public class MeetingWithOwnerDto : MeetingDto { }
}
