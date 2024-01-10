using Flats4us.Helpers.Enums;
using Helpers;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class SurveyStudentDto
    {
        [Required]
        public int Party { get; set; }

        [Required]
        public int Tidiness { get; set; }

        [Required]
        public bool Smoking { get; set; }

        [Required]
        public bool Sociability { get; set; }

        [Required]
        public bool Animals { get; set; }

        [Required]
        public bool Vegan { get; set; }

        [Required]
        public bool LookingForRoommate { get; set; }

        public int? MaxNumberOfRoommates { get; set; }

        public Gender? RoommateGender { get; set; }

        public int? MinRoommateAge { get; set; }

        public int? MaxRoommateAge { get; set; }

        public string? City { get; set; }
    }
}