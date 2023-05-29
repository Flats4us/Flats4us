using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class Meeting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeetingId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string Reason { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Meeting()
        {
            this.Students = new HashSet<Student>();
        }
    }
}
