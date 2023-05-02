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

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentOpinion>().HasData(
            new RentOpinion { 
                Id = 1,
                Date = new DateTime(2022, 1, 15),
                Localization = 8, 
                Neighbors = 7, 
                Equipment = 9, 
                ParkingPlace = 6,
                Tidiness = 9, 
                Decoration = 7,
                Loudness = 4,
                ComplianceWithOffer = 8
            },
            new RentOpinion {
                Id = 2,
                Date = new DateTime(2022, 2, 23),
                Localization = 7,
                Neighbors = 6,
                Equipment = 8,
                ParkingPlace = 5, 
                Tidiness = 8,
                Decoration = 6,
                Loudness = 5, 
                ComplianceWithOffer = 9 
            },
            new RentOpinion { 
                Id = 3,
                Date = new DateTime(2022, 3, 1),
                Localization = 9,
                Neighbors = 8,
                Equipment = 7, 
                ParkingPlace = 8,
                Tidiness = 7,
                Decoration = 7,
                Loudness = 6,
                ComplianceWithOffer = 7 
            },
            new RentOpinion { 
                Id = 4,
                Date = new DateTime(2022, 4, 10),
                Localization = 6,
                Neighbors = 7, 
                Equipment = 6,
                ParkingPlace = 4, 
                Tidiness = 6, 
                Decoration = 5,
                Loudness = 7,
                ComplianceWithOffer = 8 
            },
            new RentOpinion { 
                Id = 5,
                Date = new DateTime(2022, 5, 5), 
                Localization = 8, 
                Neighbors = 9, 
                Equipment = 7,
                ParkingPlace = 8, 
                Tidiness = 9,
                Decoration = 7,
                Loudness = 3, 
                ComplianceWithOffer = 8
            });
        }
    }
}