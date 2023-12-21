using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Rent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        // In Months
        [Required]
        public int RentPeriod { get; set; }

        [Required]
        public string ContractInfo { get; set; }

        [Required]
        public int StudentId { get; set; }

        public virtual OpinionRent OpinionRent { get; set; }

        public virtual Offer Offers { get; set; }

        public virtual Student Student { get; set; }

        public virtual ICollection<Student> OtherStudents { get; set; }

        public Rent()
        {
            OtherStudents = new HashSet<Student>();
        }
    }
}
