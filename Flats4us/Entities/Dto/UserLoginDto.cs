﻿namespace Flats4us.Entities.Dto
{
    public class UserLoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? FcmToken { get; set; }
    }
}
