using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("StudentMeeting")]
    public class StudentMeeting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int MeetingId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Meeting Meeting { get; set; }

    }
}
