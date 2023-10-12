﻿using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class UserService : IUserService
    {
        public readonly Flats4usContext _context;

        public UserService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string passwordHash)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(passwordHash, user.PasswordHash))
            {
                return null;
            }

            return user;
        }





        public async Task<User> RegisterAsync(UserRegisterDto request)
        {
            try
            {
                // Verify that the requested username does not already exist in the database
                var existingUser = await _context.Users.SingleOrDefaultAsync(x => x.Username == request.Username);
                if (existingUser != null)
                {
                    throw new Exception("Username already exists");
                }

                // Verify that the username and password meet the length requirements
                if (request.Username.Length < User.MinUsernameLenght || request.Username.Length > User.MaxUsernameLenght)
                {
                    throw new Exception($"Username must be between {User.MinUsernameLenght} and {User.MaxUsernameLenght} characters");
                }
                if (request.Password.Length < 8 || request.Password.Length > 50)
                {
                    throw new Exception("Password must be between 8 and 50 characters");
                }

                // Verify that the password contains at least one uppercase letter, one lowercase letter, and one digit
                if (!request.Password.Any(char.IsUpper) || !request.Password.Any(char.IsLower) || !request.Password.Any(char.IsDigit))
                {
                    throw new Exception("Password must contain at least one uppercase letter, one lowercase letter, and one digit");
                }

                // Hash the password
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                // Create a new user object
                User user = new User()
                {
                    Username = request.Username,
                    PasswordHash = passwordHash,
                    Role = request.Role,
                    AccountCreationDate = DateTime.UtcNow,  
                    LastLoginDate = DateTime.UtcNow,  

                    // Map additional fields from DTO
                    Name = request.Name,
                    Surname = request.Surname,
                    Street = request.Street,
                    Number = request.Number,
                    Flat = request.Flat,  
                    City = request.City,
                    PostalCode = request.PostalCode,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                };

                // Add the user to the database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.ToString()); // This will print the main exception and any inner exception
                throw;  // rethrow the exception if needed
            }
        }


        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }


        async Task<IEnumerable<User>> IUserService.GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        async Task<bool> IUserService.DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
