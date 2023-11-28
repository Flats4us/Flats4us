﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("User")]
    public abstract class User
    {
        public const int MinUsernameLenght = 6;
        public const int MaxUsernameLenght = 30;
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

        [MaxLength(MaxUsernameLenght)]
        [MinLength(MinUsernameLenght)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [MinLength(8)]
        [MaxLength(70)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,50}$")]
        public string PasswordHash { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime AccountCreationDate { get; set; }

        [Required]
        public DateTime LastLoginDate { get; set; }
    }
}
