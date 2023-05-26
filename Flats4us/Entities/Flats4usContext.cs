using Flats4us.Helpers.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;


namespace Flats4us.Entities
{
    public class Flats4usContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SurveyStudent> SurveyStudents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<OpinionStudentStudent> StudentStudentOpinions { get; set; }
        public DbSet<OpinionOwnerStudent> OwnerStudentOpinions { get; set; }

        public Flats4usContext() { }

        public Flats4usContext(DbContextOptions options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OpinionStudentStudent>()
                .HasOne(x => x.Evaluated)
                .WithMany(x => x.ReceivedStudentStudentOpinions)
                .HasForeignKey(x => x.EvaluatedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpinionStudentStudent>()
                .HasOne(x => x.Evaluator)
                .WithMany(x => x.IssuedStudentStudentOpinions)
                .HasForeignKey(x => x.EvaluatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpinionOwnerStudent>()
                .HasOne(x => x.Evaluated)
                .WithMany(x => x.ReceivedOwnertStudentOpinions)
                .HasForeignKey(x => x.EvaluatedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpinionOwnerStudent>()
                .HasOne(x => x.Evaluator)
                .WithMany(x => x.IssuedOwnerStudentOpinions)
                .HasForeignKey(x => x.EvaluatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpinionStudentOwner>()
                .HasOne(x => x.Evaluator)
                .WithMany(x => x.IssuedStudentOwnerOpinions)
                .HasForeignKey(x => x.EvaluatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpinionStudentOwner>()
                .HasOne(x => x.Evaluated)
                .WithMany(x => x.ReceivedStudentOwnerOpinions)
                .HasForeignKey(x => x.EvaluatedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Moderator>().HasData(
               new //Moderator
               {
                   UserId = 1,
                   Name = "Maciej",
                   Surname = "Kowalski",
                   Street = "Długa",
                   Number = 47,
                   Flat = 2,
                   City = "Warszawa",
                   PostalCode = "00-000",
                   Email = "maciej.kowalski@gmail.com",
                   PhoneNumber = "456736829",
                   AccountCreationDate = new DateTime(2023, 5, 10),
                   LastLoginDate = new DateTime(2023, 5, 21),
                   Password = "mkowalski123",
                   HireDate = new DateTime(2023, 5, 10)
               },
               new //Moderator
               {
                   UserId = 2,
                   Name = "Katarzyna",
                   Surname = "Nowak",
                   Street = "Kwiatowa",
                   Number = 10,
                   Flat = 3,
                   City = "Gdańsk",
                   PostalCode = "80-000",
                   Email = "katarzyna.nowak@gmail.com",
                   PhoneNumber = "512345678",
                   AccountCreationDate = new DateTime(2023, 5, 16),
                   LastLoginDate = new DateTime(2023, 5, 20),
                   Password = "knowak123",
                   HireDate = new DateTime(2023, 5, 16)
               },
               new //Moderator
               {
                   UserId = 3,
                   Name = "Adam",
                   Surname = "Kowalczyk",
                   Street = "Słoneczna",
                   Number = 20,
                   Flat = 6,
                   City = "Kraków",
                   PostalCode = "30-001",
                   Email = "adam.kowalczyk@gmail.com",
                   PhoneNumber = "601234567",
                   AccountCreationDate = new DateTime(2023, 5, 14),
                   LastLoginDate = new DateTime(2023, 5, 19),
                   Password = "akowalczyk789",
                   HireDate = new DateTime(2023, 5, 14)
               },
               new //Moderator
               {
                   UserId = 4,
                   Name = "Magdalena",
                   Surname = "Wójcik",
                   Street = "Ogrodowa",
                   Number = 15,
                   Flat = 2,
                   City = "Warszawa",
                   PostalCode = "02-000",
                   Email = "magdalena.wojcik@gmail.com",
                   PhoneNumber = "712345678",
                   AccountCreationDate = new DateTime(2023, 5, 18),
                   LastLoginDate = new DateTime(2023, 5, 20),
                   Password = "mwojcik456",
                   HireDate = new DateTime(2023, 5, 18)
               }
            );

            modelBuilder.Entity<Seeker>().HasData(
               new //Seeker
               {
                   UserId = 5,
                   Name = "Filip",
                   Surname = "Dobrej",
                   Street = "Krótka",
                   Number = 7,
                   Flat = 3,
                   City = "Warszawa",
                   PostalCode = "00-000",
                   Email = "filip.dobrej@gmail.com",
                   PhoneNumber = "456736829",
                   AccountCreationDate = new DateTime(2023, 5, 1),
                   LastLoginDate = new DateTime(2023, 5, 17),
                   Password = "fdobrej123",
                   PhotoPath = "/profile/1",
                   ActivityStatus = false,
                   DocumentPath = "/documents/1",
                   DocumentType = DocumentType.StudentCard,
                   VerificationStatus = VerificationStatus.Verified,
                   DocumentExpireDate = new DateTime(2024, 5, 21),
                   YearOfBirth = 2001,
                   Age = DateTime.Now.Year - 2001,
                   StudentNumber = "s22023",
                   University = "PJATK",
                   Facebook = "",
                   Twitter = "",
                   Instagram = "",
               },
               new
               {
                   UserId = 6,
                   Name = "Alicja",
                   Surname = "Dąbrowska",
                   Street = "Kościuszki",
                   Number = 9,
                   Flat = 4,
                   City = "Poznań",
                   PostalCode = "60-001",
                   Email = "alicja.dabrowska@gmail.com",
                   PhoneNumber = "601234567",
                   AccountCreationDate = new DateTime(2022, 6, 30),
                   LastLoginDate = new DateTime(2023, 5, 20),
                   Password = "adabrowska789",
                   PhotoPath = "/profile/2",
                   ActivityStatus = true,
                   DocumentPath = "/documents/2",
                   DocumentType = DocumentType.Passport,
                   VerificationStatus = VerificationStatus.Verified,
                   DocumentExpireDate = new DateTime(2025, 6, 30),
                   YearOfBirth = 1995,
                   Age = DateTime.Now.Year - 1995,
                   StudentNumber = "s23423",
                   University = "Uniwersytet Warszawski",
                   Facebook = "https://www.facebook.com/alicja.dabrowska",
                   Twitter = "",
                   Instagram = "https://www.instagram.com/alicja_dabrowska/"
               },
               new
               {
                   UserId = 7,
                   Name = "Dominik",
                   Surname = "Kowalczyk",
                   Street = "Szkolna",
                   Number = 12,
                   Flat = 1,
                   City = "Kraków",
                   PostalCode = "30-002",
                   Email = "dominik.kowalczyk@gmail.com",
                   PhoneNumber = "712345678",
                   AccountCreationDate = new DateTime(2023, 5, 5),
                   LastLoginDate = new DateTime(2023, 5, 19),
                   Password = "dkowalczyk456",
                   PhotoPath = "/profile/3",
                   ActivityStatus = true,
                   DocumentPath = "/documents/3",
                   DocumentType = DocumentType.ID,
                   VerificationStatus = VerificationStatus.Verified,
                   DocumentExpireDate = new DateTime(2024, 5, 31),
                   YearOfBirth = 1998,
                   Age = DateTime.Now.Year - 1998,
                   StudentNumber = "s22345",
                   University = "AGH University of Science and Technology",
                   Facebook = "https://www.facebook.com/dominik.kowalczyk",
                   Twitter = "",
                   Instagram = "https://www.instagram.com/dominik_kowalczyk/"
               },
               new
               {
                   UserId = 8,
                   Name = "Natalia",
                   Surname = "Wójcik",
                   Street = "Kwiatowa",
                   Number = 5,
                   Flat = 2,
                   City = "Warszawa",
                   PostalCode = "02-001",
                   Email = "natalia.wojcik@gmail.com",
                   PhoneNumber = "601234567",
                   AccountCreationDate = new DateTime(2023, 5, 8),
                   LastLoginDate = new DateTime(2023, 5, 18),
                   Password = "nwojcik789",
                   PhotoPath = "/profile/4",
                   ActivityStatus = true,
                   DocumentPath = "/documents/4",
                   DocumentType = DocumentType.Passport,
                   VerificationStatus = VerificationStatus.Verified,
                   DocumentExpireDate = new DateTime(2025, 5, 8),
                   YearOfBirth = 2000,
                   Age = DateTime.Now.Year - 2000,
                   StudentNumber = "s22890",
                   University = "University of Warsaw",
                   Facebook = "",
                   Twitter = "https://twitter.com/nwojcik",
                   Instagram = "https://www.instagram.com/natalia_wojcik/"
               }
            );


            //.UsingEntity(j => j
            //    .ToTable("UserTechnology")
            //    .HasData(new[]
            //    {
            //        { UsersID = 1, TechnologiesID = 1 },
            //        { UsersID = 1, TechnologiesID = 2 }
            //    }
            //));

            modelBuilder.Entity<Interest>().HasData(
                new { InterestId = 1, Name = "Sailing" },
                new { InterestId = 2, Name = "Painting" },
                new { InterestId = 3, Name = "Photography" },
                new { InterestId = 4, Name = "Cooking" },
                new { InterestId = 5, Name = "Gardening" },
                new { InterestId = 6, Name = "Playing Guitar" },
                new { InterestId = 7, Name = "Reading Books" },
                new { InterestId = 8, Name = "Hiking" },
                new { InterestId = 9, Name = "Dancing" },
                new { InterestId = 10, Name = "Yoga" }
            );


            //modelBuilder.Entity<Property>()
            //    .HasDiscriminator()
            //    .IsComplete();

            //modelBuilder.Entity<StudentOpinion>()
            //    .HasOne(x => x.Evaluated)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<StudentOpinion>()
            //    .HasOne(x => x.Evaluator)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<OwnerOpinion>()
            //    .HasOne(x => x.Evaluated)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<OwnerOpinion>()
            //    .HasOne(x => x.Evaluator)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<Advertisement>().HasData(
            //    new //Advertisement
            //    {
            //        Id = 1,
            //        Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
            //        Price = 100,
            //        EndDate = DateTime.Parse("2023-05-15"),
            //        ModeratorId = 1
            //    },
            //    new //Advertisement
            //    {
            //        Id = 2,
            //        Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
            //        Price = 250,
            //        EndDate = DateTime.Parse("2023-05-20"),
            //        ModeratorId = 2
            //    },
            //    new //Advertisement
            //    {
            //        Id = 3,
            //        Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
            //        Price = 470,
            //        EndDate = DateTime.Parse("2023-06-29"),
            //        ModeratorId = 3
            //    }
            // );

            // modelBuilder.Entity<Argument>().HasData(
            //     new //Argument
            //     {
            //         Id = 1,
            //         StartDate = new DateTime(2023, 05, 01),
            //         OwnerAcceptanceDate = new DateTime(2023, 05, 02),
            //         TenantAcceptanceDate = new DateTime(2023, 05, 03),
            //         ArgumentStatus = ArgumentStatus.Ongoing,
            //         ModeratorDecisionDate = new DateTime(2023, 05, 04),
            //         OfferId = 1,
            //         InterventionId = 1
            //     },
            //     new //Argument
            //     {
            //         Id = 2,
            //         StartDate = new DateTime(2023, 05, 02),
            //         OwnerAcceptanceDate = new DateTime(2023, 05, 03),
            //         TenantAcceptanceDate = new DateTime(2023, 05, 04),
            //         ArgumentStatus = ArgumentStatus.Resolved,
            //         ModeratorDecisionDate = new DateTime(2023, 05, 05),
            //         OfferId = 2,
            //         InterventionId = 2
            //     },
            //     new //Argument
            //     {
            //         Id = 3,
            //         StartDate = new DateTime(2023, 05, 03),
            //         OwnerAcceptanceDate = new DateTime(2023, 05, 04),
            //         TenantAcceptanceDate = new DateTime(2023, 05, 05),
            //         ArgumentStatus = ArgumentStatus.ResolvedByMod,
            //         ModeratorDecisionDate = new DateTime(2023, 05, 06),
            //         OfferId = 3,
            //         InterventionId = 3
            //     }
            // );

            // modelBuilder.Entity<ArgumentMessage>().HasData(
            //     new //ArgumentMessage
            //     {
            //         Id = 1,
            //         Date = DateTime.Now,
            //         Content = "First argument message",
            //         SenderId=1
            //     },
            //     new //ArgumentMessage
            //     {
            //         Id = 2,
            //         Date = DateTime.Now.AddDays(-1),
            //         Content = "Second argument message",
            //         SenderId=2
            //     },
            //     new //ArgumentMessage
            //     {
            //         Id = 3,
            //         Date = DateTime.Now.AddDays(-2),
            //         Content = "Third argument message",
            //         SenderId=3
            //     }
            // );

            // modelBuilder.Entity<Equipment>().HasData(
            //     new //Equipment
            //     { Id = 1, Name = "Laptop" },
            //     new //Equipment 
            //     { Id = 2, Name = "Projector" },
            //     new //Equipment 
            //     { Id = 3, Name = "Microphone" }
            // );

            // modelBuilder.Entity<Flat>().HasData(
            //     new //Flat
            //     {
            //         Id = 1,
            //         Address = "ul. Wiejska 1, Warszawa",
            //         Surface = 70,
            //         MaxInhabitants = 2,
            //         NumberOfRooms = 1
            //     },
            //     new //Flat
            //     {
            //         Id = 2,
            //         Address = "ul. Wrocławska 12, Kraków",
            //         Surface = 120,
            //         MaxInhabitants = 4,
            //         NumberOfRooms = 1
            //     },
            //     new //Flat
            //     {
            //         Id = 3,
            //         Address ="ul. Kościuszki 50, Gdańsk",
            //         Surface = 90,
            //         MaxInhabitants = 3,
            //         NumberOfRooms = 1
            //     }
            // );

            // modelBuilder.Entity<House>().HasData(
            //     new //House
            //     {
            //         Id = 4,
            //         Address = "ul. Mickiewicza 20, Kraków",
            //         Surface = 150,
            //         MaxInhabitants = 6,
            //         NumberOfRooms = 5,
            //         NumberOfFloors = 2,
            //         ParcelSurface = 500
            //     },
            //     new //House
            //     {
            //         Id = 5,
            //         Address = "ul. Długa 15, Gdańsk",
            //         Surface = 100,
            //         MaxInhabitants = 4,
            //         NumberOfRooms = 4,
            //         NumberOfFloors = 1,
            //         ParcelSurface = 300
            //     },
            //     new //House
            //     {
            //         Id = 6,
            //         Address = "ul. Jagiellońska 10, Warszawa",
            //         Surface = 200,
            //         MaxInhabitants = 8,
            //         NumberOfRooms = 6,
            //         NumberOfFloors = 3,
            //         ParcelSurface = 700
            //     }
            // );

            // modelBuilder.Entity<Intervention>().HasData(
            //     new //Intervention
            //     {
            //         Id = 1,
            //         Description = "Intervention 1",
            //         Date = new DateTime(2023, 04, 12)
            //     },
            //     new //Intervention
            //     {
            //         Id = 2,
            //         Description = "Intervention 2",
            //         Date = new DateTime(2023, 05, 01)
            //     },
            //     new //Intervention
            //     {
            //         Id = 3,
            //         Description = "Intervention 3",
            //         Date = new DateTime(2023, 01, 20)
            //     }
            // );

            // modelBuilder.Entity<Meeting>().HasData(
            //     new //Meeting
            //     {
            //         Id = 1,
            //         Date = DateTime.Now,
            //         Address = "First meeting address",
            //         Reason = "First meeting reason",
            //         OfferId=1
            //     },
            //     new //Meeting
            //     {
            //         Id = 2,
            //         Date = DateTime.Now.AddDays(1),
            //         Address = "Second meeting address",
            //         Reason = "Second meeting reason",
            //         OfferId=2
            //     },
            //     new //Meeting
            //     {
            //         Id = 3,
            //         Date = DateTime.Now.AddDays(2),
            //         Address = "Third meeting address",
            //         Reason = "Third meeting reason",
            //         OfferId=3
            //     }
            // );

            // modelBuilder.Entity<Moderator>().HasData(
            //    new //Moderator
            //    {
            //        Id = 1,
            //        Name = "Katarzyna",
            //        Surname = "Nowak",
            //        Address = "ul. Wąwozowa 25",
            //        City = "Gdańsk",
            //        PhoneNumber = "654-123-789",
            //        Email = "knowak@example.com",
            //        AccountCreationDate = new DateTime(2023, 4, 10),
            //        LastLoginDate = new DateTime(2023, 4, 29),
            //        Password = "password123",
            //        HireDate = DateTime.Parse("2023-04-10"),
            //        Department = Department.dept1
            //    },
            //    new //Moderator
            //    {
            //        Id = 2,
            //        Name = "Piotr",
            //        Surname = "Kowalski",
            //        Address = "Ul. Zaruby 13",
            //        City = "Warszawa",
            //        PhoneNumber = "486-654-321",
            //        Email = "pkowalski@example.com",
            //        AccountCreationDate = new DateTime(2023, 3, 15),
            //        LastLoginDate = new DateTime(2023, 4, 24),
            //        Password = "password456",
            //        HireDate = new DateTime(2023, 2, 25),
            //        Department = Department.dept2
            //    },
            //    new //Moderator
            //    {
            //        Id = 3,
            //        Name = "Anna",
            //        Surname = "Woźniak",
            //        Address = "ul. Kiepury 98",
            //        City = "Kraków",
            //        PhoneNumber = "741-568-892",
            //        Email = "awozniak@example.com",
            //        AccountCreationDate = DateTime.Parse("2023-01-15"),
            //        LastLoginDate = DateTime.Parse("2023-04-22"),
            //        Password = "password789",
            //        HireDate = DateTime.Parse("2023-01-14"),
            //        Department = Department.dept1
            //    }
            //);

            // modelBuilder.Entity<Offer>().HasData(
            //     new //Offer
            //     {
            //         Id = 1,
            //         PropertyId = 4,
            //         OwnerId = 1,
            //         Date = DateTime.Now,
            //         OfferStatus = OfferStatus.Current,
            //         Price = 1000,
            //         Description = "First offer",
            //         RentalPeriod = 12,
            //         NumberOfIntrested = 5,
            //         Regulations = "First offer regulations"
            //     },
            //     new //Offer
            //     {
            //         Id = 2,
            //         PropertyId = 3,
            //         OwnerId = 1,
            //         Date = DateTime.Now.AddDays(-1),
            //         OfferStatus = OfferStatus.Current,
            //         Price = 2000,
            //         Description = "Second offer",
            //         RentalPeriod = 6,
            //         NumberOfIntrested = 10,
            //         Regulations = "Second offer regulations",
            //     },
            //     new //Offer
            //     {
            //         Id = 3,
            //         PropertyId = 2,
            //         OwnerId = 2,
            //         Date = DateTime.Now.AddDays(-2),
            //         OfferStatus = OfferStatus.Outdated,
            //         Price = 1500,
            //         Description = "Third offer",
            //         RentalPeriod = 9,
            //         NumberOfIntrested = 3,
            //         Regulations = "Third offer regulations",
            //     },
            //     new //Offer
            //     {
            //         Id = 4,
            //         PropertyId = 1,
            //         OwnerId = 3,
            //         Date = DateTime.Now.AddDays(-2),
            //         OfferStatus = OfferStatus.Current,
            //         Price = 1500,
            //         Description = "Third offer",
            //         RentalPeriod = 9,
            //         NumberOfIntrested = 3,
            //         Regulations = "Third offer regulations"
            //     }
            // );

            // modelBuilder.Entity<OwnerSurvey>().HasData(
            //     new
            //     {
            //         OfferId = 1,
            //         OwnerId = 1,
            //         MaxNumberOfOccupants = 2,
            //         Smoking = false,
            //         Parties = true,
            //         Animals = false,
            //         OccupantGender = Gender.Both
            //     },
            //     new
            //     {
            //         OfferId = 2,
            //         OwnerId = 1,
            //         MaxNumberOfOccupants = 3,
            //         Smoking = true,
            //         Parties = true,
            //         Animals = false,
            //         OccupantGender = Gender.Male
            //     },
            //     new
            //     {
            //         OfferId = 3,
            //         OwnerId = 2,
            //         MaxNumberOfOccupants = 2,
            //         Smoking = false,
            //         Parties = true,
            //         Animals = false,
            //         OccupantGender = Gender.Both
            //     },
            //     new
            //     {
            //         OfferId = 4,
            //         OwnerId = 3,
            //         MaxNumberOfOccupants = 1,
            //         Smoking = false,
            //         Parties = true,
            //         Animals = false,
            //         OccupantGender = Gender.Female
            //     }
            // );


            // modelBuilder.Entity<OfferInterest>().HasData(
            //     new //OfferInterest
            //     {
            //         Id = 1,
            //         OfferId = 1,
            //         SeekerId = 1,
            //         Date = DateTime.UtcNow.AddDays(-5)
            //     },
            //     new //OfferInterest
            //     {
            //         Id = 2,
            //         OfferId = 1,
            //         SeekerId = 2,
            //         Date = DateTime.UtcNow.AddDays(-2)
            //     },
            //     new //OfferInterest
            //     {
            //         Id = 3,
            //         OfferId = 2,
            //         SeekerId = 3,
            //         Date = DateTime.UtcNow.AddDays(-1)
            //     }
            // );

            // modelBuilder.Entity<Owner>().HasData(
            //     new //Owner
            //     {
            //         Id = 10,
            //         Name = "Jan",
            //         Surname = "Kowalski",
            //         Address = "ul. Kolorowa 12",
            //         City = "Warszawa",
            //         PhoneNumber = "123456789",
            //         Email = "jan.kowalski@example.com",
            //         AccountCreationDate = new DateTime(2023, 3, 13),
            //         LastLoginDate = new DateTime(2023, 4, 29),
            //         Password = "password123",
            //         PhotoPath = "/photos/jan_kowalski.jpg",
            //         ActivityStatus = true,
            //         DocumentPath = "/documents/id_card.pdf",
            //         DocumentType = DocumentType.ID,
            //         VerificationStatus = VerificationStatus.Verified,
            //         DocumentExpireDate = new DateTime(2028, 4, 29),
            //         BankAccount = "12345678901234567890123456",
            //         TitleDeedPath = "/documents/title_deed.pdf"
            //     },
            //     new //Owner
            //     {
            //         Id = 11,
            //         Name = "Anna",
            //         Surname = "Nowak",
            //         Address = "ul. Zielona 34",
            //         City = "Kraków",
            //         PhoneNumber = "987654321",
            //         Email = "anna.nowak@example.com",
            //         AccountCreationDate = new DateTime(2023, 3, 18),
            //         LastLoginDate = new DateTime(2023, 4, 28),
            //         Password = "password456",
            //         PhotoPath = "/photos/anna_nowak.jpg",
            //         ActivityStatus = false,
            //         DocumentPath = "/documents/passport.pdf",
            //         DocumentType = DocumentType.Passport,
            //         VerificationStatus = VerificationStatus.NotVerified,
            //         DocumentExpireDate = new DateTime(2027, 8, 25),
            //         BankAccount = "09876543210987654321098765",
            //         TitleDeedPath = "/documents/title_deed.pdf"
            //     },
            //     new //Owner
            //     {
            //         Id = 12,
            //         Name = "Piotr",
            //         Surname = "Wójcik",
            //         Address = "ul. Czerwona 56",
            //         City = "Wrocław",
            //         PhoneNumber = "555666777",
            //         Email = "piotr.wojcik@example.com",
            //         AccountCreationDate = new DateTime(2023, 2, 14),
            //         LastLoginDate = new DateTime(2023, 4, 29),
            //         Password = "password789",
            //         PhotoPath = "/photos/piotr_wojcik.jpg",
            //         ActivityStatus = true,
            //         DocumentPath = "/documents/student_card.pdf",
            //         DocumentType = DocumentType.StudentCard,
            //         VerificationStatus = VerificationStatus.Verified,
            //         DocumentExpireDate = new DateTime(2024, 9, 29),
            //         BankAccount = "11122233344455566677788899",
            //         TitleDeedPath = "/documents/title_deed.pdf"
            //     }
            // );

            // modelBuilder.Entity<Payment>().HasData(
            //     new //Payment
            //     { Id = 1, PaymentPurpose = PaymentPurpose.Rent, Price = 1500, OfferId = 1, StudentId = 4 },
            //     new //Payment
            //     { Id = 2, PaymentPurpose = PaymentPurpose.Deposit, Price = 2000, OfferId = 2, StudentId = 5 },
            //     new //Payment
            //     { Id = 3, PaymentPurpose = PaymentPurpose.Rent, Price = 1200, OfferId = 1, StudentId = 4 },
            //     new //Payment
            //     { Id = 4, PaymentPurpose = PaymentPurpose.Repairs, Price = 500, OfferId = 2, StudentId = 5 },
            //     new //Payment
            //     { Id = 5, PaymentPurpose = PaymentPurpose.Rent, Price = 1000, OfferId = 3, StudentId = 6 },
            //     new //Payment 
            //     { Id = 6, PaymentPurpose = PaymentPurpose.Deposit, Price = 1500, OfferId = 3, StudentId = 6 },
            //     new //Payment 
            //     { Id = 7, PaymentPurpose = PaymentPurpose.Rent, Price = 1000, OfferId = 1, StudentId = 4 },
            //     new //Payment 
            //     { Id = 8, PaymentPurpose = PaymentPurpose.Deposit, Price = 500, OfferId = 2, StudentId = 5 },
            //     new //Payment 
            //     { Id = 9, PaymentPurpose = PaymentPurpose.Repairs, Price = 250, OfferId = 1, StudentId = 4 }
            // );

            // modelBuilder.Entity<Promotion>().HasData(
            //     new //Promotion
            //     {
            //         Id = 1,
            //         StartDate = DateTime.Parse("2023-05-01"),
            //         EndDate = DateTime.Parse("2023-05-07"),
            //         Price = 50,
            //         PromotionType = PromotionType.type1,
            //         OfferId = 1
            //     },
            //     new //Promotion
            //     {
            //         Id = 2,
            //         StartDate = DateTime.Parse("2023-05-03"),
            //         EndDate = DateTime.Parse("2023-05-10"),
            //         Price = 75,
            //         PromotionType = PromotionType.type2,
            //         OfferId = 2
            //     },
            //     new //Promotion
            //     {
            //         Id = 3,
            //         StartDate = DateTime.Parse("2023-05-05"),
            //         EndDate = DateTime.Parse("2023-05-12"),
            //         Price = 100,
            //         PromotionType = PromotionType.type1,
            //         OfferId =3
            //     },
            //     new //Promotion
            //     {
            //         Id = 4,
            //         StartDate = DateTime.Parse("2023-05-08"),
            //         EndDate = DateTime.Parse("2023-05-15"),
            //         Price = 85,
            //         PromotionType = PromotionType.type2,
            //         OfferId =1
            //     },
            //     new //Promotion
            //     {
            //         Id = 5,
            //         StartDate = DateTime.Parse("2023-05-11"),
            //         EndDate = DateTime.Parse("2023-05-18"),
            //         Price = 60,
            //         PromotionType = PromotionType.type1,
            //         OfferId =2
            //     },
            //     new //Promotion
            //     {
            //         Id = 6,
            //         StartDate = DateTime.Parse("2023-05-14"),
            //         EndDate = DateTime.Parse("2023-05-21"),
            //         Price = 90,
            //         PromotionType = PromotionType.type2,
            //         OfferId =3
            //     },
            //     new //Promotion
            //     {
            //         Id = 7,
            //         StartDate = DateTime.Parse("2023-05-17"),
            //         EndDate = DateTime.Parse("2023-05-24"),
            //         Price = 120,
            //         PromotionType = PromotionType.type1,
            //         OfferId =1
            //     },
            //     new //Promotion
            //     {
            //         Id = 8,
            //         StartDate = DateTime.Parse("2023-05-20"),
            //         EndDate = DateTime.Parse("2023-05-27"),
            //         Price = 70,
            //         PromotionType = PromotionType.type2,
            //         OfferId =2
            //     },
            //     new //Promotion
            //     {
            //         Id = 9,
            //         StartDate = DateTime.Parse("2023-05-23"),
            //         EndDate = DateTime.Parse("2023-05-30"),
            //         Price = 95,
            //         PromotionType = PromotionType.type1,
            //         OfferId =3
            //     },
            //     new //Promotion
            //     {
            //         Id = 10,
            //         StartDate = DateTime.Parse("2023-05-26"),
            //         EndDate = DateTime.Parse("2023-06-02"),
            //         Price = 80,
            //         PromotionType = PromotionType.type2,
            //         OfferId =1
            //     }
            // );

            // modelBuilder.Entity<PropertyEquipment>().HasData(
            //     new //PropertyEquipment
            //     {
            //         Id = 1,
            //         PropertyId = 1,
            //         EquipmentId = 1
            //     },
            //     new //PropertyEquipment
            //     {
            //         Id = 2,
            //         PropertyId = 2,
            //         EquipmentId = 2
            //     },
            //     new //PropertyEquipment
            //     {
            //         Id = 3,
            //         PropertyId = 3,
            //         EquipmentId = 3
            //     }
            // );

            // modelBuilder.Entity<PropertyImage>().HasData(
            // new //PropertyImage
            //     { Id = 1, Title = "Living room", ImagePath = "images/livingroom.jpg", PropertyId = 1111 },
            //     new //PropertyImage
            //     { Id = 2, Title = "Kitchen", ImagePath = "images/kitchen.jpg", PropertyId = 2111 },
            //     new //PropertyImage
            //     { Id = 3, Title = "Bedroom", ImagePath = "images/bedroom.jpg", PropertyId = 3111 }
            // );

            // modelBuilder.Entity<Rent>().HasData(
            //     new //Rent
            //     {
            //         Id = 1,
            //         StartDate = new DateTime(2022, 1, 1),
            //         LengthInMonths = 12,
            //         ContractInformations = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
            //         OfferId = 1,
            //         TenantId = 1
            //     },
            //     new //Rent
            //     {
            //         Id = 2,
            //         StartDate = new DateTime(2022, 2, 1),
            //         LengthInMonths = 6,
            //         ContractInformations = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            //         OfferId = 2,
            //         TenantId = 2
            //     },
            //     new //Rent
            //     {
            //         Id = 3,
            //         StartDate = new DateTime(2022, 3, 1),
            //         LengthInMonths = 3,
            //         ContractInformations = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
            //         OfferId = 3,
            //         TenantId = 3
            //     },
            //     new //Rent
            //     {
            //         Id = 4,
            //         StartDate = new DateTime(2022, 4, 1),
            //         LengthInMonths = 9,
            //         ContractInformations = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
            //         OfferId = 1,
            //         TenantId = 1
            //     },
            //     new //Rent
            //     {
            //         Id = 5,
            //         StartDate = new DateTime(2022, 5, 1),
            //         LengthInMonths = 12,
            //         ContractInformations = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            //         OfferId = 2,
            //         TenantId = 2
            //     },
            //     new //Rent
            //     {
            //         Id = 6,
            //         StartDate = new DateTime(2022, 6, 1),
            //         LengthInMonths = 6,
            //         ContractInformations = "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit.",
            //         OfferId = 3,
            //         TenantId = 3
            //     },
            //     new //Rent
            //     {
            //         Id = 7,
            //         StartDate = new DateTime(2022, 7, 1),
            //         LengthInMonths = 3,
            //         ContractInformations = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium.",
            //         OfferId = 1,
            //         TenantId =1
            //     },
            //     new //Rent
            //     {
            //         Id = 8,
            //         StartDate = new DateTime(2022, 8, 1),
            //         LengthInMonths = 9,
            //         ContractInformations = "Totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.",
            //         OfferId = 2,
            //         TenantId = 2
            //     },
            //     new //Rent
            //     {
            //         Id = 9,
            //         StartDate = new DateTime(2022, 9, 1),
            //         LengthInMonths = 9,
            //         ContractInformations = "Totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.",
            //         OfferId = 3,
            //         TenantId =3
            //     },
            //     new //Rent
            //     {
            //         Id = 10,
            //         StartDate = new DateTime(2022, 4, 1),
            //         LengthInMonths = 9,
            //         ContractInformations = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
            //         OfferId = 1,
            //         TenantId = 1

            //     }
            // );

            // modelBuilder.Entity<RentOpinion>().HasData(
            //     new //RentOpinion
            //     {
            //         Id = 1,
            //         Date = new DateTime(2022, 1, 15),
            //         Localization = 8,
            //         Neighbors = 7,
            //         Equipment = 9,
            //         ParkingPlace = 6,
            //         Tidiness = 9,
            //         Decoration = 7,
            //         Loudness = 4,
            //         ComplianceWithOffer = 8
            //     },
            //     new //RentOpinion
            //     {
            //         Id = 2,
            //         Date = new DateTime(2022, 2, 23),
            //         Localization = 7,
            //         Neighbors = 6,
            //         Equipment = 8,
            //         ParkingPlace = 5,
            //         Tidiness = 8,
            //         Decoration = 6,
            //         Loudness = 5,
            //         ComplianceWithOffer = 9
            //     },
            //     new //RentOpinion
            //     {
            //         Id = 3,
            //         Date = new DateTime(2022, 3, 1),
            //         Localization = 9,
            //         Neighbors = 8,
            //         Equipment = 7,
            //         ParkingPlace = 8,
            //         Tidiness = 7,
            //         Decoration = 7,
            //         Loudness = 6,
            //         ComplianceWithOffer = 7
            //     },
            //     new //RentOpinion
            //     {
            //         Id = 4,
            //         Date = new DateTime(2022, 4, 10),
            //         Localization = 6,
            //         Neighbors = 7,
            //         Equipment = 6,
            //         ParkingPlace = 4,
            //         Tidiness = 6,
            //         Decoration = 5,
            //         Loudness = 7,
            //         ComplianceWithOffer = 8
            //     },
            //     new //RentOpinion
            //     {
            //         Id = 5,
            //         Date = new DateTime(2022, 5, 5),
            //         Localization = 8,
            //         Neighbors = 9,
            //         Equipment = 7,
            //         ParkingPlace = 8,
            //         Tidiness = 9,
            //         Decoration = 7,
            //         Loudness = 3,
            //         ComplianceWithOffer = 8
            //     }
            // );

            // modelBuilder.Entity<Room>().HasData(
            //    new //Room
            //    {
            //        Id = 7,
            //        Address = "ul. Wiejska 1, Warszawa",
            //        Surface = 20,
            //        MaxInhabitants = 1,
            //        Name = "Największy"
            //    },
            //    new //Room
            //    {
            //        Id = 8,
            //        Address = "ul. Wrocławska 12, Kraków",
            //        Surface = 30,
            //        MaxInhabitants = 1,
            //        Name = "Najmniejszy"
            //    },
            //    new //Room
            //    {
            //        Id = 9,
            //        Address = "ul. Kościuszki 50, Gdańsk",
            //        Surface = 35,
            //        MaxInhabitants = 2,
            //        Name = "Najśredniejszy"
            //    }
            //);

            // modelBuilder.Entity<Seeker>().HasData(
            //     new //Seeker
            //     {
            //         Id = 7,
            //         Name = "Jakub",
            //         Surname = "Jankowski",
            //         Address = "ul. Zielonej Łąki",
            //         City = "Poznań",
            //         PhoneNumber = "367-159-465",
            //         Email = "jjankowski@example.com",
            //         AccountCreationDate = DateTime.UtcNow.AddDays(-30),
            //         LastLoginDate = DateTime.UtcNow.AddDays(-1),
            //         Password = "password123",
            //         PhotoPath = "/path/to/photo.jpg",
            //         ActivityStatus = true,
            //         DocumentPath = "/path/to/document.pdf",
            //         DocumentType = DocumentType.StudentCard,
            //         VerificationStatus = VerificationStatus.Verified,
            //         DocumentExpireDate = DateTime.UtcNow.AddYears(1),
            //         YearOfBirth = 2000,
            //         Age = 23,
            //         StudentNumber = "123456"
            //     },
            //     new //Seeker
            //     {
            //         Id = 8,
            //         Name = "Mariusz",
            //         Surname = "Drzazga",
            //         Address = "ul. Przykładowa",
            //         City = "Turkowice",
            //         PhoneNumber = "213-766-669",
            //         Email = "mtrzaska@example.com",
            //         AccountCreationDate = DateTime.UtcNow.AddDays(-60),
            //         LastLoginDate = DateTime.UtcNow.AddDays(-10),
            //         Password = "password456",
            //         PhotoPath = "/path/to/photo2.jpg",
            //         ActivityStatus = true,
            //         DocumentPath = "/path/to/document2.pdf",
            //         DocumentType = DocumentType.StudentCard,
            //         VerificationStatus = VerificationStatus.Verified,
            //         DocumentExpireDate = DateTime.UtcNow.AddYears(2),
            //         YearOfBirth = 1995,
            //         Age = 28,
            //         StudentNumber = "789012"
            //     },
            //     new //Seeker
            //     {
            //         Id = 9,
            //         Name = "Michał ",
            //         Surname = "Kubiak",
            //         Address = "ul. Narutowicza",
            //         City = "Zamość",
            //         PhoneNumber = "789-213-159",
            //         Email = "mkubiak@example.com",
            //         AccountCreationDate = DateTime.UtcNow.AddDays(-15),
            //         LastLoginDate = DateTime.UtcNow.AddDays(-2),
            //         Password = "password789",
            //         PhotoPath = "/path/to/photo3.jpg",
            //         ActivityStatus = true,
            //         DocumentPath = "/path/to/document3.pdf",
            //         DocumentType = DocumentType.Passport,
            //         VerificationStatus = VerificationStatus.Verified,
            //         DocumentExpireDate = DateTime.UtcNow.AddYears(5),
            //         YearOfBirth = 1998,
            //         Age = 25,
            //         StudentNumber = "345678"
            //     }
            // );



            // modelBuilder.Entity<StudentSurvey>().HasData(
            //     new //StudentSurvey
            //     {
            //         Id = 1,
            //         Smoking = false,
            //         Animal = false,
            //         Vegan = true,
            //         Party = 8,
            //         Tidiness = 9,
            //         Loudness = 4,
            //         Sociability = 7,
            //         Helpfulness = 6,
            //         Roommate = true,
            //         MaxNumberOfRoommates = 2,
            //         RoommateGender = Gender.Female,
            //         MinRoommateAge = 20,
            //         MaxRoommateAge = 25,
            //     },
            //     new //StudentSurvey
            //     {
            //         Id = 2,
            //         Smoking = true,
            //         Animal = false,
            //         Vegan = false,
            //         Party = 5,
            //         Tidiness = 6,
            //         Loudness = 7,
            //         Sociability = 8,
            //         Helpfulness = 9,
            //         Roommate = false,
            //         MaxNumberOfRoommates = 1,
            //         RoommateGender = Gender.Male,
            //         MinRoommateAge = 22,
            //         MaxRoommateAge = 30
            //     },
            //     new //StudentSurvey
            //     {
            //         Id = 3,
            //         Smoking = false,
            //         Animal = true,
            //         Vegan = false,
            //         Party = 9,
            //         Tidiness = 8,
            //         Loudness = 3,
            //         Sociability = 6,
            //         Helpfulness = 7,
            //         Roommate = true,
            //         MaxNumberOfRoommates = 3,
            //         RoommateGender = Gender.Both,
            //         MinRoommateAge = 18,
            //         MaxRoommateAge = 25
            //     }
            // );

            // modelBuilder.Entity<Tenant>().HasData(
            //     new //Tenant
            //     {
            //         Id = 4,
            //         Name = "Łukasz",
            //         Surname = "Dąbrowski",
            //         Address = "al. KEN",
            //         City = "Zielona Góra",
            //         PhoneNumber = "759-213-987",
            //         Email = "ldabrawski@example.com",
            //         AccountCreationDate = DateTime.UtcNow.AddDays(-30),
            //         LastLoginDate = DateTime.UtcNow.AddDays(-1),
            //         Password = "password123",
            //         PhotoPath = "/path/to/photo.jpg",
            //         ActivityStatus = true,
            //         DocumentPath = "/path/to/document.pdf",
            //         DocumentType = DocumentType.StudentCard,
            //         VerificationStatus = VerificationStatus.Verified,
            //         DocumentExpireDate = DateTime.UtcNow.AddYears(1),
            //         YearOfBirth = 2000,
            //         Age = 23,
            //         StudentNumber = "123456",
            //         RoommatesStatus = RoommatesStatus.Alone
            //     },
            //     new //Tenant
            //     {
            //         Id = 5,
            //         Name = "Ewa",
            //         Surname = "Szymańska",
            //         Address = "ul. Woronicza 32",
            //         City = "Bydgoszcz",
            //         PhoneNumber = "546-159-756",
            //         Email = "eszymanska@example.com",
            //         AccountCreationDate = DateTime.UtcNow.AddDays(-30),
            //         LastLoginDate = DateTime.UtcNow.AddDays(-1),
            //         Password = "password456",
            //         PhotoPath = "/path/to/photo.jpg",
            //         ActivityStatus = true,
            //         DocumentPath = "/path/to/document.pdf",
            //         DocumentType = DocumentType.ID,
            //         VerificationStatus = VerificationStatus.Verified,
            //         DocumentExpireDate = DateTime.UtcNow.AddYears(1),
            //         YearOfBirth = 2001,
            //         Age = 22,
            //         StudentNumber = "654321",
            //         RoommatesStatus = RoommatesStatus.Roommate
            //     },
            //     new //Tenant
            //     {
            //         Id = 6,
            //         Name = "Adam",
            //         Surname = "Cieślak",
            //         Address = "ul. Wilczy Dół",
            //         City = "Zakopane",
            //         PhoneNumber = "159-354-465",
            //         Email = "acieslak@example.com",
            //         AccountCreationDate = DateTime.UtcNow.AddDays(-30),
            //         LastLoginDate = DateTime.UtcNow.AddDays(-1),
            //         Password = "password789",
            //         PhotoPath = "/path/to/photo.jpg",
            //         ActivityStatus = true,
            //         DocumentPath = "/path/to/document.pdf",
            //         DocumentType = DocumentType.Passport,
            //         VerificationStatus = VerificationStatus.NotVerified,
            //         DocumentExpireDate = DateTime.UtcNow.AddYears(1),
            //         YearOfBirth = 1999,
            //         Age = 24,
            //         StudentNumber = "789012",
            //         RoommatesStatus = RoommatesStatus.Roommate
            //     }
            //  );


            //modelbuilder.entity<tenant>().hasdata(
            //    new
            //    {
            //        tenantid = 1,
            //        name = "jan",
            //        surname = "kowalski",
            //        addressline1 = "ul. dluga 1",
            //        addressline2 = "",
            //        addressline3 = "",
            //        email = "jan.kowalski@gmail.com",
            //        phonenumber = "123456789"
            //    },
            //    new
            //    {
            //        tenantid = 2,
            //        name = "maciej",
            //        surname = "nowak",
            //        addressline1 = "ul. dluga 45",
            //        addressline2 = "",
            //        addressline3 = "",
            //        email = "maciej.nowak@gmail.com",
            //        phonenumber = "987654321"
            //    }
            //);

            //modelbuilder.entity<flat>().hasdata(
            //    new
            //    {
            //        flatid = 1,
            //        name = "mieszkanie 1",
            //        addressline1 = "ul. dluga 1",
            //        addressline2 = "",
            //        addressline3 = "",
            //        metricarea = 40.0f,
            //        maxnumberofinhabitants = 5
            //    },
            //    new
            //    {
            //        flatid = 2,
            //        name = "mieszkanie 2",
            //        addressline1 = "ul. dluga 45",
            //        addressline2 = "",
            //        addressline3 = "",
            //        metricarea = 50.0f,
            //        maxnumberofinhabitants = 4
            //    }
            //);

            //modelbuilder.entity<rent>().hasdata(
            //    new
            //    {
            //        rentid = 1,
            //        tenantid = 1,
            //        flatid = 2,
            //        startdate = new datetime(2022, 10, 25),
            //        durationinmonths = 10,
            //        pricepermonth = 2000.0f,
            //    },
            //    new
            //    {
            //        rentid = 2,
            //        tenantid = 2,
            //        flatid = 1,
            //        startdate = new datetime(2022, 11, 5),
            //        durationinmonths = 6,
            //        pricepermonth = 2000.0f,
            //    }
            //);

        }
    }
}
