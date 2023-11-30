using Flats4us.Helpers.Enums;
using Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities.Dto
{
    public class SurveyStudentDto
    {
        [Required]
        [SurveySlider(1, 10)]
        public int Party { get; set; }

        [Required]
        [SurveySlider(1, 10)]
        public int Tidiness { get; set; }

        [Required]
        [SurveySlider(1, 10)]
        public bool Smoking { get; set; }

        [Required]
        [SurveySlider(1, 10)]
        public int Sociability { get; set; }

        [Required]
        public bool Animals { get; set; }

        [Required]
        public bool Vegan { get; set; }

        [Required]
        public bool LookingForRoommate { get; set; }

        [Required]
        [SurveyIgnore]
        public int MaxNumberOfRoommates { get; set; }

        [Required]
        [SurveyIgnore]
        public Gender RoommateGender { get; set; }

        [Required]
        [SurveyIgnore]
        public int MinRoommateAge { get; set; }

        [Required]
        [SurveyIgnore]
        public int MaxRoommateAge { get; set; }
        
        [Required]
        public int StudentId { get; set; }
    }
}
