using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Tenant")]
    public class Tenant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TenantId { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Surname { get; set; }

        [Required]
        [MaxLength(60)]
        public string? AddressLine1 { get; set; }

        [MaxLength(60)]
        public string? AddressLine2 { get; set; }

        [MaxLength(60)]
        public string? AddressLine3 { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Email { get; set;}

        [Required]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }

        public Tenant()
        { 
            Rents = new HashSet<Rent>();
        }
    }
}
