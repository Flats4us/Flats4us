using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public enum RoommatesStatus
    {
        Roommate,
        Alone
    }

    public class Tenant : Student //not abstract
    {
        [Required]
        public RoommatesStatus RoommatesStatus { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasData(
            new Tenant
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
                StudentNumber = "123456",
                RoommatesStatus = RoommatesStatus.Alone
            },
            new Tenant
            {
                Id = 2,
                Name = "Bob",
                Surname = "Johnson",
                Address = "456 Oak St",
                City = "San Francisco",
                PhoneNumber = "555-5678",
                Email = "bob.johnson@example.com",
                AccountCreationDate = DateTime.UtcNow.AddDays(-30),
                LastLoginDate = DateTime.UtcNow.AddDays(-1),
                Password = "password456",
                PhotoPath = "/path/to/photo.jpg",
                ActivityStatus = true,
                DocumentPath = "/path/to/document.pdf",
                DocumentType = DocumentType.ID,
                VerificationStatus = VerificationStatus.Verified,
                DocumentExpireDate = DateTime.UtcNow.AddYears(1),
                YearOfBirth = 2001,
                Age = 22,
                StudentNumber = "654321",
                RoommatesStatus = RoommatesStatus.Roommate
            },
            new Tenant
            {
                Id = 3,
                Name = "Charlie",
                Surname = "Brown",
                Address = "789 Maple Ave",
                City = "Chicago",
                PhoneNumber = "555-9012",
                Email = "charlie.brown@example.com",
                AccountCreationDate = DateTime.UtcNow.AddDays(-30),
                LastLoginDate = DateTime.UtcNow.AddDays(-1),
                Password = "password789",
                PhotoPath = "/path/to/photo.jpg",
                ActivityStatus = true,
                DocumentPath = "/path/to/document.pdf",
                DocumentType = DocumentType.Passport,
                VerificationStatus = VerificationStatus.NotVerified,
                DocumentExpireDate = DateTime.UtcNow.AddYears(1),
                YearOfBirth = 1999,
                Age = 24,
                StudentNumber = "789012",
                RoommatesStatus = RoommatesStatus.Roommate
            });
        }
    }
}
