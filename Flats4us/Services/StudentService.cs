using Flats4us.Entities.Dto;
using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;
using Flats4us.Services.Interfaces;
using Flats4us.Helpers;

namespace Flats4us.Services
{
    public class StudentService : OwnerStudentService, IStudentService
    {
        public StudentService(Flats4usContext context) : base(context)
        {
        }

        protected override Student CreateUserFromDto(OwnerStudentRegisterDto request)
        {
            var imageFolder = Guid.NewGuid().ToString();
            var studentDto = request as StudentRegisterDto;
            if (studentDto == null) throw new ArgumentException("Invalid DTO for student registration");

            // Populate properties from OwnerStudent
            var student = new Student();
            student = (Student)PopulateOwnerStudentFieldsFromDto(student, studentDto);

            // Fields specific to Student (like Interests, Meetings etc.) can be populated here if they're part of the DTO
            

            student.YearOfBirth = studentDto.YearOfBirth;
            
            student.StudentNumber = studentDto.StudentNumber;
            student.University = studentDto.University;
            student.Facebook = studentDto.Facebook;
            student.Twitter = studentDto.Twitter;
            student.Instagram = studentDto.Instagram;
            student.RoommatesStatus = studentDto.RoommatesStatus;
            student.IsTenant = studentDto.IsTenant;
            student.ImagesPath = imageFolder;
            return student;
        }

        public async Task RegisterAsync(StudentRegisterDto request)
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

            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.ToString()); // This will print the main exception and any inner exception
                throw;  // rethrow the exception if needed
            }


        }

        async Task<IEnumerable<User>> IStudentService.GetAllUsersAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _context.Students.FindAsync(id);
            return user;
        }

        async Task<bool> IStudentService.DeleteUserAsync(int id)
        {
            var user = await _context.Students.FindAsync(id);
            if (user == null) return false;

            _context.Students.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }


    }

}
