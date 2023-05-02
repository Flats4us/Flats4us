using Microsoft.EntityFrameworkCore;

namespace Flats4us.Entities
{
    public class Seeker : Student //not abstract
    {
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seeker>().HasData(
            new Seeker
            {
                Id = 1,
                Name = "Alice",
                Surname = "Smith",
                Address = "123 Main St",
                City = "New York",
                PhoneNumber = "555-1234",
                Email = "alice.smith@example.com",
                AccountCreationDate = DateTime.UtcNow.AddDays(-30),
                LastLoginDate = DateTime.UtcNow.AddDays(-1),
                Password = "password123",
                PhotoPath = "/path/to/photo.jpg",
                ActivityStatus = true,
                DocumentPath = "/path/to/document.pdf",
                DocumentType = DocumentType.StudentCard,
                VerificationStatus = VerificationStatus.Verified,
                DocumentExpireDate = DateTime.UtcNow.AddYears(1),
                YearOfBirth = 2000,
                Age = 23,
                StudentNumber = "123456"
            },
            new Seeker
            {
                Id = 2,
                Name = "Bob",
                Surname = "Jones",
                Address = "456 Elm St",
                City = "Los Angeles",
                PhoneNumber = "555-5678",
                Email = "bob.jones@example.com",
                AccountCreationDate = DateTime.UtcNow.AddDays(-60),
                LastLoginDate = DateTime.UtcNow.AddDays(-10),
                Password = "password456",
                PhotoPath = "/path/to/photo2.jpg",
                ActivityStatus = true,
                DocumentPath = "/path/to/document2.pdf",
                DocumentType = DocumentType.DriverLicense,
                VerificationStatus = VerificationStatus.Pending,
                DocumentExpireDate = DateTime.UtcNow.AddYears(2),
                YearOfBirth = 1995,
                Age = 28,
                StudentNumber = "789012"
            },
            new Seeker
            {
                Id = 3,
                Name = "Carol",
                Surname = "Johnson",
                Address = "789 Oak St",
                City = "Chicago",
                PhoneNumber = "555-9012",
                Email = "carol.johnson@example.com",
                AccountCreationDate = DateTime.UtcNow.AddDays(-15),
                LastLoginDate = DateTime.UtcNow.AddDays(-2),
                Password = "password789",
                PhotoPath = "/path/to/photo3.jpg",
                ActivityStatus = true,
                DocumentPath = "/path/to/document3.pdf",
                DocumentType = DocumentType.Passport,
                VerificationStatus = VerificationStatus.Verified,
                DocumentExpireDate = DateTime.UtcNow.AddYears(5),
                YearOfBirth = 1998,
                Age = 25,
                StudentNumber = "345678"
            });
        }
    }
}
