using Flats4us.Entities.Dto;
using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;
using Flats4us.Services.Interfaces;
using Flats4us.Helpers;
using AutoMapper;

namespace Flats4us.Services
{
    public class OwnerService : OwnerStudentService, IOwnerService
    {
        public OwnerService(Flats4usContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override Owner CreateUserFromDto(OwnerStudentRegisterDto request)
        {
            var imageFolder = Guid.NewGuid().ToString();

            var ownerDto = request as OwnerRegisterDto; // Assuming you have a specific DTO for Owner registration.
            if (ownerDto == null) throw new ArgumentException("Invalid DTO for owner registration");

            var owner = new Owner();
            PopulateOwnerStudentFieldsFromDto(owner, ownerDto); // This will populate the fields common to OwnerStudent.

            // Fields specific to Owner can be populated here if they're part of the DTO
            owner.BankAccount = ownerDto.BankAccount;
            owner.ImagesPath = imageFolder;

            // ... other Owner-specific fields ...

            return owner;
        }

        public async Task<User> RegisterAsync(OwnerRegisterDto request)
        {
            try
            {
                // Verify that the requested email does not already exist in the database
                var existingUser = await _context.Users.SingleOrDefaultAsync(x => x.Email == request.Email);
                if (existingUser != null)
                {
                    throw new Exception("Email already exists");
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
                OwnerStudent user = CreateUserFromDto(request);

                var imageFolder = user.ImagesPath;

                if (request.ProfilePicture != null && request.ProfilePicture.Length > 0)
                {
                    await ImageUtility.ProcessAndSaveImage(request.ProfilePicture, $"Images/Users/{imageFolder}/ProfilePicture");
                }
                if (request.Document != null && request.Document.Length > 0)
                {
                    await ImageUtility.ProcessAndSaveImage(request.Document, $"Images/Users/{imageFolder}/Document");
                }

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

        async Task<IEnumerable<User>> IOwnerService.GetAllUsersAsync()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _context.Owners.FindAsync(id);
            return user;
        }

        async Task<bool> IOwnerService.DeleteUserAsync(int id)
        {
            var user = await _context.Owners.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
