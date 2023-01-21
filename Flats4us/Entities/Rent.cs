using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Flats4us.Entities
{
    [Table("Rent")]
    public class Rent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentId { get; set; }

        [Required]
        public int TenantId { get; set; }

        [Required]
        public int FlatId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int DurationInMonths { get; set; }

        [Required]
        public float PricePerMonth { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual Flat Flat { get; set; }
    }
}
