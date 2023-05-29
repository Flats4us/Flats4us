using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Entities
{
    [Table("RentOpinion")]
    public class RentOpinion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("Rent")]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Scale 1-10
        [Required]
        public int Localization { get; set; }

        // Scale 1-10
        [Required]
        public int Neighbors { get; set; }

        // Scale 1-10
        [Required]
        public int Equipment { get; set; }

        // Scale 1-10
        [Required]
        public int ParkingPlace { get; set; }

        // Scale 1-10
        [Required]
        public int Tidiness { get; set; }

        // Scale 1-10
        [Required]
        public int Decoration { get; set; }

        // Scale 1-10
        [Required]
        public int Loudness { get; set; }

        // Scale 1-10
        [Required]
        public int ComplianceWithOffer { get; set; }

        public virtual Rent Rent { get; set; }


    }
}