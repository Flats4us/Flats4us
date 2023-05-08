using Flats4us.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class SurveyDto
    {
        public bool Smoking { get; set; }
        public bool Animal { get; set; }
        public bool Vegan { get; set; }
        public int Party { get; set; }
        public int Tidiness { get; set; }
        public int Loudness { get; set; }
        public int Sociability { get; set; }
        public int Helpfulness { get; set; }
        public bool Roommate { get; set; }
        public int MaxNumberOfRoommates { get; set; }
        public Gender RoommateGender { get; set; }
        public int MinRoommateAge { get; set; }
        public int MaxRoommateAge { get; set; }
    }
}
