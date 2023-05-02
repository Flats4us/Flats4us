using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Owner : OwnerStudent //not abstract
    {
        [Required]
        public string? BankAccount { get; set; }

        [Required]
        public string? TitleDeedPath { get; set; }

        public virtual ICollection<OwnerOpinion> IssuedRatings { get; set; }

        public virtual ICollection<StudentOpinion> ReceivedRatings { get; set; }

        public Owner()
        {
            IssuedRatings = new HashSet<OwnerOpinion>();
            ReceivedRatings = new HashSet<StudentOpinion>();
        }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().HasData(
            new Owner
            {
                Id = 1,
                Name = "Jan",
                Surname = "Kowalski",
                Address = "ul. Kolorowa 12",
                City = "Warszawa",
                PhoneNumber = "123456789",
                Email = "jan.kowalski@example.com",
                AccountCreationDate = new DateTime(2023, 3, 13),
                LastLoginDate = new DateTime(2023, 4, 29),
                Password = "password123",
                PhotoPath = "/photos/jan_kowalski.jpg",
                ActivityStatus = true,
                DocumentPath = "/documents/id_card.pdf",
                DocumentType = DocumentType.ID,
                VerificationStatus = VerificationStatus.Verified,
                DocumentExpireDate = new DateTime(2028, 4, 29),
                BankAccount = "12345678901234567890123456",
                TitleDeedPath = "/documents/title_deed.pdf"
            },
            new Owner
            {
                Id = 2,
                Name = "Anna",
                Surname = "Nowak",
                Address = "ul. Zielona 34",
                City = "Kraków",
                PhoneNumber = "987654321",
                Email = "anna.nowak@example.com",
                AccountCreationDate = new DateTime(2023, 3, 18),
                LastLoginDate = new DateTime(2023, 4, 28),
                Password = "password456",
                PhotoPath = "/photos/anna_nowak.jpg",
                ActivityStatus = false,
                DocumentPath = "/documents/passport.pdf",
                DocumentType = DocumentType.Passport,
                VerificationStatus = VerificationStatus.NotVerified,
                DocumentExpireDate = new DateTime(2027, 8, 25),
                BankAccount = "09876543210987654321098765",
                TitleDeedPath = "/documents/title_deed.pdf"
            },
            new Owner
            {
                Id = 3,
                Name = "Piotr",
                Surname = "Wójcik",
                Address = "ul. Czerwona 56",
                City = "Wrocław",
                PhoneNumber = "555666777",
                Email = "piotr.wojcik@example.com",
                AccountCreationDate = new DateTime(2023, 2, 14),
                LastLoginDate = new DateTime(2023, 4, 29),
                Password = "password789",
                PhotoPath = "/photos/piotr_wojcik.jpg",
                ActivityStatus = true,
                DocumentPath = "/documents/student_card.pdf",
                DocumentType = DocumentType.StudentCard,
                VerificationStatus = VerificationStatus.Verified,
                DocumentExpireDate = new DateTime(2024, 9, 29),
                BankAccount = "11122233344455566677788899",
                TitleDeedPath = "/documents/title_deed.pdf"
            });
        }
    }
}
