using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Flat")]
    public class Flat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlatId { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        [MaxLength(60)]
        public string AddressLine1 { get; set; }

        [MaxLength(60)]
        public string AddressLine2 { get; set; }

        [MaxLength(60)]
        public string AddressLine3 { get; set; }

        [Required]
        [MaxLength(5)]
        public float MetricArea { get; set;}

        [Required]
        [MaxLength(5)]
        public int MaxNumberOfInhabitants { get; set;}

        public virtual ICollection<Rent> Rents { get; set; }

        public Flat()
        {
            Rents = new HashSet<Rent>();
        }
    }
}
