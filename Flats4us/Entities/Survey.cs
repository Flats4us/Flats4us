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


    }
}
