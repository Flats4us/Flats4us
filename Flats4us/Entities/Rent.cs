using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Rent")]
    public class Rent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int? LengthInMonths { get; set; }

        [Required]
        public string? ContractInformations { get; set; }

        public virtual RentOpinion RentOpinion { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual ICollection<Student> OtherTenants { get; set; }

        public Rent()
        {
            OtherTenants = new HashSet<Student>();
        }

    }
}
