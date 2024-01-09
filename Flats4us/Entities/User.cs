﻿using Flats4us.Helpers.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("User")]
    public abstract class User
    {
        public const int MinPasswordLenght = 6;
        public const int MaxPasswordeLenght = 30;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[MinLength(8)]
        //[MaxLength(50)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,50}$")]
        public string PasswordHash { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime AccountCreationDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        [Required]
        public bool ActivityStatus { get; set; }

        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        public virtual ICollection<TechnicalProblem> TechnicalProblems { get; set; }

    }
}
