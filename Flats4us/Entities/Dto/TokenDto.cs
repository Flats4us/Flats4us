﻿using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class TokenDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public long ExpiresAt { get; set; }
    }
}
