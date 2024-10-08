﻿using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class PasswordResetDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
