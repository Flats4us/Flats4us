using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int Flat { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }

    public class OwnerStudentRegisterDto : UserRegisterDto
    {
        public string PhotoPath { get; set; }
        public bool ActivityStatus { get; set; }
        public string DocumentPath { get; set; }
        public DocumentType DocumentType { get; set; }
        public VerificationStatus VerificationStatus { get; set; }
        public DateTime DocumentExpireDate { get; set; }
    }

    public class StudentRegisterDto : OwnerStudentRegisterDto
    {
        
        [Required]
        public int YearOfBirth { get; set; }

        [Required]
        public string StudentNumber { get; set; }

        [Required]
        public string University { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Instagram { get; set; }

        [Required]
        public RoommatesStatus RoommatesStatus { get; set; }

        [Required]
        public bool IsTenant { get; set; }
    }

    public class OwnerRegisterDto : OwnerStudentRegisterDto
    {
        [Required]
        public string BankAccount { get; set; }
    }

}
