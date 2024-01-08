using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Rent
    {
        [Key, ForeignKey("Offer")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? NextPaymentDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int OfferId {  get; set; }

        public virtual OpinionRent OpinionRent { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual Student Student { get; set; }

        public virtual ICollection<Student> OtherStudents { get; set; }

        public Rent()
        {
            OtherStudents = new HashSet<Student>();
        }
    }
}
