using Flats4us.Entities;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Flats4us.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

public static class DataSeeder
{
    private static FileUploadService _fileUploadService;

    public static async Task SeedDataAsync(Flats4usContext dbContext, IConfiguration configuration)
    {
        _fileUploadService = new FileUploadService(dbContext, configuration);

        await _fileUploadService.ClearDirectoryAsync(configuration["FileUploadSettings:UploadPath"]);

        #region Equipment

        var equipment1 = new Equipment
        {
            Name = "Dishwasher"
        };
        var equipment2 = new Equipment
        {
            Name = "WashingMachine"
        };
        var equipment3 = new Equipment
        {
            Name = "Iron"
        };
        var equipment4 = new Equipment
        {
            Name = "Kettle"
        };
        var equipment5 = new Equipment
        {
            Name = "CoffeeMachine"
        };
        var equipment6 = new Equipment
        {
            Name = "AirConditioning"
        };
        var equipment7 = new Equipment
        {
            Name = "Balcony"
        };
        var equipment8 = new Equipment
        {
            Name = "TV"
        };
        var equipment9 = new Equipment
        {
            Name = "Bath"
        };
        var equipment10 = new Equipment
        {
            Name = "Oven"
        };
        var equipment11 = new Equipment
        {
            Name = "Microwave"
        };
        var equipment12 = new Equipment
        {
            Name = "Elevator"
        };

        dbContext.Equipment.AddRange(equipment1, equipment2, equipment3, equipment4, equipment5, equipment6, equipment7, equipment8, equipment9, equipment10, equipment11, equipment12);

        #endregion

        #region Interest

        var interest1 = new Interest
        {
            Name = "Photography"
        };
        var interest2 = new Interest
        {
            Name = "Hiking"
        };
        var interest3 = new Interest
        {
            Name = "Cooking"
        };
        var interest4 = new Interest
        {
            Name = "Traveling"
        };
        var interest5 = new Interest
        {
            Name = "Reading"
        };
        var interest6 = new Interest
        {
            Name = "Gardening"
        };
        var interest7 = new Interest
        {
            Name = "Music"
        };
        var interest8 = new Interest
        {
            Name = "Volunteering"
        };
        var interest9 = new Interest
        {
            Name = "Sport"
        };
        var interest10 = new Interest
        {
            Name = "Volunteering"
        };
        var interest11 = new Interest
        {
            Name = "Painting"
        };
        var interest12 = new Interest
        {
            Name = "Cycling"
        };
        var interest13 = new Interest
        {
            Name = "Yoga"
        };
        var interest14 = new Interest
        {
            Name = "Games"
        };
        var interest15 = new Interest
        {
            Name = "Writing"
        };
        var interest16 = new Interest
        {
            Name = "Movies"
        };
        var interest17 = new Interest
        {
            Name = "Technology"
        };
        var interest18 = new Interest
        {
            Name = "Astronomy"
        };
        var interest19 = new Interest
        {
            Name = "DIY"
        };
        var interest20 = new Interest
        {
            Name = "Modeling"
        };


        dbContext.Interests.AddRange(interest1, interest2, interest3, interest4, interest5, interest6, interest7, interest8, interest9, interest10, interest11, interest12, interest13, interest14, interest15, interest16, interest17, interest18, interest19, interest20);

        #endregion

        #region Owner

        var owner1 = new Owner
        {
            Name = "Maciej",
            Surname = "Kowalski",
            Address = "Kaukaska 9/2, 02-760 Warszawa",
            Email = "mkowalski@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 1, 12),
            VerificationOrRejectionDate = new DateTime(2023, 1, 12),
            LastLoginDate = new DateTime(2023, 10, 12),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Mkowalski123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 12, 8),
            BankAccount = "12341234123412341234123412",
            DocumentNumber = "XXX 000000"
        };
        var owner2 = new Owner
        {
            Name = "Barbara",
            Surname = "Nowak",
            Address = "Tuchlińska 2/2, 02-695 Warszawa",
            Email = "bnowak@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 3, 23),
            DateForVerificationSorting = new DateTime(2023, 3, 23),
            LastLoginDate = new DateTime(2023, 10, 10),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Bnowak123"),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 9, 8),
            BankAccount = "12341234123412341234123412",
            DocumentNumber = "XXX 000000"
        };
        var owner3 = new Owner
        {
            Name = "Robert",
            Surname = "Pawlak",
            Address = "Kormoranów 9/5, 02-836 Warszawa",
            Email = "rpawlak@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 7, 13),
            VerificationOrRejectionDate = new DateTime(2023, 7, 13),
            LastLoginDate = new DateTime(2023, 10, 20),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Rpawlak123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2026, 4, 8),
            BankAccount = "12341234123412341234123412",
            DocumentNumber = "XXX 000000"
        };
        var owner4 = new Owner
        {
            Name = "Katarzyna",
            Surname = "Klik",
            Address = "Sanocka 11B/1, 02-110 Warszawa",
            Email = "kklik@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 2, 8),
            VerificationOrRejectionDate = new DateTime(2023, 2, 8),
            LastLoginDate = new DateTime(2023, 9, 30),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Kklik123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2029, 5, 14),
            BankAccount = "12341234123412341234123412",
            DocumentNumber = "XXX 000000"
        };
        var owner5 = new Owner
        {
            Name = "Anna",
            Surname = "Nowakowska",
            Address = "Północna 10, 03-065 Warszawa",
            Email = "anowakowska@gmail.com",
            PhoneNumber = "987654321",
            AccountCreationDate = new DateTime(2022, 5, 20),
            VerificationOrRejectionDate = new DateTime(2022, 5, 20),
            LastLoginDate = new DateTime(2023, 5, 20),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Anowakowska123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2026, 11, 15),
            BankAccount = "11112222333344445555666677",
            DocumentNumber = "YYY 123456"
        };
        var owner6 = new Owner
        {
            Name = "Piotr",
            Surname = "Dąbrowski",
            Address = "Słoneczna 5, 30-001 Kraków",
            Email = "pdabrowski@gmail.com",
            PhoneNumber = "555444333",
            AccountCreationDate = new DateTime(2023, 3, 10),
            VerificationOrRejectionDate = new DateTime(2023, 3, 10),
            LastLoginDate = new DateTime(2023, 8, 10),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Pdabrowski123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 10, 22),
            BankAccount = "22223333444455556666777788",
            DocumentNumber = "ZZZ 654321"
        };
        var owner7 = new Owner
        {
            Name = "Jan",
            Surname = "Wiśniewski",
            Address = "Łąkowa 7, 50-405 Wrocław",
            Email = "jwisniewski@gmail.com",
            PhoneNumber = "111222333",
            AccountCreationDate = new DateTime(2023, 6, 15),
            VerificationOrRejectionDate = new DateTime(2023, 6, 15),
            LastLoginDate = new DateTime(2023, 11, 15),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Jwisniewski123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2026, 8, 17),
            BankAccount = "33334444555566667777888899",
            DocumentNumber = "ABC 987654"
        };
        var owner8 = new Owner
        {
            Name = "Magdalena",
            Surname = "Lewandowska",
            Address = "Ogrodowa 3, 80-001 Gdańsk",
            Email = "mlewandowska@gmail.com",
            PhoneNumber = "777888999",
            AccountCreationDate = new DateTime(2022, 8, 25),
            DateForVerificationSorting = new DateTime(2022, 8, 25),
            LastLoginDate = new DateTime(2023, 2, 25),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Mlewandowska123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.NotVerified,
            DocumentExpireDate = new DateTime(2025, 9, 30),
            BankAccount = "44445555666677778888999900",
            DocumentNumber = "DEF 123456"
        };
        var owner9 = new Owner
        {
            Name = "Karolina",
            Surname = "Kaczmarek",
            Address = "Polna 12, 60-123 Poznań",
            Email = "kkaczmarek@gmail.com",
            PhoneNumber = "999888777",
            AccountCreationDate = new DateTime(2023, 4, 8),
            VerificationOrRejectionDate = new DateTime(2023, 4, 8),
            LastLoginDate = new DateTime(2023, 9, 8),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Kkaczmarek123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2026, 7, 10),
            BankAccount = "55556666777788889999000011",
            DocumentNumber = "GHI 654321"
        };
        var owner10 = new Owner
        {
            Name = "Tomasz",
            Surname = "Zając",
            Address = "Lesna 4, 40-004 Katowice",
            Email = "tzajac@gmail.com",
            PhoneNumber = "444555666",
            AccountCreationDate = new DateTime(2022, 10, 3),
            DateForVerificationSorting = new DateTime(2022, 10, 3),
            LastLoginDate = new DateTime(2023, 3, 3),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Tzajac123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.NotVerified,
            DocumentExpireDate = new DateTime(2025, 8, 20),
            BankAccount = "66667777888899990000111122",
            DocumentNumber = "JKL 987654"
        };
        var owner11 = new Owner
        {
            Name = "Ewa",
            Surname = "Wójcik",
            Address = "Krótka 6, 70-005 Szczecin",
            Email = "ewojcik@gmail.com",
            PhoneNumber = "333222111",
            AccountCreationDate = new DateTime(2023, 7, 18),
            VerificationOrRejectionDate = new DateTime(2023, 7, 18),
            LastLoginDate = new DateTime(2023, 12, 18),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Ewojcik123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2027, 5, 5),
            BankAccount = "77778888999900001111222233",
            DocumentNumber = "MNO 123456"
        };
        var owner12 = new Owner
        {
            Name = "Katarzyna",
            Surname = "Wójcik",
            Address = "Łączna 15, 90-005 Łódź",
            Email = "kwojcik@gmail.com",
            PhoneNumber = "999888777",
            AccountCreationDate = new DateTime(2023, 4, 18),
            VerificationOrRejectionDate = new DateTime(2023, 4, 18),
            LastLoginDate = new DateTime(2023, 9, 18),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Kwojcik123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2026, 10, 25),
            BankAccount = "55556666777788889999000011",
            DocumentNumber = "GHI 654321"
        };
        var owner13 = new Owner
        {
            Name = "Artur",
            Surname = "Jankowski",
            Address = "Słowiańska 12, 33-100 Tarnów",
            Email = "ajankowski@gmail.com",
            PhoneNumber = "222333444",
            AccountCreationDate = new DateTime(2023, 2, 8),
            DateForVerificationSorting = new DateTime(2023, 2, 8),
            LastLoginDate = new DateTime(2023, 7, 8),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Ajankowski123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.NotVerified,
            DocumentExpireDate = new DateTime(2025, 11, 20),
            BankAccount = "66667777888899990000111122",
            DocumentNumber = "JKL 987654"
        };
        var owner14 = new Owner
        {
            Name = "Barbara",
            Surname = "Kamińska",
            Address = "Krótka 20, 20-001 Lublin",
            Email = "bkaminska@gmail.com",
            PhoneNumber = "444555666",
            AccountCreationDate = new DateTime(2023, 7, 21),
            VerificationOrRejectionDate = new DateTime(2023, 7, 21),
            LastLoginDate = new DateTime(2023, 12, 21),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Bkaminska123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2027, 1, 5),
            BankAccount = "77778888999900001111222233",
            DocumentNumber = "MNO 123456"
        };
        var owner15 = new Owner
        {
            Name = "Dominika",
            Surname = "Sękowska",
            Address = "Złota 20, 20-001 Wołomin",
            Email = "dsekowska@gmail.com",
            PhoneNumber = "444555666",
            AccountCreationDate = new DateTime(2024, 1, 2),
            VerificationOrRejectionDate = new DateTime(2024, 1, 12),
            LastLoginDate = new DateTime(2024, 6, 13),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Dsekowska123"),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2027, 1, 5),
            BankAccount = "77778888999900001111222233",
            DocumentNumber = "MNO 123456"
        };


        dbContext.Owners.AddRange(owner1, owner2, owner3, owner4, owner5, owner6, owner7, owner8, owner9, owner10, owner11, owner12, owner13, owner14, owner15);

        SeedUserFiles(owner1, Gender.Male).Wait();
        SeedUserFiles(owner2, Gender.Female).Wait();
        SeedUserFiles(owner3, Gender.Male).Wait();
        SeedUserFiles(owner4, Gender.Female).Wait();
        SeedUserFiles(owner5, Gender.Female).Wait();
        SeedUserFiles(owner6, Gender.Male).Wait();
        SeedUserFiles(owner7, Gender.Male).Wait();
        SeedUserFiles(owner8, Gender.Female).Wait();
        SeedUserFiles(owner9, Gender.Female).Wait();
        SeedUserFiles(owner10, Gender.Male).Wait();
        SeedUserFiles(owner11, Gender.Female).Wait();
        SeedUserFiles(owner12, Gender.Female).Wait();
        SeedUserFiles(owner13, Gender.Male).Wait();
        SeedUserFiles(owner14, Gender.Female).Wait();
        SeedUserFiles(owner15, Gender.Female).Wait();

        #endregion

        #region Student

        var student1 = new Student
        {
            Name = "Kajetan",
            Surname = "Kajetański",
            Address = "Kaukaska 11/6, 02-760 Warszawa",
            Email = "kkajetanski@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 1, 12),
            VerificationOrRejectionDate = new DateTime(2023, 1, 12),
            LastLoginDate = new DateTime(2023, 10, 12),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Kkajetanski123"),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 12, 8),
            BirthDate = new DateTime(2002, 12, 1),
            StudentNumber = "s27235",
            University = "Warszawski Uniwersytet Medyczny",
            Links = "https://www.facebook.com|https://twitter.com|https://www.instagram.com",
            IsTenant = false,
            Interests = { interest12, interest14, interest1 }
        };
        var student2 = new Student
        {
            Name = "Adrian",
            Surname = "Klocek",
            Address = "Racławicka 26, 02-601 Warszawa",
            Email = "aklocek@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 1, 12),
            VerificationOrRejectionDate = new DateTime(2023, 1, 12),
            LastLoginDate = new DateTime(2023, 10, 12),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Aklocek123"),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 12, 8),
            BirthDate = new DateTime(2002, 12, 1),
            StudentNumber = "s2137",
            University = "Collegium Civitas w Warszawie",
            Links = "https://www.facebook.com|https://twitter.com|https://www.instagram.com",
            IsTenant = false,
            Interests = { interest8, interest7, interest2 }
        };
        var student3 = new Student
        {
            Name = "Kuba",
            Surname = "Filipek",
            Address = "Racławicka 26, 02-601 Warszawa",
            Email = "kfilipek@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 1, 12),
            VerificationOrRejectionDate = new DateTime(2023, 1, 12),
            LastLoginDate = new DateTime(2023, 10, 12),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Kfilipek123"),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 12, 8),
            BirthDate = new DateTime(1978, 12, 1), //45 lat
            StudentNumber = "s2137",
            University = "Wojskowa Akademia Techniczna w Warszawie",
            Links = "https://www.facebook.com|https://twitter.com|https://www.instagram.com",
            IsTenant = false,
            Interests = { interest8, interest7, interest2 }
        };
        var student4 = new Student
        {
            Name = "Łukasz",
            Surname = "Guziewicz",
            Address = "Racławicka 26, 02-601 Warszawa",
            Email = "lguziewicz@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 1, 12),
            VerificationOrRejectionDate = new DateTime(2023, 1, 12),
            LastLoginDate = new DateTime(2023, 10, 12),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Lguziewicz123"),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 12, 8),
            BirthDate = new DateTime(1998, 12, 1), //25 lat
            StudentNumber = "s12345",
            University = "Szkoła Główna Gospodarstwa Wiejskiego w Warszawie",
            Links = "https://www.facebook.com|https://twitter.com|https://www.instagram.com",
            IsTenant = false,
            Interests = { interest8, interest7, interest2 }
        };
        var student5 = new Student
        {
            Name = "Anna",
            Surname = "Nowak",
            Address = "Piotrkowska 22/8, 90-101 Łódź",
            Email = "anowak@gmail.com",
            PhoneNumber = "987654321",
            AccountCreationDate = new DateTime(2023, 2, 5),
            VerificationOrRejectionDate = new DateTime(2023, 2, 5),
            LastLoginDate = new DateTime(2023, 11, 5),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Anowak123"),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2026, 6, 15),
            BirthDate = new DateTime(1995, 5, 18),  //29 lat
            StudentNumber = "s19876",
            University = "Polsko-Japońska Akademia Technik Komputerowych w Warszawie",
            Links = "https://www.facebook.com|https://twitter.com|https://www.instagram.com",
            IsTenant = true,
            Interests = { interest6, interest3, interest8 }
        };
        var student6 = new Student
        {
            Name = "Zenon",
            Surname = "Kowalski",
            Address = "Aleje Jerozolimskie 45/3, 00-697 Warszawa",
            Email = "zkowalski@gmail.com",
            PhoneNumber = "555666777",
            AccountCreationDate = new DateTime(2023, 3, 20),
            VerificationOrRejectionDate = new DateTime(2023, 3, 20),
            LastLoginDate = new DateTime(2023, 12, 20),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Zkowalski123"),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2027, 4, 30),
            BirthDate = new DateTime(1990, 8, 7),  //33 lata
            StudentNumber = "s12345",
            University = "Uniwersytet Warszawski",
            Links = "https://www.facebook.com|https://twitter.com|https://www.instagram.com",
            IsTenant = false,
            Interests = { interest1, interest3, interest9 }
        };
        var student7 = new Student
        {
            Name = "Marta",
            Surname = "Wiśniewska",
            Address = "ul. Lecha 7/15, 50-501 Wrocław",
            Email = "mwisniewska@gmail.com",
            PhoneNumber = "789012345",
            AccountCreationDate = new DateTime(2023, 4, 10),
            VerificationOrRejectionDate = new DateTime(2023, 4, 10),
            LastLoginDate = new DateTime(2024, 1, 1),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Mwisniewska123"),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2028, 8, 22),
            BirthDate = new DateTime(1993, 7, 14),  //28 lat
            StudentNumber = "s33456",
            University = "Politechnika Warszawska",
            Links = "https://www.facebook.com|https://twitter.com|https://www.instagram.com",
            IsTenant = true,
            Interests = { interest19, interest17, interest6 }
        };
        var student8 = new Student
        {
            Name = "Piotr",
            Surname = "Zawadzki",
            Address = "ul. Mickiewicza 3/2, 30-059 Kraków",
            Email = "pzawadzki@gmail.com",
            PhoneNumber = "654321098",
            AccountCreationDate = new DateTime(2023, 5, 15),
            DateForVerificationSorting = new DateTime(2023, 5, 15),
            LastLoginDate = new DateTime(2023, 12, 15),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Pzawadzki123"),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.NotVerified,
            DocumentExpireDate = new DateTime(2026, 5, 10),
            BirthDate = new DateTime(1994, 11, 28),  //27 lat
            StudentNumber = "s44567",
            University = "Akademia Leona Kożmińskiego w Warszawie",
            Links = "https://www.facebook.com|https://twitter.com|https://www.instagram.com",
            IsTenant = false,
            Interests = { interest4, interest15, interest8 }
        };
        var student9 = new Student
        {
            Name = "Karolina",
            Surname = "Dąbrowska",
            Address = "ul. Krakowska 18/7, 20-001 Lublin",
            Email = "kdabrowska@gmail.com",
            PhoneNumber = "876543210",
            AccountCreationDate = new DateTime(2023, 6, 25),
            VerificationOrRejectionDate = new DateTime(2023, 6, 25),
            LastLoginDate = new DateTime(2023, 12, 25),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Kdabrowska123"),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2029, 9, 5),
            BirthDate = new DateTime(1991, 3, 8),  //32 lata
            StudentNumber = "s56789",
            University = "Collegium Humanum w Warszawie",
            Links = "https://www.facebook.com|https://twitter.com|https://www.instagram.com",
            IsTenant = true,
            Interests = { interest17, interest4, interest6 }
        };
        var student10 = new Student
        {
            Name = "Łukasz",
            Surname = "Nowicki",
            Address = "ul. Armii Krajowej 14/4, 33-100 Tarnów",
            Email = "lnowicki@gmail.com",
            PhoneNumber = "999888777",
            AccountCreationDate = new DateTime(2023, 7, 10),
            VerificationOrRejectionDate = new DateTime(2023, 7, 10),
            LastLoginDate = new DateTime(2023, 12, 10),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Lnowicki123"),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2027, 12, 18),
            BirthDate = new DateTime(1992, 9, 22),  //29 lat
            StudentNumber = "s67890",
            University = "Uczelnia Łazarskiego w Warszawie",
            Links = "https://www.facebook.com|https://twitter.com|https://www.instagram.com",
            IsTenant = false,
            Interests = { interest15, interest13, interest18 }
        };
        var student11 = new Student // rent5
        {
            Name = "Anna",
            Surname = "Kowalska",
            Address = "ul. Jana Pawła II 5/3, 30-084 Kraków",
            Email = "akowalska@gmail.com",
            PhoneNumber = "888777666",
            AccountCreationDate = new DateTime(2022, 8, 15),
            VerificationOrRejectionDate = new DateTime(2022, 8, 20),
            LastLoginDate = new DateTime(2024, 1, 5),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Akowalska123"),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2028, 4, 30),
            BirthDate = new DateTime(1995, 11, 15),  // 28 lat
            StudentNumber = "s12345",
            University = "Uniwersytet Jagielloński",
            Links = "https://www.linkedin.com|https://www.github.com|https://www.instagram.com",
            IsTenant = false,
            Interests = { interest2, interest7, interest14 }
        };
        var student12 = new Student // rent6
        {
            Name = "Michał",
            Surname = "Nowak",
            Address = "ul. Piłsudskiego 12/6, 02-678 Warszawa",
            Email = "mnowak@gmail.com",
            PhoneNumber = "777666555",
            AccountCreationDate = new DateTime(2021, 3, 18),
            VerificationOrRejectionDate = new DateTime(2021, 3, 25),
            LastLoginDate = new DateTime(2024, 5, 2),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Mnowak123"),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2028, 7, 15),
            BirthDate = new DateTime(1998, 1, 10),  // 26 lat
            StudentNumber = "s98765",
            University = "Politechnika Warszawska",
            Links = "https://www.linkedin.com|https://twitter.com|https://www.instagram.com",
            IsTenant = false,
            Interests = { interest3, interest5, interest9 }
        };
        var student13 = new Student //rent 7
        {
            Name = "Katarzyna",
            Surname = "Zielińska",
            Address = "ul. Grunwaldzka 22/8, 40-027 Katowice",
            Email = "kzielinska@gmail.com",
            PhoneNumber = "666555444",
            AccountCreationDate = new DateTime(2020, 11, 5),
            VerificationOrRejectionDate = new DateTime(2020, 11, 10),
            LastLoginDate = new DateTime(2024, 6, 1),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Kzielinska123"),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2029, 9, 22),
            BirthDate = new DateTime(1993, 5, 30),  // 31 lat
            StudentNumber = "s54321",
            University = "Uniwersytet Śląski",
            Links = "https://www.facebook.com|https://www.linkedin.com|https://www.instagram.com",
            IsTenant = false,
            Interests = { interest1, interest6, interest11 }
        };
        var student14 = new Student
        {
            Name = "Paweł",
            Surname = "Wiśniewski",
            Address = "ul. Kościuszki 9/7, 80-171 Gdańsk",
            Email = "pwisniewski@gmail.com",
            PhoneNumber = "555444333",
            AccountCreationDate = new DateTime(2019, 4, 22),
            VerificationOrRejectionDate = new DateTime(2019, 4, 28),
            LastLoginDate = new DateTime(2024, 4, 10),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Pwisniewski123"),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2026, 11, 30),
            BirthDate = new DateTime(1996, 8, 15),  // 27 lat
            StudentNumber = "s24680",
            University = "Uniwersytet Gdański",
            Links = "https://www.linkedin.com|https://www.github.com|https://www.twitter.com",
            IsTenant = false,
            Interests = { interest4, interest8, interest12 }
        };
        var student15 = new Student
        {
            Name = "Agnieszka",
            Surname = "Kaczmarek",
            Address = "ul. Długa 18/10, 60-100 Poznań",
            Email = "akaczmarek@gmail.com",
            PhoneNumber = "444333222",
            AccountCreationDate = new DateTime(2021, 6, 30),
            VerificationOrRejectionDate = new DateTime(2021, 7, 5),
            LastLoginDate = new DateTime(2024, 3, 15),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Akaczmarek123"),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2029, 10, 12),
            BirthDate = new DateTime(1994, 3, 22),  // 30 lat
            StudentNumber = "s13579",
            University = "Uniwersytet im. Adama Mickiewicza w Poznaniu",
            Links = "https://www.facebook.com|https://twitter.com|https://www.linkedin.com",
            IsTenant = false,
            Interests = { interest10, interest16, interest19 }
        };
        var student16 = new Student
        {
            Name = "Piotr",
            Surname = "Nowak",
            Address = "ul. Krótka 5, 30-200 Kraków",
            Email = "pnowak@gmail.com",
            PhoneNumber = "555666777",
            AccountCreationDate = new DateTime(2021, 8, 15),
            VerificationOrRejectionDate = new DateTime(2021, 8, 20),
            LastLoginDate = new DateTime(2024, 4, 20),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Pnowak123"),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2030, 5, 25),
            BirthDate = new DateTime(1995, 7, 10),  // 28 lat
            StudentNumber = "s24680",
            University = "Uniwersytet Jagielloński",
            Links = "https://www.facebook.com/pnowak|https://twitter.com/pnowak|https://www.linkedin.com/in/pnowak",
            IsTenant = false,
            Interests = { interest3, interest8, interest12 }
        };
        var student17 = new Student
        {
            Name = "Marzena",
            Surname = "Wójcik",
            Address = "ul. Lipowa 3/5, 50-300 Wrocław",
            Email = "mwojcik@gmail.com",
            PhoneNumber = "111222333",
            AccountCreationDate = new DateTime(2021, 7, 10),
            VerificationOrRejectionDate = new DateTime(2021, 7, 15),
            LastLoginDate = new DateTime(2024, 5, 10),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Mwojcik123"),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2031, 8, 20),
            BirthDate = new DateTime(1996, 9, 5),  // 27 lat
            StudentNumber = "s97531",
            University = "Uniwersytet Wrocławski",
            Links = "https://www.facebook.com/mwojcik|https://twitter.com/mwojcik|https://www.linkedin.com/in/mwojcik",
            IsTenant = false,
            Interests = { interest5, interest14, interest15 }
        };
        var student18 = new Student
        {
            Name = "Piotr",
            Surname = "Kowalski",
            Address = "ul. Parkowa 12, 00-001 Warszawa",
            Email = "pkowalski@gmail.com",
            PhoneNumber = "777888999",
            AccountCreationDate = new DateTime(2021, 9, 20),
            VerificationOrRejectionDate = new DateTime(2021, 9, 25),
            LastLoginDate = new DateTime(2024, 6, 5),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Pkowalski789"),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2032, 12, 31),
            BirthDate = new DateTime(1993, 11, 15),  // 30 lat
            StudentNumber = "s75319",
            University = "Uniwersytet Warszawski",
            Links = "https://www.facebook.com/pkowalski|https://twitter.com/pkowalski|https://www.linkedin.com/in/pkowalski",
            IsTenant = false,
            Interests = { interest1, interest7, interest18 }
        };
        var student19 = new Student
        {
            Name = "Ewelina",
            Surname = "Nowak",
            Address = "ul. Zielona 7, 30-400 Kraków",
            Email = "enowak@gmail.com",
            PhoneNumber = "555444333",
            AccountCreationDate = new DateTime(2021, 10, 5),
            VerificationOrRejectionDate = new DateTime(2021, 10, 10),
            LastLoginDate = new DateTime(2024, 7, 15),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Enowak123"),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2033, 5, 20),
            BirthDate = new DateTime(1997, 4, 25),  // 27 lat
            StudentNumber = "s86420",
            University = "Uniwersytet Pedagogiczny w Krakowie",
            Links = "https://www.facebook.com/mnowak|https://twitter.com/mnowak|https://www.linkedin.com/in/mnowak",
            IsTenant = false,
            Interests = { interest2, interest9, interest15 }
        };
        var student20 = new Student
        {
            Name = "Anna",
            Surname = "Wiśniewska",
            Address = "ul. Kwiatowa 15, 80-200 Gdańsk",
            Email = "awiśniewska@gmail.com",
            PhoneNumber = "111222333",
            AccountCreationDate = new DateTime(2021, 11, 12),
            VerificationOrRejectionDate = new DateTime(2021, 11, 17),
            LastLoginDate = new DateTime(2024, 8, 20),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Awisniewska123"),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2034, 3, 10),
            BirthDate = new DateTime(1998, 8, 5),  // 25 lat
            StudentNumber = "s102938",
            University = "Uniwersytet Gdański",
            Links = "https://www.facebook.com/awisniewska|https://twitter.com/awisniewska|https://www.linkedin.com/in/awisniewska",
            IsTenant = true,
            Interests = { interest4, interest11, interest20 }
        };

        dbContext.Students.AddRange(student1, student2, student3, student4, student5, student6, student7, student8, student9, student10, student11, student12, student13, student14, student15, student16, student17, student18, student19, student20);

        SeedUserFiles(student1, Gender.Male).Wait();
        SeedUserFiles(student2, Gender.Male).Wait();
        SeedUserFiles(student3, Gender.Male).Wait();
        SeedUserFiles(student4, Gender.Male).Wait();
        SeedUserFiles(student5, Gender.Female).Wait();
        SeedUserFiles(student6, Gender.Male).Wait();
        SeedUserFiles(student7, Gender.Female).Wait();
        SeedUserFiles(student8, Gender.Male).Wait();
        SeedUserFiles(student9, Gender.Female).Wait();
        SeedUserFiles(student10, Gender.Male).Wait();
        SeedUserFiles(student11, Gender.Female).Wait();
        SeedUserFiles(student12, Gender.Male).Wait();
        SeedUserFiles(student13, Gender.Female).Wait();
        SeedUserFiles(student14, Gender.Male).Wait();
        SeedUserFiles(student15, Gender.Female).Wait();
        SeedUserFiles(student16, Gender.Male).Wait();
        SeedUserFiles(student17, Gender.Female).Wait();
        SeedUserFiles(student18, Gender.Male).Wait();
        SeedUserFiles(student19, Gender.Female).Wait();
        SeedUserFiles(student20, Gender.Female).Wait();

        #endregion

        #region Moderator

        var moderator1 = new Moderator
        {
            Name = "Zbyszek",
            Surname = "Moderator",
            Address = "Kormoranów 1, 02-836 Warszawa",
            Email = "zmoderator@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 1, 12),
            LastLoginDate = new DateTime(2023, 1, 12),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Zmoderator123"),
            VerificationStatus = VerificationStatus.Verified,
            HireDate = new DateTime(2023, 2, 7),
        };

        dbContext.Moderators.AddRange(moderator1);

        #endregion

        #region Flat

        var flat1 = new Flat
        {
            Province = "Mazowieckie",
            District = "Wilanów",
            Street = "Radosna",
            Number = "4",
            Flat = 3,
            City = "Warszawa",
            PostalCode = "02-956",
            GeoLat = 52.174155,
            GeoLon = 21.067260675687436,
            Area = 40,
            MaxNumberOfInhabitants = 2,
            ConstructionYear = 2000,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 10, 1),
            VerificationOrRejectionDate = new DateTime(2023, 10, 1),
            Owner = owner1,
            NumberOfRooms = 2,
            Floor = 2,
            Equipment = { equipment1, equipment2, equipment4, equipment5 }
        };
        var flat2 = new Flat
        {
            Province = "Mazowieckie",
            District = "Śródmieście",
            Street = "Mokotowska",
            Number = "23",
            Flat = 5,
            City = "Warszawa",
            PostalCode = "00-560",
            GeoLat = 52.22087225,
            GeoLon = 21.018367870470342,
            Area = 64,
            MaxNumberOfInhabitants = 4,
            ConstructionYear = 1980,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 10, 9),
            VerificationOrRejectionDate = new DateTime(2023, 10, 9),
            Owner = owner1,
            NumberOfRooms = 3,
            Floor = 2,
            Equipment = { equipment7, equipment9, equipment11 }
        };
        var flat3 = new Flat
        {
            Province = "Mazowieckie",
            District = "Mokotów",
            Street = "Jana Sebastiana Bacha",
            Number = "34A",
            Flat = 56,
            City = "Warszawa",
            PostalCode = "00-560",
            GeoLat = 52.17159595,
            GeoLon = 21.022209107962908,
            Area = 50,
            MaxNumberOfInhabitants = 3,
            ConstructionYear = 1980,
            VerificationStatus = VerificationStatus.NotVerified,
            CreationDate = new DateTime(2023, 11, 1),
            DateForVerificationSorting = new DateTime(2023, 11, 1),
            Owner = owner2,
            NumberOfRooms = 2,
            Floor = 7,
            Equipment = { equipment1, equipment3, equipment5, equipment6, equipment8 }
        };
        var flat4 = new Flat
        {
            Province = "Mazowieckie",
            District = "Mokotów",
            Street = "Stefana Batorego",
            Number = "18",
            Flat = 2,
            City = "Warszawa",
            PostalCode = "02-591",
            GeoLat = 52.2100468,
            GeoLon = 21.0026623433193,
            Area = 70,
            MaxNumberOfInhabitants = 3,
            ConstructionYear = 1993,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 7, 1),
            VerificationOrRejectionDate = new DateTime(2023, 7, 1),
            Owner = owner2,
            NumberOfRooms = 3,
            Floor = 2,
            Equipment = { equipment2, equipment4, equipment7, equipment9, equipment10 }
        };
        var flat5 = new Flat //owner id 4, offer id 26
        {
            Province = "Mazowieckie",
            District = "Ochota",
            Street = "Radomska",
            Number = "8",
            Flat = 6,
            City = "Warszawa",
            PostalCode = "02-323",
            GeoLat = 52.216403,
            GeoLon = 20.979500848056322,
            Area = 45,
            MaxNumberOfInhabitants = 2,
            ConstructionYear = 2001,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 5, 6),
            DateForVerificationSorting = null,
            VerificationOrRejectionDate = new DateTime(2023, 5, 16),
            Owner = owner3,
            NumberOfRooms = 2,
            Floor = 3,
            Equipment = { equipment1, equipment2, equipment5, equipment8 }
        };
        var flat6 = new Flat
        {
            Province = "Mazowieckie",
            District = "Mokotów",
            Street = "Komputerowa",
            Number = "8",
            Flat = 7,
            City = "Warszawa",
            PostalCode = "02-676",
            GeoLat = 52.17544005,
            GeoLon = 20.991583177892394,
            Area = 50,
            MaxNumberOfInhabitants = 2,
            ConstructionYear = 2010,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 7, 3),
            DateForVerificationSorting = null,
            VerificationOrRejectionDate = new DateTime(2023, 7, 3),
            Owner = owner3,
            NumberOfRooms = 3,
            Floor = 3,
            Equipment = { equipment4, equipment6, equipment9 }
        };
        var flat7 = new Flat
        {
            Province = "Mazowieckie",
            District = "Ursynów",
            Street = "Romualda Gutta",
            Number = "1",
            Flat = 34,
            City = "Warszawa",
            PostalCode = "02-777",
            GeoLat = 52.15631365,
            GeoLon = 21.048578136288008,
            Area = 55,
            MaxNumberOfInhabitants = 2,
            ConstructionYear = 2003,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 11, 14),
            VerificationOrRejectionDate = new DateTime(2023, 11, 14),
            Owner = owner3,
            NumberOfRooms = 3,
            Floor = 9,
            Equipment = { equipment3, equipment4, equipment7, equipment10, equipment11 }
        };
        var flat8 = new Flat
        {
            Province = "Mazowieckie",
            District = "Ursynów",
            Street = "Hawajska",
            Number = "20",
            Flat = 30,
            City = "Warszawa",
            PostalCode = "02-776",
            GeoLat = 52.15212235,
            GeoLon = 21.039417876021126,
            Area = 55,
            MaxNumberOfInhabitants = 2,
            ConstructionYear = 1987,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 9, 4),
            VerificationOrRejectionDate = new DateTime(2023, 9, 4),
            Owner = owner4,
            NumberOfRooms = 2,
            Floor = 10,
            Equipment = { equipment1, equipment2, equipment5, equipment7, equipment8 }
        };
        var flat9 = new Flat
        {
            Province = "Mazowieckie",
            District = "Mokotów",
            Street = "Białej Floty",
            Number = "2A",
            Flat = 104,
            City = "Warszawa",
            PostalCode = "02-092",
            GeoLat = 52.194627749999995,
            GeoLon = 20.98457290224141,
            Area = 45,
            MaxNumberOfInhabitants = 2,
            ConstructionYear = 2019,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 8, 18),
            VerificationOrRejectionDate = new DateTime(2023, 8, 18),
            Owner = owner4,
            NumberOfRooms = 2,
            Floor = 4,
            Equipment = { equipment3, equipment6, equipment11 }
        };
        var flat10 = new Flat
        {
            Province = "Mazowieckie",
            District = "Żoliborz",
            Street = "Ludwika Rydygiera",
            Number = "11A",
            Flat = 13,
            City = "Warszawa",
            PostalCode = "01-793",
            GeoLat = 52.2593113,
            GeoLon = 20.978944876442206,
            Area = 80,
            MaxNumberOfInhabitants = 4,
            ConstructionYear = 2015,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 4, 6),
            VerificationOrRejectionDate = new DateTime(2023, 4, 6),
            Owner = owner4,
            NumberOfRooms = 4,
            Floor = 2,
            Equipment = { equipment1, equipment2, equipment3, equipment6, equipment8 }
        };
        var flat11 = new Flat
        {
            Province = "Małopolskie",
            District = "Stare Miasto",
            Street = "Grodzka",
            Number = "10",
            Flat = 5,
            City = "Kraków",
            PostalCode = "31-006",
            GeoLat = 50.061512,
            GeoLon = 19.937788,
            Area = 55,
            MaxNumberOfInhabitants = 3,
            ConstructionYear = 1960,
            VerificationStatus = VerificationStatus.NotVerified,
            CreationDate = new DateTime(2023, 7, 15),
            DateForVerificationSorting = new DateTime(2023, 7, 15),
            Owner = owner5,
            NumberOfRooms = 3,
            Floor = 4,
            Equipment = { equipment3, equipment5, equipment6 }
        };
        var flat12 = new Flat
        {
            Province = "Wielkopolskie",
            District = "Stare Miasto",
            Street = "Wrocławska",
            Number = "20",
            Flat = 1,
            City = "Poznań",
            PostalCode = "61-837",
            GeoLat = 52.404219,
            GeoLon = 16.931965,
            Area = 70,
            MaxNumberOfInhabitants = 4,
            ConstructionYear = 1985,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 5, 20),
            VerificationOrRejectionDate = new DateTime(2023, 5, 20),
            Owner = owner7,
            NumberOfRooms = 4,
            Floor = 3,
            Equipment = { equipment1, equipment4, equipment7 }
        };
        var flat13 = new Flat
        {
            Province = "Dolnośląskie",
            District = "Krzyki",
            Street = "Tęczowa",
            Number = "8",
            Flat = 12,
            City = "Wrocław",
            PostalCode = "53-601",
            GeoLat = 51.097465,
            GeoLon = 17.030298,
            Area = 90,
            MaxNumberOfInhabitants = 5,
            ConstructionYear = 2010,
            VerificationStatus = VerificationStatus.NotVerified,
            CreationDate = new DateTime(2023, 9, 28),
            DateForVerificationSorting = new DateTime(2023, 9, 28),
            Owner = owner8,
            NumberOfRooms = 4,
            Floor = 6,
            Equipment = { equipment2, equipment5, equipment8 }
        };
        var flat14 = new Flat
        {
            Province = "Śląskie",
            District = "Centrum",
            Street = "3 Maja",
            Number = "15",
            Flat = 7,
            City = "Katowice",
            PostalCode = "40-097",
            GeoLat = 50.259366,
            GeoLon = 19.021578,
            Area = 65,
            MaxNumberOfInhabitants = 3,
            ConstructionYear = 2005,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 12, 10),
            VerificationOrRejectionDate = new DateTime(2023, 12, 10),
            Owner = owner9,
            NumberOfRooms = 3,
            Floor = 3,
            Equipment = { equipment3, equipment6, equipment9 }
        };
        var flat15 = new Flat
        {
            Province = "Kujawsko-Pomorskie",
            District = "Centrum",
            Street = "Jana Pawła II",
            Number = "21",
            Flat = 37,
            City = "Toruń",
            PostalCode = "40-097",
            GeoLat = 50.259366,
            GeoLon = 19.021578,
            Area = 65,
            MaxNumberOfInhabitants = 3,
            ConstructionYear = 2005,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2024, 1, 10),
            VerificationOrRejectionDate = new DateTime(2024, 1, 12),
            Owner = owner15,
            NumberOfRooms = 3,
            Floor = 1,
            Equipment = { equipment3, equipment5, equipment9 }
        };
        var flat16 = new Flat
        {
            Province = "Lubelskie",
            District = "Międzylesie",
            Street = "Zaruby",
            Number = "13",
            Flat = 6,
            City = "Lublin",
            PostalCode = "40-097",
            GeoLat = 50.435421,
            GeoLon = 48.021578,
            Area = 36,
            MaxNumberOfInhabitants = 2,
            ConstructionYear = 2005,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2024, 1, 12),
            VerificationOrRejectionDate = new DateTime(2024, 1, 14),
            Owner = owner15,
            NumberOfRooms = 2,
            Floor = 3,
            Equipment = { equipment1, equipment2, equipment4 }
        };
        var flat17 = new Flat
        {
            Province = "Lubuskie",
            District = "Gdzieś",
            Street = "Wąwozowa",
            Number = "43",
            Flat = 5,
            City = "Zielona Góra",
            PostalCode = "26-846",
            GeoLat = 48.318329,
            GeoLon = 25.286845,
            Area = 84,
            MaxNumberOfInhabitants = 7,
            ConstructionYear = 2010,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2024, 3, 15),
            VerificationOrRejectionDate = new DateTime(2024, 3, 17),
            Owner = owner9,
            NumberOfRooms = 7,
            Floor = 6,
            Equipment = { equipment5, equipment6, equipment6 }
        };

        dbContext.Flats.AddRange(flat1, flat2, flat3, flat4, flat5, flat6, flat7, flat8, flat9, flat10, flat11, flat12, flat13, flat14, flat15, flat16, flat17);

        await SeedPropertyFiles(flat1);
        await SeedPropertyFiles(flat2);
        await SeedPropertyFiles(flat3);
        await SeedPropertyFiles(flat4);
        await SeedPropertyFiles(flat5);
        await SeedPropertyFiles(flat6);
        await SeedPropertyFiles(flat7);
        await SeedPropertyFiles(flat8);
        await SeedPropertyFiles(flat9);
        await SeedPropertyFiles(flat10);
        await SeedPropertyFiles(flat11);
        await SeedPropertyFiles(flat12);
        await SeedPropertyFiles(flat13);
        await SeedPropertyFiles(flat14);
        await SeedPropertyFiles(flat15);
        await SeedPropertyFiles(flat16);
        await SeedPropertyFiles(flat17);

        #endregion

        #region Room

        var room1 = new Room
        {
            Province = "Mazowieckie",
            District = "Ochota",
            Street = "Aleje Jerozolimskie",
            Number = "101",
            Flat = 14,
            City = "Warszawa",
            PostalCode = "02-011",
            GeoLat = 52.2261597,
            GeoLon = 20.997204600410768,
            Area = 20,
            MaxNumberOfInhabitants = 1,
            ConstructionYear = 1970,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 6, 23),
            VerificationOrRejectionDate = new DateTime(2023, 6, 23),
            Owner = owner1,
            Floor = 4,
            Equipment = { equipment4, equipment5, equipment7, equipment9, equipment11 }
        };
        var room2 = new Room
        {
            Province = "Mazowieckie",
            District = "Śródmieście",
            Street = "Nowogrodzka",
            Number = "40",
            Flat = 10,
            City = "Warszawa",
            PostalCode = "00-691",
            GeoLat = 52.228628,
            GeoLon = 21.01060072067528,
            Area = 15,
            MaxNumberOfInhabitants = 1,
            ConstructionYear = 1969,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 7, 22),
            VerificationOrRejectionDate = new DateTime(2023, 7, 22),
            Owner = owner1,
            Floor = 3,
            Equipment = { equipment1, equipment2, equipment4, equipment5, equipment7, equipment10 }
        };
        var room3 = new Room
        {
            Province = "Mazowieckie",
            District = "Włochy",
            Street = "Skromna",
            Number = "3A",
            Flat = 1,
            City = "Warszawa",
            PostalCode = "02-250",
            GeoLat = 52.18856375,
            GeoLon = 20.9548763,
            Area = 25,
            MaxNumberOfInhabitants = 1,
            ConstructionYear = 2023,
            VerificationStatus = VerificationStatus.NotVerified,
            CreationDate = new DateTime(2023, 10, 3),
            DateForVerificationSorting = new DateTime(2023, 10, 3),
            Owner = owner2,
            Floor = 1,
            Equipment = { equipment3, equipment6, equipment8, equipment11 }
        };
        var room4 = new Room //owner id 4, offer id 27
        {
            Province = "Mazowieckie",
            District = "Mokotów",
            Street = "Obrzeżna",
            Number = "12",
            Flat = 20,
            City = "Warszawa",
            PostalCode = "02-691",
            GeoLat = 52.1722711,
            GeoLon = 20.999646745366846,
            Area = 20,
            MaxNumberOfInhabitants = 1,
            ConstructionYear = 2005,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 8, 30),
            DateForVerificationSorting = null,
            VerificationOrRejectionDate = new DateTime(2023, 9, 7),
            Owner = owner3,
            Floor = 4,
            Equipment = { equipment1, equipment2, equipment4, equipment5, equipment8 }
        };
        var room5 = new Room
        {
            Province = "Mazowieckie",
            District = "Praga-Południe",
            Street = "Bajońska",
            Number = "13",
            Flat = 10,
            City = "Warszawa",
            PostalCode = "03-963",
            GeoLat = 52.2264519,
            GeoLon = 21.057582403957994,
            Area = 22,
            MaxNumberOfInhabitants = 1,
            ConstructionYear = 2000,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 3, 17),
            VerificationOrRejectionDate = new DateTime(2023, 3, 17),
            Owner = owner4,
            Floor = 3,
            Equipment = { equipment3, equipment7, equipment9, equipment10 }
        };
        var room6 = new Room
        {
            Province = "Małopolskie",
            District = "Stare Miasto",
            Street = "Grodzka",
            Number = "15",
            Flat = 3,
            City = "Kraków",
            PostalCode = "31-001",
            GeoLat = 50.061829,
            GeoLon = 19.937526,
            Area = 25,
            MaxNumberOfInhabitants = 1,
            ConstructionYear = 1950,
            VerificationStatus = VerificationStatus.NotVerified,
            CreationDate = new DateTime(2023, 8, 10),
            DateForVerificationSorting = new DateTime(2023, 8, 10),
            Owner = owner5,
            Floor = 2,
            Equipment = { equipment1, equipment3, equipment6, equipment8, equipment10 }
        };
        var room7 = new Room
        {
            Province = "Wielkopolskie",
            District = "Stare Miasto",
            Street = "Św. Marcin",
            Number = "25",
            Flat = 6,
            City = "Poznań",
            PostalCode = "61-803",
            GeoLat = 52.406369,
            GeoLon = 16.934847,
            Area = 18,
            MaxNumberOfInhabitants = 1,
            ConstructionYear = 1985,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 9, 5),
            VerificationOrRejectionDate = new DateTime(2023, 9, 5),
            Owner = owner6,
            Floor = 3,
            Equipment = { equipment2, equipment4, equipment7, equipment9, equipment11 }
        };
        var room8 = new Room
        {
            Province = "Łódzkie",
            District = "Bałuty",
            Street = "Piotrkowska",
            Number = "120",
            Flat = 9,
            City = "Łódź",
            PostalCode = "90-001",
            GeoLat = 51.765188,
            GeoLon = 19.458248,
            Area = 22,
            MaxNumberOfInhabitants = 1,
            ConstructionYear = 1965,
            VerificationStatus = VerificationStatus.NotVerified,
            CreationDate = new DateTime(2023, 10, 20),
            DateForVerificationSorting = new DateTime(2023, 10, 20),
            Owner = owner8,
            Floor = 5,
            Equipment = { equipment3, equipment5, equipment8, equipment10, equipment12 }
        };
        var room9 = new Room
        {
            Province = "Opolskie",
            District = "Rakowiec",
            Street = "Grzesiowska",
            Number = "485",
            Flat = 7,
            City = "Opole",
            PostalCode = "16-846",
            GeoLat = 25.784685,
            GeoLon = 25.795486,
            Area = 48,
            MaxNumberOfInhabitants = 1,
            ConstructionYear = 2019,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2024, 2, 20),
            DateForVerificationSorting = new DateTime(2024, 2, 21),
            Owner = owner15,
            Floor = 10,
            Equipment = { equipment1, equipment3, equipment5, equipment7, equipment12 }
        };
        var room10 = new Room
        {
            Province = "Podkarpackie",
            District = "Śródmieście",
            Street = "Jana Pawła II",
            Number = "123",
            Flat = 7,
            City = "Rzeszów",
            PostalCode = "35-064",
            GeoLat = 50.041187,
            GeoLon = 21.999119,
            Area = 48,
            MaxNumberOfInhabitants = 1,
            ConstructionYear = 2007,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2024, 3, 3),
            DateForVerificationSorting = new DateTime(2024, 3, 8),
            Owner = owner15,
            Floor = 5,
            Equipment = { equipment2, equipment5, equipment5, equipment11, equipment12 }
        };
        var room11 = new Room
        {
            Province = "Podlaskie",
            District = "Centrum",
            Street = "Lipowa",
            Number = "58",
            Flat = 7,
            City = "Białystok",
            PostalCode = "15-424",
            GeoLat = 53.132488,
            GeoLon = 23.168840,
            Area = 48,
            MaxNumberOfInhabitants = 1,
            ConstructionYear = 1965,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2024, 3, 25),
            DateForVerificationSorting = new DateTime(2024, 3, 29),
            Owner = owner15,
            Floor = 2,
            Equipment = { equipment2, equipment3, equipment4, equipment9, equipment11 }
        };


        dbContext.Rooms.AddRange(room1, room2, room3, room4, room5, room6, room7, room8, room9, room10, room11);

        await SeedPropertyFiles(room1);
        await SeedPropertyFiles(room2);
        await SeedPropertyFiles(room3);
        await SeedPropertyFiles(room4);
        await SeedPropertyFiles(room5);
        await SeedPropertyFiles(room6);
        await SeedPropertyFiles(room7);
        await SeedPropertyFiles(room8);
        await SeedPropertyFiles(room9);
        await SeedPropertyFiles(room10);
        await SeedPropertyFiles(room11);

        #endregion

        #region Houses

        var house1 = new House
        {
            Province = "Mazowieckie",
            District = "Żoliborz",
            Street = "Stefana Czarnieckiego",
            Number = "40",
            City = "Warszawa",
            PostalCode = "01-548",
            GeoLat = 52.26661245,
            GeoLon = 20.991445019425615,
            Area = 150,
            MaxNumberOfInhabitants = 6,
            ConstructionYear = 2001,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 7, 14),
            VerificationOrRejectionDate = new DateTime(2023, 7, 14),
            Owner = owner1,
            NumberOfRooms = 5,
            NumberOfFloors = 3,
            PlotArea = 200,
            Equipment = { equipment1, equipment2, equipment5, equipment6, equipment7 }
        };
        var house2 = new House
        {
            Province = "Mazowieckie",
            District = "Śródmieście",
            Street = "Lekarska",
            Number = "6",
            City = "Warszawa",
            PostalCode = "00-610",
            GeoLat = 52.2178477,
            GeoLon = 21.006591811162405,
            Area = 110,
            MaxNumberOfInhabitants = 4,
            ConstructionYear = 1989,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 8, 9),
            VerificationOrRejectionDate = new DateTime(2023, 8, 9),
            Owner = owner2,
            NumberOfRooms = 4,
            NumberOfFloors = 2,
            PlotArea = 120,
            Equipment = { equipment3, equipment4, equipment8, equipment10, equipment11 }
        };
        var house3 = new House
        {
            Province = "Mazowieckie",
            District = "Mokotów",
            Street = "Wacława Żenczykowskiego",
            Number = "4",
            City = "Warszawa",
            PostalCode = "00-707",
            GeoLat = 52.213129249999994,
            GeoLon = 21.064734397488117,
            Area = 180,
            MaxNumberOfInhabitants = 6,
            ConstructionYear = 1999,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 8, 14),
            VerificationOrRejectionDate = new DateTime(2023, 8, 14),
            Owner = owner2,
            NumberOfRooms = 7,
            NumberOfFloors = 4,
            PlotArea = 210,
            Equipment = { equipment2, equipment4, equipment6, equipment9 }
        };
        var house4 = new House //owner id 4, offer id 28
        {
            Province = "Mazowieckie",
            District = "Ursynów",
            Street = "Modelowa",
            Number = "1",
            City = "Warszawa",
            PostalCode = "02-797",
            GeoLat = 52.1537627,
            GeoLon = 21.05884787182448,
            Area = 120,
            MaxNumberOfInhabitants = 4,
            ConstructionYear = 1997,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 4, 5),
            DateForVerificationSorting = null,
            VerificationOrRejectionDate = new DateTime(2023, 4, 7),
            Owner = owner3,
            NumberOfRooms = 4,
            NumberOfFloors = 3,
            PlotArea = 130,
            Equipment = { equipment1, equipment3, equipment5, equipment7, equipment8 }
        };
        var house5 = new House
        {
            Province = "Mazowieckie",
            District = "Włochy",
            Street = "Orzechowa",
            Number = "34",
            City = "Warszawa",
            PostalCode = "02-244",
            GeoLat = 52.1905611,
            GeoLon = 20.94665133190366,
            Area = 130,
            MaxNumberOfInhabitants = 4,
            ConstructionYear = 2010,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 7, 12),
            VerificationOrRejectionDate = new DateTime(2023, 7, 12),
            Owner = owner4,
            NumberOfRooms = 3,
            NumberOfFloors = 1,
            PlotArea = 140,
            Equipment = { equipment2, equipment6, equipment10, equipment11 }
        };
        var house6 = new House
        {
            Province = "Małopolskie",
            District = "Stare Miasto",
            Street = "Grodzka",
            Number = "10",
            City = "Kraków",
            PostalCode = "31-044",
            GeoLat = 50.060202,
            GeoLon = 19.9348,
            Area = 180,
            MaxNumberOfInhabitants = 8,
            ConstructionYear = 1985,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 5, 28),
            VerificationOrRejectionDate = new DateTime(2023, 5, 28),
            Owner = owner5,
            NumberOfRooms = 6,
            NumberOfFloors = 2,
            PlotArea = 250,
            Equipment = { equipment3, equipment4, equipment6, equipment8 }
        };
        var house7 = new House
        {
            Province = "Wielkopolskie",
            District = "Jeżyce",
            Street = "Święty Marcin",
            Number = "80",
            City = "Poznań",
            PostalCode = "61-809",
            GeoLat = 52.405333,
            GeoLon = 16.933526,
            Area = 130,
            MaxNumberOfInhabitants = 5,
            ConstructionYear = 1998,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 8, 10),
            VerificationOrRejectionDate = new DateTime(2023, 8, 10),
            Owner = owner6,
            NumberOfRooms = 4,
            NumberOfFloors = 2,
            PlotArea = 180,
            Equipment = { equipment1, equipment5, equipment7 }
        };
        var house8 = new House
        {
            Province = "Śląskie",
            District = "Centrum",
            Street = "Mariacka",
            Number = "15",
            City = "Katowice",
            PostalCode = "40-014",
            GeoLat = 50.2598,
            GeoLon = 19.0238,
            Area = 200,
            MaxNumberOfInhabitants = 7,
            ConstructionYear = 2005,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 9, 5),
            VerificationOrRejectionDate = new DateTime(2023, 9, 5),
            Owner = owner8,
            NumberOfRooms = 7,
            NumberOfFloors = 3,
            PlotArea = 300,
            Equipment = { equipment2, equipment3, equipment8 }
        };
        var house9 = new House
        {
            Province = "Pomorskie",
            District = "Oliwa",
            Street = "Opata Jacka Rybińskiego",
            Number = "23",
            City = "Gdańsk",
            PostalCode = "80-320",
            GeoLat = 54.402348,
            GeoLon = 18.567110,
            Area = 200,
            MaxNumberOfInhabitants = 6,
            ConstructionYear = 2005,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2024, 4, 5),
            VerificationOrRejectionDate = new DateTime(2024, 4, 6),
            Owner = owner15,
            NumberOfRooms = 3,
            NumberOfFloors = 1,
            PlotArea = 84,
            Equipment = { equipment5, equipment6, equipment9 }
        };
        var house10 = new House
        {
            Province = "Świętokrzyskie",
            District = "Sady",
            Street = "Sienkiewicza",
            Number = "12",
            City = "Kielce",
            PostalCode = "25-333",
            GeoLat = 50.866077,
            GeoLon = 20.628569,
            Area = 200,
            MaxNumberOfInhabitants = 9,
            ConstructionYear = 2005,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2024, 4, 10),
            VerificationOrRejectionDate = new DateTime(2024, 4, 13),
            Owner = owner15,
            NumberOfRooms = 9,
            NumberOfFloors = 3,
            PlotArea = 306,
            Equipment = { equipment4, equipment6, equipment8 }
        };
        var house11 = new House
        {
            Province = "Warmińsko-Mazurskie",
            District = "Śródmieście",
            Street = "Kościuszki",
            Number = "18",
            City = "Olsztyn",
            PostalCode = "10-503",
            GeoLat = 53.778422,
            GeoLon = 20.481449,
            Area = 200,
            MaxNumberOfInhabitants = 5,
            ConstructionYear = 2010,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2024, 4, 20),
            VerificationOrRejectionDate = new DateTime(2024, 4, 22),
            Owner = owner15,
            NumberOfRooms = 5,
            NumberOfFloors = 2,
            PlotArea = 150,
            Equipment = { equipment5, equipment6, equipment9 }
        };
        var house12 = new House
        {
            Province = "Zachodniopomorskie",
            District = "Pogodno",
            Street = "Wojska Polskiego",
            Number = "45",
            City = "Szczecin",
            PostalCode = "70-473",
            GeoLat = 53.42894,
            GeoLon = 14.55302,
            Area = 200,
            MaxNumberOfInhabitants = 12,
            ConstructionYear = 2005,
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2024, 4, 12),
            VerificationOrRejectionDate = new DateTime(2024, 4, 14),
            Owner = owner15,
            NumberOfRooms = 12,
            NumberOfFloors = 3,
            PlotArea = 406,
            Equipment = { equipment5, equipment6, equipment9 }
        };

        dbContext.Houses.AddRange(house1, house2, house3, house4, house5, house6, house7, house8, house9, house10, house11, house12);

        await SeedPropertyFiles(house1);
        await SeedPropertyFiles(house2);
        await SeedPropertyFiles(house3);
        await SeedPropertyFiles(house4);
        await SeedPropertyFiles(house5);
        await SeedPropertyFiles(house6);
        await SeedPropertyFiles(house7);
        await SeedPropertyFiles(house8);
        await SeedPropertyFiles(house9);
        await SeedPropertyFiles(house10);
        await SeedPropertyFiles(house11);
        await SeedPropertyFiles(house12);

        #endregion

        #region Offers

        var offer1 = new Offer
        {
            Date = new DateTime(2023, 10, 10),
            OfferStatus = OfferStatus.Rented,
            Price = 2000,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 12, 1),
            EndDate = new DateTime(2024, 7, 1),
            NumberOfInterested = 9,
            Regulations = "placeholder",
            Property = flat1
        };
        var offer2 = new Offer
        {
            Date = new DateTime(2023, 10, 5),
            OfferStatus = OfferStatus.Rented,
            Price = 2200,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 12, 15),
            EndDate = new DateTime(2024, 3, 15),
            NumberOfInterested = 10,
            Regulations = "placeholder",
            Property = flat2
        };
        var offer3 = new Offer
        {
            Date = new DateTime(2023, 9, 25),
            OfferStatus = OfferStatus.Rented,
            Price = 1800,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 12, 10),
            EndDate = new DateTime(2024, 6, 10),
            NumberOfInterested = 8,
            Regulations = "placeholder",
            Property = flat4
        };
        var offer4 = new Offer //property id 6
        {
            Date = new DateTime(2023, 9, 20),
            OfferStatus = OfferStatus.Rented,
            Price = 2100,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 12, 1),
            EndDate = new DateTime(2024, 8, 1),
            NumberOfInterested = 9,
            Regulations = "placeholder",
            Property = flat6
        };
        var offer5 = new Offer //property id 7
        {
            Date = new DateTime(2023, 9, 15),
            OfferStatus = OfferStatus.Current,
            Price = 1900,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2024, 6, 1),
            NumberOfInterested = 10,
            Regulations = "placeholder",
            Property = flat7
        };
        var offer6 = new Offer
        {
            Date = new DateTime(2023, 9, 10),
            OfferStatus = OfferStatus.Current,
            Price = 2050,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2024, 2, 1),
            EndDate = new DateTime(2024, 10, 1),
            NumberOfInterested = 8,
            Regulations = "placeholder",
            Property = flat8
        };
        var offer7 = new Offer
        {
            Date = new DateTime(2023, 9, 5),
            OfferStatus = OfferStatus.Current,
            Price = 1950,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 12, 20),
            EndDate = new DateTime(2024, 10, 20),
            NumberOfInterested = 9,
            Regulations = "placeholder",
            Property = flat9
        };
        var offer8 = new Offer
        {
            Date = new DateTime(2023, 8, 25),
            OfferStatus = OfferStatus.Current,
            Price = 2100,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2024, 1, 10),
            EndDate = new DateTime(2024, 6, 10),
            NumberOfInterested = 8,
            Regulations = "placeholder",
            Property = flat10
        };
        var offer9 = new Offer
        {
            Date = new DateTime(2023, 8, 20),
            OfferStatus = OfferStatus.Current,
            Price = 2200,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 12, 10),
            EndDate = new DateTime(2024, 8, 10),
            NumberOfInterested = 9,
            Regulations = "placeholder",
            Property = house1
        };
        var offer10 = new Offer
        {
            Date = new DateTime(2023, 8, 15),
            OfferStatus = OfferStatus.Current,
            Price = 1800,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2024, 3, 1),
            EndDate = new DateTime(2024, 6, 1),
            NumberOfInterested = 10,
            Regulations = "placeholder",
            Property = house2
        };
        var offer11 = new Offer
        {
            Date = new DateTime(2023, 8, 10),
            OfferStatus = OfferStatus.Current,
            Price = 2050,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 12, 15),
            EndDate = new DateTime(2024, 8, 15),
            NumberOfInterested = 8,
            Regulations = "placeholder",
            Property = house3
        };
        var offer12 = new Offer
        {
            Date = new DateTime(2023, 8, 5),
            OfferStatus = OfferStatus.Current,
            Price = 1950,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 12, 1),
            EndDate = new DateTime(2024, 9, 1),
            NumberOfInterested = 9,
            Regulations = "placeholder",
            Property = house5
        };
        var offer13 = new Offer
        {
            Date = new DateTime(2023, 7, 25),
            OfferStatus = OfferStatus.Current,
            Price = 1900,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 12, 1),
            EndDate = new DateTime(2024, 4, 1),
            NumberOfInterested = 8,
            Regulations = "placeholder",
            Property = room1
        };
        var offer14 = new Offer
        {
            Date = new DateTime(2023, 7, 20),
            OfferStatus = OfferStatus.Current,
            Price = 2200,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2024, 1, 15),
            EndDate = new DateTime(2024, 7, 15),
            NumberOfInterested = 9,
            Regulations = "placeholder",
            Property = room2
        };
        var offer15 = new Offer
        {
            Date = new DateTime(2023, 7, 15),
            OfferStatus = OfferStatus.Current,
            Price = 2000,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2024, 2, 1),
            EndDate = new DateTime(2024, 8, 1),
            NumberOfInterested = 10,
            Regulations = "placeholder",
            Property = room5
        };
        var offer16 = new Offer
        {
            Date = new DateTime(2023, 10, 10),
            OfferStatus = OfferStatus.Current,
            Price = 2000,
            Deposit = 1500,
            Description = "Przestronne mieszkanie w centrum Krakowa.",
            StartDate = new DateTime(2023, 12, 1),
            EndDate = new DateTime(2024, 7, 1),
            NumberOfInterested = 9,
            Regulations = "Brak zwierząt.",
            Property = flat11
        };
        var offer17 = new Offer
        {
            Date = new DateTime(2023, 9, 25),
            OfferStatus = OfferStatus.Current,
            Price = 1800,
            Deposit = 2000,
            Description = "Dom w świetnej lokalizacji, niedaleko Rynku.",
            StartDate = new DateTime(2023, 11, 15),
            EndDate = new DateTime(2024, 10, 15),
            NumberOfInterested = 5,
            Regulations = "Tylko osoby niepalące.",
            Property = house6
        };
        var offer18 = new Offer
        {
            Date = new DateTime(2023, 11, 5),
            OfferStatus = OfferStatus.Current,
            Price = 1500,
            Deposit = 1000,
            Description = "Przytulne mieszkanie w pobliżu centrum Poznania.",
            StartDate = new DateTime(2023, 12, 5),
            EndDate = new DateTime(2024, 6, 5),
            NumberOfInterested = 3,
            Regulations = "Wynajem na min. rok.",
            Property = flat12
        };
        var offer19 = new Offer
        {
            Date = new DateTime(2023, 10, 20),
            OfferStatus = OfferStatus.Current,
            Price = 2500,
            Deposit = 2000,
            Description = "Duży dom z ogrodem w centrum Katowic.",
            StartDate = new DateTime(2023, 11, 30),
            EndDate = new DateTime(2024, 11, 30),
            NumberOfInterested = 7,
            Regulations = "Brak zwierząt domowych.",
            Property = house7
        };
        var offer20 = new Offer
        {
            Date = new DateTime(2023, 12, 1),
            OfferStatus = OfferStatus.Current,
            Price = 1200,
            Deposit = 800,
            Description = "Przyjemne mieszkanie w samym sercu Wrocławia.",
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 12, 31),
            NumberOfInterested = 2,
            Regulations = "Wynajem krótkoterminowy.",
            Property = flat13
        };
        var offer21 = new Offer
        {
            Date = new DateTime(2023, 9, 10),
            OfferStatus = OfferStatus.Current,
            Price = 2200,
            Deposit = 1800,
            Description = "Elegancki apartament z widokiem na panoramę miasta.",
            StartDate = new DateTime(2023, 11, 1),
            EndDate = new DateTime(2024, 10, 31),
            NumberOfInterested = 6,
            Regulations = "Tylko dla osób pracujących.",
            Property = flat14
        };
        var offer22 = new Offer
        {
            Date = new DateTime(2023, 11, 15),
            OfferStatus = OfferStatus.Current,
            Price = 3000,
            Deposit = 2500,
            Description = "Piękny dom z ogrodem w zacisznej okolicy.",
            StartDate = new DateTime(2023, 12, 15),
            EndDate = new DateTime(2024, 12, 15),
            NumberOfInterested = 4,
            Regulations = "Wynajem długoterminowy.",
            Property = house8
        };
        var offer23 = new Offer
        {
            Date = new DateTime(2023, 10, 25),
            OfferStatus = OfferStatus.Current,
            Price = 1600,
            Deposit = 1200,
            Description = "Przestronne mieszkanie blisko centrum Łodzi.",
            StartDate = new DateTime(2023, 11, 25),
            EndDate = new DateTime(2024, 11, 25),
            NumberOfInterested = 8,
            Regulations = "Brak zwierząt.",
            Property = room7
        };
        var offer24 = new Offer
        {
            Date = new DateTime(2023, 12, 5),
            OfferStatus = OfferStatus.Current,
            Price = 2800,
            Deposit = 2000,
            Description = "Wygodny apartament w pobliżu Rynku.",
            StartDate = new DateTime(2024, 1, 5),
            EndDate = new DateTime(2024, 12, 5),
            NumberOfInterested = 3,
            Regulations = "Tylko osoby niepalące",
            Property = room8
        };
        var offer25 = new Offer
        {
            Date = new DateTime(2023, 11, 20),
            OfferStatus = OfferStatus.Current,
            Price = 3500,
            Deposit = 3000,
            Description = "Nowoczesny dom w spokojnej okolicy.",
            StartDate = new DateTime(2023, 12, 20),
            EndDate = new DateTime(2024, 12, 20),
            NumberOfInterested = 6,
            Regulations = "Brak zwierząt.",
            Property = house8
        };
        var offer26 = new Offer //property id 5
        {
            Date = new DateTime(2023, 6, 15),
            OfferStatus = OfferStatus.Current,
            Price = 2100,
            Deposit = 1500,
            Description = "wynajem nr 26",
            StartDate = new DateTime(2023, 8, 1),
            EndDate = new DateTime(2024, 5, 1),
            NumberOfInterested = 3,
            Regulations = "regulacje pod wynajem 26",
            Property = flat5
        };
        var offer27 = new Offer //property id 18
        {
            Date = new DateTime(2023, 9, 17),
            OfferStatus = OfferStatus.Current,
            Price = 2100,
            Deposit = 1500,
            Description = "wynajem nr 27",
            StartDate = new DateTime(2023, 10, 1),
            EndDate = new DateTime(2024, 5, 10),
            NumberOfInterested = 2,
            Regulations = "regulacje pod wynajem 27",
            Property = room4
        };
        var offer28 = new Offer //property id 26
        {
            Date = new DateTime(2023, 9, 20),
            OfferStatus = OfferStatus.Current,
            Price = 2100,
            Deposit = 1500,
            Description = "wynajem nr 28",
            StartDate = new DateTime(2023, 12, 1),
            EndDate = new DateTime(2024, 6, 1),
            NumberOfInterested = 9,
            Regulations = "regulacje pod wynajem 28",
            Property = house4
        };
        var offer29 = new Offer 
        {
            Date = new DateTime(2024, 6, 1),
            OfferStatus = OfferStatus.Waiting,
            Price = 1990,
            Deposit = 1000,
            Description = "wynajem nr 29",
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2025, 7, 1),
            NumberOfInterested = 0,
            Regulations = "regulacje pod wynajem 29",
            Property = flat15
        };
        var offer30 = new Offer
        {
            Date = new DateTime(2024, 6, 1),
            OfferStatus = OfferStatus.Waiting,
            Price = 1990,
            Deposit = 1000,
            Description = "wynajem nr 30",
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2025, 7, 1),
            NumberOfInterested = 0,
            Regulations = "regulacje pod wynajem 30",
            Property = flat16
        };
        var offer31 = new Offer
        {
            Date = new DateTime(2024, 6, 1),
            OfferStatus = OfferStatus.Waiting,
            Price = 1990,
            Deposit = 1000,
            Description = "wynajem nr 31",
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2025, 7, 1),
            NumberOfInterested = 0,
            Regulations = "regulacje pod wynajem 31",
            Property = flat17
        };
        var offer32 = new Offer
        {
            Date = new DateTime(2024, 6, 1),
            OfferStatus = OfferStatus.Waiting,
            Price = 1990,
            Deposit = 1000,
            Description = "wynajem nr 32",
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2025, 7, 1),
            NumberOfInterested = 0,
            Regulations = "regulacje pod wynajem 32",
            Property = room9
        };
        var offer33 = new Offer
        {
            Date = new DateTime(2024, 6, 1),
            OfferStatus = OfferStatus.Waiting,
            Price = 1990,
            Deposit = 1000,
            Description = "wynajem nr 33",
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2025, 7, 1),
            NumberOfInterested = 0,
            Regulations = "regulacje pod wynajem 33",
            Property = room10
        };
        var offer34 = new Offer
        {
            Date = new DateTime(2024, 6, 1),
            OfferStatus = OfferStatus.Waiting,
            Price = 1990,
            Deposit = 1000,
            Description = "wynajem nr 34",
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2025, 7, 1),
            NumberOfInterested = 0,
            Regulations = "regulacje pod wynajem 34",
            Property = room11
        };
        var offer35 = new Offer
        {
            Date = new DateTime(2024, 6, 1),
            OfferStatus = OfferStatus.Waiting,
            Price = 1990,
            Deposit = 1000,
            Description = "wynajem nr 35",
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2025, 7, 1),
            NumberOfInterested = 0,
            Regulations = "regulacje pod wynajem 35",
            Property = house9
        };
        var offer36 = new Offer
        {
            Date = new DateTime(2024, 6, 1),
            OfferStatus = OfferStatus.Waiting,
            Price = 1990,
            Deposit = 1000,
            Description = "wynajem nr 36",
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2025, 7, 1),
            NumberOfInterested = 0,
            Regulations = "regulacje pod wynajem 36",
            Property = house10
        };
        var offer37 = new Offer
        {
            Date = new DateTime(2024, 6, 1),
            OfferStatus = OfferStatus.Waiting,
            Price = 1990,
            Deposit = 1000,
            Description = "wynajem nr 37",
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2025, 7, 1),
            NumberOfInterested = 0,
            Regulations = "regulacje pod wynajem 37",
            Property = house11
        };
        var offer38 = new Offer
        {
            Date = new DateTime(2024, 6, 1),
            OfferStatus = OfferStatus.Waiting,
            Price = 1990,
            Deposit = 1000,
            Description = "wynajem nr 38",
            StartDate = new DateTime(2024, 7, 1),
            EndDate = new DateTime(2025, 7, 1),
            NumberOfInterested = 0,
            Regulations = "regulacje pod wynajem 38",
            Property = house12
        };


        dbContext.Offers.AddRange(offer1, offer2, offer3, offer4, offer5, offer6, offer7, offer8, offer9, offer10, offer11, offer12, offer13, offer14, offer15, offer16, offer17, offer18, offer19, offer20, offer21, offer22, offer23, offer24, offer25, offer26, offer27, offer28, offer29, offer30, offer31, offer32, offer33, offer34, offer35, offer36, offer37, offer38);

        #endregion

        #region OfferPromotions

        var offerPromotion1 = new OfferPromotion
        {
            StartDate = new DateTime(2023, 11, 26),
            EndDate = new DateTime(2024, 11, 30),
            Price = 50,
            Offer = offer5
        };
        var offerPromotion2 = new OfferPromotion
        {
            StartDate = new DateTime(2023, 11, 26),
            EndDate = new DateTime(2024, 11, 30),
            Price = 50,
            Offer = offer6
        };
        var offerPromotion3 = new OfferPromotion
        {
            StartDate = new DateTime(2022, 11, 26),
            EndDate = new DateTime(2023, 11, 30),
            Price = 50,
            Offer = offer7
        };

        dbContext.OfferPromotions.AddRange(offerPromotion1, offerPromotion2, offerPromotion3);

        #endregion

        #region OwnerOfferSurveys

        var surveyOwnerOffer1 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer1
        };
        var surveyOwnerOffer2 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = false,
            AnimalsAllowed = false,
            Gender = Gender.Male,
            Offer = offer2
        };
        var surveyOwnerOffer3 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Female,
            Offer = offer3
        };
        var surveyOwnerOffer4 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer4
        };
        var surveyOwnerOffer5 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Male,
            Offer = offer5
        };
        var surveyOwnerOffer6 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = false,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer6
        };
        var surveyOwnerOffer7 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = false,
            AnimalsAllowed = false,
            Gender = Gender.Both,
            Offer = offer7
        };
        var surveyOwnerOffer8 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer8
        };
        var surveyOwnerOffer9 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Female,
            Offer = offer9
        };
        var surveyOwnerOffer10 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = false,
            AnimalsAllowed = false,
            Gender = Gender.Both,
            Offer = offer10
        };
        var surveyOwnerOffer11 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Male,
            Offer = offer11
        };
        var surveyOwnerOffer12 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Female,
            Offer = offer12
        };
        var surveyOwnerOffer13 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = false,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer13
        };
        var surveyOwnerOffer14 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = false,
            AnimalsAllowed = false,
            Gender = Gender.Both,
            Offer = offer14
        };
        var surveyOwnerOffer15 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer15
        };
        var surveyOwnerOffer16 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer16
        };
        var surveyOwnerOffer17 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer17
        };
        var surveyOwnerOffer18 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer18
        };
        var surveyOwnerOffer19 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer19
        };
        var surveyOwnerOffer20 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer20
        };
        var surveyOwnerOffer21 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer21
        };
        var surveyOwnerOffer22 = new SurveyOwnerOffer
        {
            SmokingAllowed = true,
            PartiesAllowed = true,
            AnimalsAllowed = true,
            Gender = Gender.Both,
            Offer = offer22
        };
        var surveyOwnerOffer23 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Female,
            Offer = offer23
        };
        var surveyOwnerOffer24 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Female,
            Offer = offer24
        };
        var surveyOwnerOffer25 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Female,
            Offer = offer25
        };
        var surveyOwnerOffer26 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Female,
            Offer = offer26
        };
        var surveyOwnerOffer27 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Female,
            Offer = offer27
        };
        var surveyOwnerOffer28 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Female,
            Offer = offer28
        };
        var surveyOwnerOffer29 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Female,
            Offer = offer29
        };
        var surveyOwnerOffer30 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = true,
            AnimalsAllowed = false,
            Gender = Gender.Female,
            Offer = offer30
        };
        var surveyOwnerOffer31 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = false,
            AnimalsAllowed = true,
            Gender = Gender.Male,
            Offer = offer31
        };
        var surveyOwnerOffer32 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = false,
            AnimalsAllowed = true,
            Gender = Gender.Male,
            Offer = offer32
        };
        var surveyOwnerOffer33 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = false,
            AnimalsAllowed = true,
            Gender = Gender.Male,
            Offer = offer33
        };
        var surveyOwnerOffer34 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = false,
            AnimalsAllowed = true,
            Gender = Gender.Male,
            Offer = offer34
        };
        var surveyOwnerOffer35 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = false,
            AnimalsAllowed = true,
            Gender = Gender.Male,
            Offer = offer35
        };
        var surveyOwnerOffer36 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = false,
            AnimalsAllowed = true,
            Gender = Gender.Male,
            Offer = offer36
        };
        var surveyOwnerOffer37 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = false,
            AnimalsAllowed = true,
            Gender = Gender.Male,
            Offer = offer37
        };
        var surveyOwnerOffer38 = new SurveyOwnerOffer
        {
            SmokingAllowed = false,
            PartiesAllowed = false,
            AnimalsAllowed = true,
            Gender = Gender.Male,
            Offer = offer38
        };

        dbContext.OwnerOfferSurveys.AddRange(surveyOwnerOffer1, surveyOwnerOffer2, surveyOwnerOffer3, surveyOwnerOffer4, surveyOwnerOffer5, surveyOwnerOffer6, surveyOwnerOffer7, surveyOwnerOffer8, surveyOwnerOffer9, surveyOwnerOffer10, surveyOwnerOffer11, surveyOwnerOffer12, surveyOwnerOffer13, surveyOwnerOffer14, surveyOwnerOffer15, surveyOwnerOffer16, surveyOwnerOffer17, surveyOwnerOffer18, surveyOwnerOffer19, surveyOwnerOffer20, surveyOwnerOffer2, surveyOwnerOffer21, surveyOwnerOffer22, surveyOwnerOffer23, surveyOwnerOffer24, surveyOwnerOffer25, surveyOwnerOffer26, surveyOwnerOffer27, surveyOwnerOffer28, surveyOwnerOffer29, surveyOwnerOffer30, surveyOwnerOffer31, surveyOwnerOffer32, surveyOwnerOffer33, surveyOwnerOffer34, surveyOwnerOffer35, surveyOwnerOffer36, surveyOwnerOffer37, surveyOwnerOffer38);

        #endregion

        #region StudentSurveys

        var surveyStudent1 = new SurveyStudent
        {
            Party = 1,
            Smoking = true,
            Sociability = true,
            Animals = true,
            Vegan = false,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 3,
            RoommateGender = 0,
            MinRoommateAge = 20,
            MaxRoommateAge = 30,
            City = "Warszawa",
            Student = student1
        };
        var surveyStudent2 = new SurveyStudent
        {
            Party = 1,
            Smoking = true,
            Sociability = true,
            Animals = true,
            Vegan = false,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 3,
            RoommateGender = 0,
            MinRoommateAge = 30,
            MaxRoommateAge = 50,
            City = "Warszawa",
            Student = student2
        };
        var surveyStudent3 = new SurveyStudent
        {
            Party = 1,
            Smoking = true,
            Sociability = true,
            Animals = true,
            Vegan = false,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 3,
            RoommateGender = 0,
            MinRoommateAge = 20,
            MaxRoommateAge = 150,
            City = "Warszawa",
            Student = student3
        };
        var surveyStudent4 = new SurveyStudent
        {
            Party = 1,
            Smoking = true,
            Sociability = true,
            Animals = true,
            Vegan = false,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 3,
            RoommateGender = 0,
            MinRoommateAge = 70,
            MaxRoommateAge = 100,
            City = "Warszawa",
            Student = student4
        };
        var surveyStudent5 = new SurveyStudent
        {
            Party = 3,
            Smoking = false,
            Sociability = true,
            Animals = false,
            Vegan = true,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 3,
            RoommateGender = 0,
            MinRoommateAge = 25,
            MaxRoommateAge = 50,
            City = "Warszawa",
            Student = student5
        };
        var surveyStudent6 = new SurveyStudent
        {
            Party = 2,
            Smoking = false,
            Sociability = true,
            Animals = true,
            Vegan = true,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 4,
            RoommateGender = 0,
            MinRoommateAge = 22,
            MaxRoommateAge = 28,
            City = "Warszawa",
            Student = student6
        };
        var surveyStudent7 = new SurveyStudent
        {
            Party = 4,
            Smoking = true,
            Sociability = false,
            Animals = true,
            Vegan = false,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 2,
            RoommateGender = 0,
            MinRoommateAge = 28,
            MaxRoommateAge = 40,
            City = "Warszawa",
            Student = student7
        };
        var surveyStudent8 = new SurveyStudent
        {
            Party = 2,
            Smoking = false,
            Sociability = true,
            Animals = false,
            Vegan = true,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 3,
            RoommateGender = 0,
            MinRoommateAge = 24,
            MaxRoommateAge = 32,
            City = "Warszawa",
            Student = student8
        };
        var surveyStudent9 = new SurveyStudent
        {
            Party = 3,
            Smoking = false,
            Sociability = true,
            Animals = true,
            Vegan = false,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 2,
            RoommateGender = 0,
            MinRoommateAge = 26,
            MaxRoommateAge = 36,
            City = "Warszawa",
            Student = student9
        };
        var surveyStudent10 = new SurveyStudent
        {
            Party = 1,
            Smoking = true,
            Sociability = true,
            Animals = false,
            Vegan = false,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 4,
            RoommateGender = 0,
            MinRoommateAge = 23,
            MaxRoommateAge = 29,
            City = "Warszawa",
            Student = student10
        };
        var surveyStudent11 = new SurveyStudent
        {
            Party = 2,
            Smoking = false,
            Sociability = true,
            Animals = true,
            Vegan = false,
            LookingForRoommate = false,
            MaxNumberOfRoommates = 3,
            RoommateGender = 0,
            MinRoommateAge = 24,
            MaxRoommateAge = 30,
            City = "Kraków",
            Student = student11
        };
        var surveyStudent12 = new SurveyStudent
        {
            Party = 3,
            Smoking = true,
            Sociability = true,
            Animals = true,
            Vegan = true,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 2,
            RoommateGender = 0,
            MinRoommateAge = 25,
            MaxRoommateAge = 28,
            City = "Gdańsk",
            Student = student12
        };
        var surveyStudent13 = new SurveyStudent
        {
            Party = 1,
            Smoking = false,
            Sociability = false,
            Animals = false,
            Vegan = false,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 1,
            RoommateGender = 0,
            MinRoommateAge = 26,
            MaxRoommateAge = 27,
            City = "Poznań",
            Student = student13
        };
        var surveyStudent14 = new SurveyStudent
        {
            Party = 2,
            Smoking = true,
            Sociability = true,
            Animals = false,
            Vegan = false,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 4,
            RoommateGender = 0,
            MinRoommateAge = 23,
            MaxRoommateAge = 29,
            City = "Warszawa",
            Student = student14
        };
        var surveyStudent15 = new SurveyStudent
        {
            Party = 3,
            Smoking = false,
            Sociability = true,
            Animals = true,
            Vegan = true,
            LookingForRoommate = false,
            MaxNumberOfRoommates = 3,
            RoommateGender = 0,
            MinRoommateAge = 24,
            MaxRoommateAge = 30,
            City = "Kraków",
            Student = student15
        };
        var surveyStudent16 = new SurveyStudent
        {
            Party = 1,
            Smoking = true,
            Sociability = true,
            Animals = true,
            Vegan = false,
            LookingForRoommate = false,
            MaxNumberOfRoommates = 2,
            RoommateGender = 0,
            MinRoommateAge = 25,
            MaxRoommateAge = 28,
            City = "Gdańsk",
            Student = student16
        };
        var surveyStudent17 = new SurveyStudent
        {
            Party = 2,
            Smoking = false,
            Sociability = false,
            Animals = false,
            Vegan = false,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 1,
            RoommateGender = 0,
            MinRoommateAge = 26,
            MaxRoommateAge = 27,
            City = "Poznań",
            Student = student17
        };
        var surveyStudent18 = new SurveyStudent
        {
            Party = 3,
            Smoking = true,
            Sociability = true,
            Animals = false,
            Vegan = false,
            LookingForRoommate = true,
            MaxNumberOfRoommates = 4,
            RoommateGender = 0,
            MinRoommateAge = 23,
            MaxRoommateAge = 29,
            City = "Warszawa",
            Student = student18
        };
        var surveyStudent19 = new SurveyStudent
        {
            Party = 1,
            Smoking = false,
            Sociability = true,
            Animals = true,
            Vegan = true,
            LookingForRoommate = false,
            MaxNumberOfRoommates = 3,
            RoommateGender = 0,
            MinRoommateAge = 24,
            MaxRoommateAge = 30,
            City = "Kraków",
            Student = student19
        };
        var surveyStudent20 = new SurveyStudent
        {
            Party = 2,
            Smoking = true,
            Sociability = true,
            Animals = true,
            Vegan = false,
            LookingForRoommate = false,
            MaxNumberOfRoommates = 2,
            RoommateGender = 0,
            MinRoommateAge = 25,
            MaxRoommateAge = 28,
            City = "Gdańsk",
            Student = student20
        };


        dbContext.StudentSurveys.AddRange(surveyStudent1, surveyStudent2, surveyStudent3, surveyStudent4, surveyStudent5, surveyStudent6, surveyStudent7, surveyStudent8, surveyStudent9, surveyStudent10, surveyStudent11, surveyStudent12, surveyStudent13, surveyStudent14, surveyStudent15, surveyStudent16, surveyStudent17, surveyStudent18, surveyStudent19, surveyStudent20);

        #endregion

        #region Meetings

        var meeting1 = new Meeting
        {
            Date = new DateTime(2023, 12, 28),
            Place = "placeholder",
            Reason = "placeholder",
            Offer = offer4,
            Student = student2
        };

        dbContext.Meetings.AddRange(meeting1);

        #endregion

        #region Matcher

        var match1 = new Matcher
        {
            IsStudent1Interested = true,
            IsStudent2Interested = false,
            Student1 = student3,
            Student2 = student4
        };
        var match2 = new Matcher
        {
            IsStudent1Interested = true,
            IsStudent2Interested = true,
            Student1 = student3,
            Student2 = student5
        };
        var match3 = new Matcher
        {
            IsStudent1Interested = true,
            IsStudent2Interested = false,
            Student1 = student4,
            Student2 = student5
        };
        var match4 = new Matcher
        {
            IsStudent1Interested = true,
            IsStudent2Interested = true,
            Student1 = student3,
            Student2 = student7
        };
        var match5 = new Matcher
        {
            IsStudent1Interested = true,
            IsStudent2Interested = true,
            Student1 = student3,
            Student2 = student10
        };
        var match6 = new Matcher
        {
            IsStudent1Interested = true,
            IsStudent2Interested = false,
            Student1 = student1,
            Student2 = student3
        };

        dbContext.Matcher.AddRange(match1, match2, match3, match4, match5, match6);

        #endregion

        #region TechnicalProblem

        var technicalProblem1 = new TechnicalProblem
        {
            Kind = TechnicalProblemType.Other,
            Description = "blablalba",
            Date = new DateTime(2024, 1, 08),
            Solved = true,
            User = student3
        };
        var technicalProblem2 = new TechnicalProblem
        {
            Kind = TechnicalProblemType.Payment,
            Description = "2blablalba",
            Date = new DateTime(2024, 1, 04),
            Solved = true,
            User = student4
        };
        var technicalProblem3 = new TechnicalProblem
        {
            Kind = TechnicalProblemType.Payment,
            Description = "3blablalba",
            Date = new DateTime(2024, 1, 04),
            Solved = false,
            User = student4
        };
        var technicalProblem4 = new TechnicalProblem
        {
            Kind = TechnicalProblemType.Payment,
            Description = "4blablalba",
            Date = new DateTime(2023, 12, 30),
            Solved = false,
            User = student4
        };
        var technicalProblem5 = new TechnicalProblem
        {
            Kind = TechnicalProblemType.Payment,
            Description = "5blablalba",
            Date = new DateTime(2023, 12, 29),
            Solved = false,
            User = student4
        };
        var technicalProblem6 = new TechnicalProblem
        {
            Kind = TechnicalProblemType.ApplicationOperation,
            Description = "Technical Problem o numerze 6",
            Date = new DateTime(2024, 2, 29),
            Solved = false,
            User = student6
        };
        var technicalProblem7 = new TechnicalProblem
        {
            Kind = TechnicalProblemType.AccountPerformance,
            Description = "Technical Problem o numerze 7",
            Date = new DateTime(2024, 3, 29),
            Solved = true,
            User = student7
        };
        var technicalProblem8 = new TechnicalProblem
        {
            Kind = TechnicalProblemType.ApplicationOperation,
            Description = "Technical Problem o numerze 8",
            Date = new DateTime(2024, 3, 14),
            Solved = false,
            User = student8
        };
        var technicalProblem9 = new TechnicalProblem
        {
            Kind = TechnicalProblemType.Payment,
            Description = "Technical Problem o numerze 9",
            Date = new DateTime(2024, 1, 15),
            Solved = true,
            User = student9
        };
        var technicalProblem10 = new TechnicalProblem
        {
            Kind = TechnicalProblemType.ApplicationOperation,
            Description = "Technical Problem o numerze 10",
            Date = new DateTime(2024, 2, 2),
            Solved = false,
            User = student10
        };

        dbContext.TechnicalProblems.AddRange(technicalProblem1, technicalProblem2, technicalProblem3, technicalProblem4, technicalProblem5, technicalProblem6, technicalProblem7, technicalProblem8, technicalProblem9, technicalProblem10);

        #endregion

        #region Rent

        var rent1 = new Rent
        {
            StartDate = new DateTime(2024, 1, 1),
            Duration = 12,
            EndDate = new DateTime(2025, 1, 1),
            Offer = offer1,
            Student = student1,
            OtherStudents = { student2, student3 }
        };
        var rent2 = new Rent
        {
            StartDate = new DateTime(2024, 2, 1),
            Duration = 11,
            EndDate = new DateTime(2025, 1, 1),
            Offer = offer2,
            Student = student4,
            OtherStudents = { student5, student6 }
        };
        var rent3 = new Rent
        {
            StartDate = new DateTime(2024, 3, 1),
            Duration = 10,
            EndDate = new DateTime(2025, 1, 1),
            Offer = offer3,
            Student = student7,
            OtherStudents = { student8, student9 }
        };
        var rent4 = new Rent
        {
            StartDate = new DateTime(2023, 4, 1),
            Duration = 9,
            EndDate = new DateTime(2025, 1, 1),
            Offer = offer4,
            Student = student10,
            OtherStudents = { }
        };
        var rent5 = new Rent // offer26
        {
            StartDate = new DateTime(2023, 9, 1),
            Duration = 8,
            EndDate = new DateTime(2024, 5, 1),
            Offer = offer26,
            Student = student11,
            OtherStudents = { }
        };
        var rent6 = new Rent //offer27
        {
            StartDate = new DateTime(2023, 11, 1),
            Duration = 5,
            EndDate = new DateTime(2024, 4, 1),
            Offer = offer27,
            Student = student12,
            OtherStudents = { }
        };
        var rent7 = new Rent //offer28, 
        {
            StartDate = new DateTime(2024, 1, 1),
            Duration = 5,
            EndDate = new DateTime(2024, 6, 1),
            Offer = offer28,
            Student = student13,
            OtherStudents = { student14, student15 }
        };

        dbContext.Rents.AddRange(rent1, rent2, rent3, rent4, rent5, rent6, rent7);

        #endregion

        #region Notifications

        var alert1 = new Notification
        {
            Title = "Meeting Reminder",
            Body = "You have a meeting tomorrow at 10:00 AM.",
            DateTime = new DateTime(2024, 6, 11),
            Read = false,
            User = student1
        };

        var alert2 = new Notification
        {
            Title = "Payment Due",
            Body = "Your monthly subscription payment is due soon.",
            DateTime = new DateTime(2024, 6, 15),
            Read = false,
            User = student1
        };

        dbContext.Notifications.AddRange(alert1, alert2);

        #endregion

        #region Payments

        #region PaymentsRent1

        var payment1 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Deposit,
            Amount = 1500,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 1, 1),
            PaymentDate = new DateTime(2024, 1, 8),
            PaidAtDate = new DateTime(2024, 1, 4),
            Rent = rent1
        };
        var payment2 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2000,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 1, 1),
            PaymentDate = new DateTime(2024, 1, 8),
            PaidAtDate = new DateTime(2024, 1, 4),
            Rent = rent1
        };
        var payment3 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2000,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 2, 1),
            PaymentDate = new DateTime(2024, 2, 8),
            PaidAtDate = new DateTime(2024, 2, 4),
            Rent = rent1
        };
        var payment4 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2000,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 3, 1),
            PaymentDate = new DateTime(2024, 3, 8),
            PaidAtDate = new DateTime(2024, 3, 4),
            Rent = rent1
        };
        var payment5 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2000,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 4, 1),
            PaymentDate = new DateTime(2024, 4, 8),
            PaidAtDate = new DateTime(2024, 4, 4),
            Rent = rent1
        };
        var payment6 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2000,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 5, 1),
            PaymentDate = new DateTime(2024, 5, 8),
            PaidAtDate = new DateTime(2024, 5, 4),
            Rent = rent1
        };
        var payment7 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2000,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 6, 1),
            PaymentDate = new DateTime(2024, 6, 8),
            PaidAtDate = new DateTime(2024, 6, 4),
            Rent = rent1
        };
        var payment8 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2000,
            IsPaid = false,
            CreatedDate = new DateTime(2024, 7, 1),
            PaymentDate = new DateTime(2024, 7, 8),
            Rent = rent1
        };

        #endregion PaymentsRent1
        #region PaymentsRent2

        var payment9 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Deposit,
            Amount = 1500,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 2, 1),
            PaymentDate = new DateTime(2024, 2, 8),
            PaidAtDate = new DateTime(2024, 2, 4),
            Rent = rent2
        };
        var payment10 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2200,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 2, 1),
            PaymentDate = new DateTime(2024, 2, 8),
            PaidAtDate = new DateTime(2024, 2, 4),
            Rent = rent2
        };
        var payment11 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2200,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 3, 1),
            PaymentDate = new DateTime(2024, 3, 8),
            PaidAtDate = new DateTime(2024, 3, 4),
            Rent = rent2
        };
        var payment12 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2200,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 4, 1),
            PaymentDate = new DateTime(2024, 4, 8),
            PaidAtDate = new DateTime(2024, 4, 4),
            Rent = rent2
        };
        var payment13 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2200,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 5, 1),
            PaymentDate = new DateTime(2024, 5, 8),
            PaidAtDate = new DateTime(2024, 5, 4),
            Rent = rent2
        };
        var payment14 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2200,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 6, 1),
            PaymentDate = new DateTime(2024, 6, 8),
            PaidAtDate = new DateTime(2024, 6, 4),
            Rent = rent2
        };
        var payment15 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2200,
            IsPaid = false,
            CreatedDate = new DateTime(2024, 7, 1),
            PaymentDate = new DateTime(2024, 7, 8),
            Rent = rent2
        };

        #endregion PaymentsRent2
        #region PaymentsRent3

        var payment16 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Deposit,
            Amount = 1500,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 3, 1),
            PaymentDate = new DateTime(2024, 3, 8),
            PaidAtDate = new DateTime(2024, 3, 4),
            Rent = rent3
        };
        var payment17 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 1800,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 3, 1),
            PaymentDate = new DateTime(2024, 3, 8),
            PaidAtDate = new DateTime(2024, 3, 4),
            Rent = rent3
        };
        var payment18 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 1800,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 4, 1),
            PaymentDate = new DateTime(2024, 4, 8),
            PaidAtDate = new DateTime(2024, 4, 4),
            Rent = rent3
        };
        var payment19 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 1800,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 5, 1),
            PaymentDate = new DateTime(2024, 5, 8),
            PaidAtDate = new DateTime(2024, 5, 4),
            Rent = rent3
        };
        var payment20 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 1800,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 6, 1),
            PaymentDate = new DateTime(2024, 6, 8),
            PaidAtDate = new DateTime(2024, 6, 4),
            Rent = rent3
        };
        var payment21 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 1800,
            IsPaid = false,
            CreatedDate = new DateTime(2024, 7, 1),
            PaymentDate = new DateTime(2024, 7, 8),
            Rent = rent3
        };

        #endregion PaymentsRent3
        #region PaymentsRent4

        var payment22 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Deposit,
            Amount = 1500,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 4, 1),
            PaymentDate = new DateTime(2024, 4, 8),
            PaidAtDate = new DateTime(2024, 4, 4),
            Rent = rent4
        };
        var payment23 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 4, 1),
            PaymentDate = new DateTime(2024, 4, 8),
            PaidAtDate = new DateTime(2024, 4, 4),
            Rent = rent4
        };
        var payment24 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 5, 1),
            PaymentDate = new DateTime(2024, 5, 8),
            PaidAtDate = new DateTime(2024, 5, 4),
            Rent = rent4
        };
        var payment25 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 6, 1),
            PaymentDate = new DateTime(2024, 6, 8),
            PaidAtDate = new DateTime(2024, 6, 4),
            Rent = rent4
        };
        var payment26 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = false,
            CreatedDate = new DateTime(2024, 7, 1),
            PaymentDate = new DateTime(2024, 7, 8),
            Rent = rent4
        };

        #endregion PaymentsRent4
        #region PaymentsRent5

        var payment27 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Deposit,
            Amount = 1500,
            IsPaid = true,
            CreatedDate = new DateTime(2023, 9, 1),
            PaymentDate = new DateTime(2023, 9, 1),
            PaidAtDate = new DateTime(2023, 9, 1),
            Rent = rent5
        };
        var payment28 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2023, 9, 1),
            PaymentDate = new DateTime(2023, 9, 1),
            PaidAtDate = new DateTime(2023, 9, 1),
            Rent = rent5
        };
        var payment29 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2023, 10, 1),
            PaymentDate = new DateTime(2023, 10, 1),
            PaidAtDate = new DateTime(2023, 10, 1),
            Rent = rent5
        };
        var payment30 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2023, 11, 1),
            PaymentDate = new DateTime(2023, 11, 1),
            PaidAtDate = new DateTime(2023, 11, 1),
            Rent = rent5
        };
        var payment31 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2023, 12, 1),
            PaymentDate = new DateTime(2023, 12, 1),
            PaidAtDate = new DateTime(2023, 12, 1),
            Rent = rent5
        };
        var payment32 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 1, 1),
            PaymentDate = new DateTime(2024, 1, 1),
            PaidAtDate = new DateTime(2024, 1, 1),
            Rent = rent5
        };
        var payment33 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 2, 1),
            PaymentDate = new DateTime(2024, 2, 1),
            PaidAtDate = new DateTime(2024, 2, 1),
            Rent = rent5
        };
        var payment34 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 3, 1),
            PaymentDate = new DateTime(2024, 3, 1),
            PaidAtDate = new DateTime(2024, 3, 1),
            Rent = rent5
        };
        var payment35 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 4, 1),
            PaymentDate = new DateTime(2024, 4, 1),
            PaidAtDate = new DateTime(2024, 4, 1),
            Rent = rent5
        };

        #endregion PaymentsRent5
        #region PaymentsRent6

        var payment36 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Deposit,
            Amount = 1500,
            IsPaid = true,
            CreatedDate = new DateTime(2023, 11, 1),
            PaymentDate = new DateTime(2023, 11, 1),
            PaidAtDate = new DateTime(2023, 11, 1),
            Rent = rent6
        };
        var payment37 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2023, 11, 1),
            PaymentDate = new DateTime(2023, 11, 1),
            PaidAtDate = new DateTime(2023, 11, 1),
            Rent = rent6
        };
        var payment38 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2023, 12, 1),
            PaymentDate = new DateTime(2023, 12, 1),
            PaidAtDate = new DateTime(2023, 12, 1),
            Rent = rent6
        };
        var payment39 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 1, 1),
            PaymentDate = new DateTime(2024, 1, 1),
            PaidAtDate = new DateTime(2024, 1, 1),
            Rent = rent6
        };
        var payment40 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 2, 1),
            PaymentDate = new DateTime(2024, 2, 1),
            PaidAtDate = new DateTime(2024, 2, 1),
            Rent = rent6
        };
        var payment41 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 3, 1),
            PaymentDate = new DateTime(2024, 3, 1),
            PaidAtDate = new DateTime(2024, 3, 1),
            Rent = rent6
        };

        #endregion PaymentsRent6
        #region PaymentsRent7

        var payment42 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Deposit,
            Amount = 1500,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 1, 1),
            PaymentDate = new DateTime(2024, 1, 1),
            PaidAtDate = new DateTime(2024, 1, 1),
            Rent = rent7
        };
        var payment43 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 1, 1),
            PaymentDate = new DateTime(2024, 1, 1),
            PaidAtDate = new DateTime(2024, 1, 1),
            Rent = rent7
        };
        var payment44 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 2, 1),
            PaymentDate = new DateTime(2024, 2, 1),
            PaidAtDate = new DateTime(2024, 2, 1),
            Rent = rent7
        };
        var payment45 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 3, 1),
            PaymentDate = new DateTime(2024, 3, 1),
            PaidAtDate = new DateTime(2024, 3, 1),
            Rent = rent7
        };
        var payment46 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 4, 1),
            PaymentDate = new DateTime(2024, 4, 1),
            PaidAtDate = new DateTime(2024, 4, 1),
            Rent = rent7
        };
        var payment47 = new Payment
        {
            PaymentPurpose = PaymentPurpose.Rent,
            Amount = 2100,
            IsPaid = true,
            CreatedDate = new DateTime(2024, 5, 1),
            PaymentDate = new DateTime(2024, 5, 1),
            PaidAtDate = new DateTime(2024, 5, 1),
            Rent = rent7
        };

        #endregion PaymentsRent7


        dbContext.Payments.AddRange(payment1, payment2, payment3, payment4, payment5, payment6, payment7, payment8, payment9, payment10, payment11, payment12, payment13, payment14, payment15, payment16, payment17, payment18, payment19, payment20, payment21, payment22, payment23, payment24, payment25, payment26, payment27, payment28, payment29, payment30, payment31, payment32, payment33, payment34, payment35, payment36, payment37, payment38, payment39, payment40, payment41, payment42, payment43, payment44, payment45, payment46, payment47);

        #endregion

        #region RentOpinion

        var rentOpinion1 = new RentOpinion
        {
            Date = DateTime.Now,
            Rating = 1,
            Service = 1,
            Location = 1,
            Equipment = 1,
            QualityForMoney = 1,
            Description = "bla bla bla",
            Student = student1,
            Property = flat1
        };
        var rentOpinion2 = new RentOpinion
        {
            Date = DateTime.Now,
            Rating = 2,
            Service = 2,
            Location = 2,
            Equipment = 2,
            QualityForMoney = 2,
            Description = "Lorem ipsum dolor sit amet",
            Student = student2,
            Property = flat1
        };
        var rentOpinion3 = new RentOpinion
        {
            Date = DateTime.Now,
            Rating = 3,
            Service = 3,
            Location = 3,
            Equipment = 3,
            QualityForMoney = 3,
            Description = "Consectetur adipiscing elit",
            Student = student3,
            Property = flat1
        };
        var rentOpinion4 = new RentOpinion
        {
            Date = DateTime.Now,
            Rating = 4,
            Service = 4,
            Location = 4,
            Equipment = 4,
            QualityForMoney = 4,
            Description = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
            Student = student4,
            Property = house1
        };
        var rentOpinion5 = new RentOpinion
        {
            Date = DateTime.Now,
            Rating = 5,
            Service = 5,
            Location = 5,
            Equipment = 5,
            QualityForMoney = 5,
            Description = "Opinia do rent 5",
            Student = student11,
            Property = flat5
        };
        var rentOpinion6 = new RentOpinion
        {
            Date = DateTime.Now,
            Rating = 6,
            Service = 6,
            Location = 6,
            Equipment = 6,
            QualityForMoney = 6,
            Description = "Opinia do rent 6",
            Student = student12,
            Property = room4
        };
        var rentOpinion7 = new RentOpinion
        {
            Date = DateTime.Now,
            Rating = 7,
            Service = 7,
            Location = 7,
            Equipment = 7,
            QualityForMoney = 7,
            Description = "Opinia do rent 7",
            Student = student13,
            Property = house4
        };

        dbContext.RentOpinions.AddRange(rentOpinion1, rentOpinion2, rentOpinion3, rentOpinion4, rentOpinion5, rentOpinion6, rentOpinion7);

        #endregion

        #region GroupChat

        var groupChat1 = new GroupChat
        {
            Name = "groupChat do argument 1"
        };

        var groupChat2 = new GroupChat
        {
            Name = "groupChat do argument 2"
        };

        var groupChat3 = new GroupChat
        {
            Name = "groupChat do argument 3"
        };

        var groupChat4 = new GroupChat
        {
            Name = "groupChat do argument 4"
        };

        var groupChat5 = new GroupChat
        {
            Name = "groupChat do argument 5"
        };

        var groupChat6 = new GroupChat
        {
            Name = "groupChat do argument 6"
        };

        dbContext.GroupChats.AddRange(groupChat1, groupChat2, groupChat3, groupChat4, groupChat5, groupChat6);

        #endregion

        #region UserGroupChat

        var userGroupChat1 = new UserGroupChat
        {
            User = student1,
            GroupChat = groupChat1
        };

        var userGroupChat2 = new UserGroupChat
        {
            User = owner1,
            GroupChat = groupChat1
        };

        var userGroupChat3 = new UserGroupChat
        {
            User = student4,
            GroupChat = groupChat2
        };

        var userGroupChat4 = new UserGroupChat
        {
            User = rent2.Offer.Property.Owner,
            GroupChat = groupChat2
        };

        var userGroupChat5 = new UserGroupChat
        {
            User = student7,
            GroupChat = groupChat3
        };

        var userGroupChat6 = new UserGroupChat
        {
            User = rent3.Offer.Property.Owner,
            GroupChat = groupChat3
        };

        var userGroupChat7 = new UserGroupChat
        {
            User = student10,
            GroupChat = groupChat4
        };

        var userGroupChat8 = new UserGroupChat
        {
            User = rent4.Offer.Property.Owner,
            GroupChat = groupChat4
        };

        var userGroupChat9 = new UserGroupChat
        {
            User = student8,
            GroupChat = groupChat5
        };

        var userGroupChat10 = new UserGroupChat
        {
            User = rent3.Offer.Property.Owner,
            GroupChat = groupChat5
        };

        var userGroupChat11 = new UserGroupChat
        {
            User = student3,
            GroupChat = groupChat6
        };

        var userGroupChat12 = new UserGroupChat
        {
            User = rent1.Offer.Property.Owner,
            GroupChat = groupChat6
        };

        dbContext.UserGroupChats.AddRange(userGroupChat1, userGroupChat2, userGroupChat3, userGroupChat4, userGroupChat5, userGroupChat6, userGroupChat7, userGroupChat8, userGroupChat9, userGroupChat10, userGroupChat11, userGroupChat12);

        #endregion

        #region Argument

        var argument1 = new Argument
        {
            Title = "Tytuł do pierwszego sporu",
            Description = "sprzeczka pierwsza",
            StartDate = new DateTime(2023, 1, 12),
            OwnerAcceptanceDate = new DateTime(2023, 2, 13),
            StudentAccceptanceDate = new DateTime(2023, 2, 13),
            ArgumentStatus = ArgumentStatus.Resolved,
            InterventionNeed = false,
            InterventionNeedDate = null,
            ArgumentCreatedByUserId = owner1.UserId,
            Rent = rent1,
            Student = student1,
            GroupChat = groupChat1
        };

        var argument2 = new Argument
        {
            Title = "Tytuł do drugeigo sporu",
            Description = "a to jest druga sprzeczka",
            StartDate = new DateTime(2023, 1, 12),
            OwnerAcceptanceDate = new DateTime(2023, 1, 13),
            StudentAccceptanceDate = null,
            ArgumentStatus = ArgumentStatus.Ongoing,
            InterventionNeed = true,
            InterventionNeedDate = new DateTime(2023, 1 , 14),
            ArgumentCreatedByUserId = student4.UserId,
            Rent = rent2,
            Student = student4,
            GroupChat = groupChat2
        };

        var argument3 = new Argument
        {
            Title = "Tytuł do trzeciego sporu",
            Description = "to jest sprzeczka trzecia",
            StartDate = new DateTime(2024, 1, 12),
            OwnerAcceptanceDate = null,
            StudentAccceptanceDate = null,
            ArgumentStatus = ArgumentStatus.Ongoing,
            InterventionNeed = true,
            InterventionNeedDate = new DateTime(2024, 2, 13),
            ArgumentCreatedByUserId = owner2.UserId,
            Rent = rent3,
            Student = student7,
            GroupChat = groupChat3
        };

        var argument4 = new Argument
        {
            Title = "Tytuł do czwartego sporu",
            Description = "to jest sprzeczka czwarta",
            StartDate = new DateTime(2023, 6, 24),
            OwnerAcceptanceDate = null,
            StudentAccceptanceDate = null,
            ArgumentStatus = ArgumentStatus.Ongoing,
            InterventionNeed = true,
            InterventionNeedDate = new DateTime(2023, 6, 25),
            ArgumentCreatedByUserId = student10.UserId,
            Rent = rent4,
            Student = student10,
            GroupChat = groupChat4
        };

        var argument5 = new Argument
        {
            Title = "Tytuł do piątego sporu",
            Description = "to jest sprzeczka piąta",
            StartDate = new DateTime(2024, 4, 12),
            OwnerAcceptanceDate = new DateTime(2024, 4, 15),
            StudentAccceptanceDate = new DateTime(2024, 4, 15),
            ArgumentStatus = ArgumentStatus.Resolved,
            InterventionNeed = false,
            InterventionNeedDate = null,
            ArgumentCreatedByUserId = student8.UserId,
            Rent = rent3,
            Student = student8,
            GroupChat = groupChat5
        };

        var argument6 = new Argument
        {
            Title = "Tytuł do szóstego sporu",
            Description = "sprzeczka szósta",
            StartDate = new DateTime(2024, 2, 22),
            OwnerAcceptanceDate = null,
            StudentAccceptanceDate = null,
            ArgumentStatus = ArgumentStatus.Ongoing,
            InterventionNeed = true,
            InterventionNeedDate = new DateTime(2024, 2, 23),
            ArgumentCreatedByUserId = owner1.UserId,
            Rent = rent1,
            Student = student3,
            GroupChat = groupChat6
        };

        dbContext.Arguments.AddRange(argument1, argument2, argument3, argument4, argument5, argument6);

        #endregion

        #region ArgumentIntervention

        var argumentIntervention1 = new ArgumentIntervention
        {

            Date = new DateTime(2023, 7, 9),
            Justification = "interwencja do argumentu 4tego",
            Argument = argument4,
            Moderator = moderator1
        };

        var argumentIntervention2 = new ArgumentIntervention
        {

            Date = new DateTime(2024, 4, 13),
            Justification = "interwencja do argumentu 5tego",
            Argument = argument5,
            Moderator = moderator1
        };

        var argumentIntervention3 = new ArgumentIntervention
        {

            Date = new DateTime(2024, 1, 14),
            Justification = "interwencja do argumentu 3tego",
            Argument = argument3,
            Moderator = moderator1
        };

        var argumentIntervention4 = new ArgumentIntervention
        {

            Date = new DateTime(2024, 1, 25),
            Justification = "interwencja do argumentu 3tego druga",
            Argument = argument3,
            Moderator = moderator1
        };

        dbContext.ArgumentInterventions.AddRange(argumentIntervention1, argumentIntervention2, argumentIntervention3, argumentIntervention4);

        #endregion

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedPropertyFiles(Property property)
    {
        if (property.VerificationStatus == VerificationStatus.NotVerified)
        {
            var titleDeedPath = Path.Combine("FileSeed", "PropertiesSeed", "TitleDeed", "TitleDeed.pdf");

            if (!File.Exists(titleDeedPath))
            {
                throw new FileNotFoundException($"The file {titleDeedPath} does not exist.");
            }

            property.TitleDeed = await _fileUploadService.CreateFileFromSourceFilePathAsync(titleDeedPath);
        }

        var imagesPath = Path.Combine("FileSeed", "PropertiesSeed", "Images");

        var random = new Random();
        var files = Directory.GetFiles(imagesPath);
        var selectedFiles = files.OrderBy(f => random.Next()).Take(4);

        foreach (var file in selectedFiles)
        {
            property.Images.Add(await _fileUploadService.CreateFileFromSourceFilePathAsync(file));
        }
    }

    private static async Task SeedUserFiles(OwnerStudent user, Gender gender)
    {
        if (user.VerificationStatus == VerificationStatus.NotVerified)
        {
            var documentPath = string.Empty;

            switch (user.DocumentType)
            {
                case DocumentType.StudentCard:
                    documentPath = Path.Combine("FileSeed", "UsersSeed", "StudentCard", "StudentCard.jpg");
                    break;
                case DocumentType.ID:
                    documentPath = Path.Combine("FileSeed", "UsersSeed", "ID", "ID.jpg");
                    break;
                case DocumentType.Passport:
                    documentPath = Path.Combine("FileSeed", "UsersSeed", "Passport", "Passport.jpg");
                    break;
            }

            if (!File.Exists(documentPath))
            {
                throw new FileNotFoundException($"The file {documentPath} does not exist.");
            }

            user.Document = await _fileUploadService.CreateFileFromSourceFilePathAsync(documentPath);
        }

        try
        {
            var profilePicturePath = await ProfilePictureSeeder.GetRandomProfilePicturePath(gender);

            user.ProfilePicture = await _fileUploadService.CreateFileFromSourceFilePathAsync(profilePicturePath);

            if (File.Exists(profilePicturePath))
            {
                File.Delete(profilePicturePath);
            }
        }
        catch (Exception)
        {
            var backupImagePath = Path.Combine("FileSeed", "UsersSeed", "ProfilePicture", "ProfilePicture.jpg");

            if (File.Exists(backupImagePath))
            {
                user.ProfilePicture = await _fileUploadService.CreateFileFromSourceFilePathAsync(backupImagePath);
            }
        }

        await Task.Delay(1000);
    }
}