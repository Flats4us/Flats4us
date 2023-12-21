using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class StudentDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string StudentNumber { get; set; }

        [Required]
        public string University { get; set; }
    }
}
