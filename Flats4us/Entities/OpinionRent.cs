using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class OpinionRent
    {
        [ForeignKey("Rent")]
        public int OpinionRentId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Scale 1-10
        [Required]
        public int Location { get; set; }

        // Scale 1-10
        [Required]
        public int Neighbors { get; set; }

        // Scale 1-10
        [Required]
        public int Equipment { get; set; }

        // Scale 1-10
        [Required]
        public int ParkingSpace { get; set; }

        // Scale 1-10
        [Required]
        public int Tidiness { get; set; }

        // Scale 1-10
        [Required]
        public int Decoration { get; set; }

        // Scale 1-10
        [Required]
        public int Noisiness { get; set; }

        // Scale 1-10
        [Required]
        public int ComplianceWithOffer { get; set; }

        public virtual Rent Rent { get; set; }
    }
}
