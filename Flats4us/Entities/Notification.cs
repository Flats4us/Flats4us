using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public int UserId { get; set; }

        [Required]
        public bool IsChat { get; set; }

        public virtual User User { get; set; }
    }
}
