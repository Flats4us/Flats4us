using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class NotificationDto
    {
        [Required]
        public int NotificationId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public bool Read { get; set; }

        [Required]
        public bool IsChat { get; set; }
    }
}
