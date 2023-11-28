using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public int? Flat { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class OwnerStudentRegisterDto : UserRegisterDto
    {
        public IFormFile ProfilePicture { get; set; }
        public IFormFile Document { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime DocumentExpireDate { get; set; }
    }

    public class StudentRegisterDto : OwnerStudentRegisterDto
    {
        
        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string StudentNumber { get; set; }

        [Required]
        public string University { get; set; }

        public string? Facebook { get; set; }

        public string? Twitter_X { get; set; }

        public string? Instagram { get; set; }

        [Required]
        public RoommatesStatus RoommatesStatus { get; set; }

        [Required]
        public bool IsTenant { get; set; }
    }

    public class OwnerRegisterDto : OwnerStudentRegisterDto
    {
        [Required]
        public string BankAccount { get; set; }

        [Required]
        public string DocumentNumber { get; set; }
    }
}
