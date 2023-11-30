using Flats4us.Helpers.Enums;
using Helpers;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class SurveyStudentDto
    {
        public int SurveyStudentId { get; set; }
        public int Party { get; set; }
        public int Tidiness { get; set; }
        public bool Smoking { get; set; }
        public int Sociability { get; set; }
        public bool Animals { get; set; }
        public bool Vegan { get; set; }
        public bool LookingForRoommate { get; set; }
        public int MaxNumberOfRoommates { get; set; }
        public Gender RoommateGender { get; set; }
        public int MinRoommateAge { get; set; }
        //public virtual Student Student { get; set; }
    }
}
