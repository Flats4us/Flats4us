﻿namespace Flats4us.Entities.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
