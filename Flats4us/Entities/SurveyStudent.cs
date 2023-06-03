using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class SurveyStudent
    {
        [ForeignKey("Student")]
        public int SurveyStudentId { get; set; }

        // Scale 1-10
        [Required]
        public int Party { get; set; }

        // Scale 1-10
        [Required]
        public int Tidiness { get; set; }

        // Yes/No
        [Required]
        public bool Smoking { get; set; }

        // Scale 1-10
        [Required]
        public int Sociability { get; set; }

        // Yes/No
        [Required]
        public bool Animals { get; set; }

        // Yes/No
        [Required]
        public bool Vegan { get; set; }

        // Yes/No
        [Required]
        public bool LookingForRoommate { get; set; }

        // 0-6
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
