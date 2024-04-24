using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class OwnerStudentDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public FileDto ProfilePicture { get; set; }
    }
}
