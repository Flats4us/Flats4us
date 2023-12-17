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

        [Required]
        public int OfferId { get; set; }

        [Required]
        public int StudentId {  get; set; }

        public virtual Offer Offer { get; set; }

        public virtual Student Student { get; set; }
    }
}
