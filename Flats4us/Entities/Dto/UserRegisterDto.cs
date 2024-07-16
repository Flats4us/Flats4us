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
        public string Address { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string? FcmToken { get; set; }
    }

    public class OwnerStudentRegisterDto : UserRegisterDto
    {
        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Date must be from future")]
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

        [Required]
        public List<string> Links { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Value must be between 1 and 10.")]
        public int Party { get; set; }

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

        [Required]
        public List<int> InterestIds { get; set; }
    }

    public class OwnerRegisterDto : OwnerStudentRegisterDto
    {
        [Required]
        public string BankAccount { get; set; }

        [Required]
        public string DocumentNumber { get; set; }
    }

    public class UserFilesDto
    {
        public IFormFile? ProfilePicture { get; set; }
         
        public IFormFile? Document { get; set; }
    }
}
