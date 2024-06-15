using Flats4us.Helpers.Enums;

namespace Flats4us.Entities.Dto
{
    public class EditUserDto
    {
        // Fields from EditUserGeneral
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Fields from EditUserSensitive
        public string? Name { get; set; }
        public string? Surname { get; set; }

        // Fields from EditOwnerSensitiveDto
        public string? BankAccount { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime? DocumentExpireDate { get; set; }

        // Fields from EditStudentSensitiveDto
        public DateTime? BirthDate { get; set; }
        public string? StudentNumber { get; set; }
        public string? University { get; set; }
        public List<string>? Links { get; set; }
        public List<int>? InterestIds { get; set; }


    }
}
