using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class UserRegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Number { get; set; }

        public int? Flat { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }

    public class OwnerStudentRegisterDto : UserRegisterDto
    {
        [Required]
        public IFormFile ProfilePicture { get; set; }

        [Required]
        public IFormFile Document { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
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
        [Range(1, 10, ErrorMessage = "Value must be between 1 and 10.")]
        public int Party { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Value must be between 1 and 10.")]
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

        [Required]
        [ValidInterestJson(ErrorMessage = "Invalid JSON format in the InterestJson field")]
        public string InterestJson { get; set; }
    }

    public class OwnerRegisterDto : OwnerStudentRegisterDto
    {
        [Required]
        public string BankAccount { get; set; }

        [Required]
        public string DocumentNumber { get; set; }
    }
}
