using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public enum Gender
    {
        Male,
        Female,
        Both
    }


    [Table("Survey")]
    public class Survey //ankieta do studenta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("Student")]
        public int Id { get; set; }

        [Required]
        public bool Smoking { get; set; }

        [Required]
        public bool Animal { get; set; }

        [Required]
        public bool Vegan { get; set; }

        // Scale 1-10
        [Required]
        public int Party { get; set; }

        // Scale 1-10
        [Required]
        public int Tidiness { get; set; }

        // Scale 1-10
        [Required]
        public int Loudness { get; set; }

        // Scale 1-10
        [Required]
        public int Sociability { get; set; }

        // Scale 1-10
        [Required]
        public int Helpfulness { get; set; }

        [Required]
        public bool Roommate { get; set; }

        [Required]
        public int MaxNumberOfRoommates { get; set; }

        [Required]
        public Gender RoommateGender { get; set; }

        [Required]
        public int MinRoommateAge { get; set; }

        [Required]
        public int MaxRoommateAge { get; set; }

        public virtual Student Student { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>().HasData(
            new Survey
            {
                Smoking = false,
                Animal = false,
                Vegan = true,
                Party = 8,
                Tidiness = 9,
                Loudness = 4,
                Sociability = 7,
                Helpfulness = 6,
                Roommate = true,
                MaxNumberOfRoommates = 2,
                RoommateGender = Gender.Female,
                MinRoommateAge = 20,
                MaxRoommateAge = 25,
            },
            new Survey
            {
                Smoking = true,
                Animal = false,
                Vegan = false,
                Party = 5,
                Tidiness = 6,
                Loudness = 7,
                Sociability = 8,
                Helpfulness = 9,
                Roommate = false,
                MaxNumberOfRoommates = 1,
                RoommateGender = Gender.Male,
                MinRoommateAge = 22,
                MaxRoommateAge = 30
            },
            new Survey
            {
                Smoking = false,
                Animal = true,
                Vegan = false,
                Party = 9,
                Tidiness = 8,
                Loudness = 3,
                Sociability = 6,
                Helpfulness = 7,
                Roommate = true,
                MaxNumberOfRoommates = 3,
                RoommateGender = Gender.Both,
                MinRoommateAge = 18,
                MaxRoommateAge = 25
            });

        }
    }
}
