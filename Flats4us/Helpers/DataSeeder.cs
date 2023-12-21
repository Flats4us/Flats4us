using Azure.Core;
using Flats4us.Entities;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using System;
using System.Linq;

public static class DataSeeder
{
    public static void SeedData(Flats4usContext dbContext)
    {
        ImageUtility.DeleteDirectory("Images/Properties").Wait();
        ImageUtility.DeleteDirectory("Images/Users").Wait();

        #region Equipment

        var equipment1 = new Equipment { 
            Name = "Zmywarka"
        };
        var equipment2 = new Equipment
        {
            Name = "Pralka"
        };
        var equipment3 = new Equipment
        {
            Name = "Żelazko"
        };
        var equipment4 = new Equipment
        {
            Name = "Czajnik"
        };
        var equipment5 = new Equipment
        {
            Name = "Ekspres do kawy"
        };
        var equipment6 = new Equipment
        {
            Name = "Klimatyzacja"
        };
        var equipment7 = new Equipment
        {
            Name = "Balkon"
        };
        var equipment8 = new Equipment
        {
            Name = "Telewizor"
        };
        var equipment9 = new Equipment
        {
            Name = "Wanna"
        };
        var equipment10 = new Equipment
        {
            Name = "Piekarnik"
        };
        var equipment11 = new Equipment
        {
            Name = "Mikrofalówka"
        };

        dbContext.Equipment.AddRange(equipment1, equipment2, equipment3, equipment4, equipment5, equipment6, equipment7, equipment8, equipment9, equipment10, equipment11);

        #endregion

        #region Interest

        var interest1 = new Interest
        {
            Name = "Fotografia"
        };
        var interest2 = new Interest
        {
            Name = "Turystyka piesza"
        };
        var interest3 = new Interest
        {
            Name = "Gotowanie"
        };
        var interest4 = new Interest
        {
            Name = "Podróże"
        };
        var interest5 = new Interest
        {
            Name = "Czytanie"
        };
        var interest6 = new Interest
        {
            Name = "Ogrodnictwo"
        };
        var interest7 = new Interest
        {
            Name = "Muzyka"
        };
        var interest8 = new Interest
        {
            Name = "Wolontariat"
        };
        var interest9 = new Interest
        {
            Name = "Sport"
        };
        var interest10 = new Interest
        {
            Name = "Języki"
        };
        var interest11 = new Interest
        {
            Name = "Malarstwo"
        };
        var interest12 = new Interest
        {
            Name = "Rower"
        };
        var interest13 = new Interest
        {
            Name = "Yoga"
        };
        var interest14 = new Interest
        {
            Name = "Gry komputerowe"
        };
        var interest15 = new Interest
        {
            Name = "Pisanie"
        };
        var interest16 = new Interest
        {
            Name = "Film"
        };
        var interest17 = new Interest
        {
            Name = "Technologia"
        };
        var interest18 = new Interest
        {
            Name = "Astronomia"
        };
        var interest19 = new Interest
        {
            Name = "DIY"
        };
        var interest20 = new Interest
        {
            Name = "Modelarstwo"
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
            DateForVerificationSorting = new DateTime(2023, 1, 12),
            LastLoginDate = new DateTime(2023, 10, 12),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Mkowalski123"),
            ActivityStatus = false,
            ImagesPath = Guid.NewGuid().ToString(),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.NotVerified,
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
            ActivityStatus = false,
            ImagesPath = Guid.NewGuid().ToString(),
            DocumentType = DocumentType.Passport,
            VerificationStatus = VerificationStatus.NotVerified,
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
            DateForVerificationSorting = new DateTime(2023, 7, 13),
            LastLoginDate = new DateTime(2023, 10, 20),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Rpawlak123"),
            ActivityStatus = false,
            ImagesPath = Guid.NewGuid().ToString(),
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
            DateForVerificationSorting = new DateTime(2023, 2, 8),
            LastLoginDate = new DateTime(2023, 9, 30),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Kklik123"),
            ImagesPath = Guid.NewGuid().ToString(),
            ActivityStatus = false,
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2029, 5, 14),
            BankAccount = "12341234123412341234123412",
            DocumentNumber = "XXX 000000"
        };

        dbContext.Owners.AddRange(owner1, owner2, owner3, owner4);

        ImageUtility.SeedUserImage(owner1.ImagesPath, owner1.VerificationStatus, owner1.DocumentType).Wait();
        ImageUtility.SeedUserImage(owner2.ImagesPath, owner2.VerificationStatus, owner2.DocumentType).Wait();
        ImageUtility.SeedUserImage(owner3.ImagesPath, owner3.VerificationStatus, owner3.DocumentType).Wait();
        ImageUtility.SeedUserImage(owner4.ImagesPath, owner4.VerificationStatus, owner4.DocumentType).Wait();

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
            DateForVerificationSorting = new DateTime(2023, 1, 12),
            LastLoginDate = new DateTime(2023, 10, 12),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Kkajetanski123"),
            ActivityStatus = false,
            ImagesPath = Guid.NewGuid().ToString(),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 12, 8),
            BirthDate = new DateTime(2002, 12, 1),
            StudentNumber = "s27235",
            University = "PJATK",
            Links = "https://www.facebook.com/profile.php?id=XXXXXXXXX|https://twitter.com/kkajetanski|https://www.instagram.com/kkajetanski/",
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
            DateForVerificationSorting = new DateTime(2023, 1, 12),
            LastLoginDate = new DateTime(2023, 10, 12),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Aklocek123"),
            ActivityStatus = false,
            ImagesPath = Guid.NewGuid().ToString(),
            DocumentType = DocumentType.StudentCard,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 12, 8),
            BirthDate = new DateTime(2002, 12, 1),
            StudentNumber = "s2137",
            University = "PJATK",
            Links = "https://www.facebook.com/profile.php?id=XXXXXXXXX|https://twitter.com/aklocek|https://www.instagram.com/aklocek/",
            IsTenant = false,
            Interests = { interest8, interest7, interest2 }
        };

        dbContext.Students.AddRange(student1, student2);

        ImageUtility.SeedUserImage(student1.ImagesPath, student1.VerificationStatus, student1.DocumentType).Wait();
        ImageUtility.SeedUserImage(student2.ImagesPath, student2.VerificationStatus, student2.DocumentType).Wait();

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
            ActivityStatus = false,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 10, 1),
            DateForVerificationSorting = new DateTime(2023, 10, 1),
            Owner = owner1,
            NumberOfRooms = 2,
            Floor = 2,
            Elevator = false,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 10, 9),
            DateForVerificationSorting = new DateTime(2023, 10, 9),
            Owner = owner1,
            NumberOfRooms = 3,
            Floor = 2,
            Elevator = false,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.NotVerified,
            CreationDate = new DateTime(2023, 11, 1),
            DateForVerificationSorting = new DateTime(2023, 11, 1),
            Owner = owner2,
            NumberOfRooms = 2,
            Floor = 7,
            Elevator = true,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 7, 1),
            DateForVerificationSorting = new DateTime(2023, 7, 1),
            Owner = owner2,
            NumberOfRooms = 3,
            Floor = 2,
            Elevator = false,
            Equipment = { equipment2, equipment4, equipment7, equipment9, equipment10 }
        };
        var flat5 = new Flat
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.NotVerified,
            CreationDate = new DateTime(2023, 5, 6),
            DateForVerificationSorting = new DateTime(2023, 5, 6),
            Owner = owner3,
            NumberOfRooms = 2,
            Floor = 3,
            Elevator = false,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 7, 3),
            DateForVerificationSorting = new DateTime(2023, 7, 3),
            Owner = owner3,
            NumberOfRooms = 3,
            Floor = 3,
            Elevator = true,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 11, 14),
            DateForVerificationSorting = new DateTime(2023, 11, 14),
            Owner = owner3,
            NumberOfRooms = 3,
            Floor = 9,
            Elevator = true,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 9, 4),
            DateForVerificationSorting = new DateTime(2023, 9, 4),
            Owner = owner4,
            NumberOfRooms = 2,
            Floor = 10,
            Elevator = true,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 8, 18),
            DateForVerificationSorting = new DateTime(2023, 8, 18),
            Owner = owner4,
            NumberOfRooms = 2,
            Floor = 4,
            Elevator = true,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 4, 6),
            DateForVerificationSorting = new DateTime(2023, 4, 6),
            Owner = owner4,
            NumberOfRooms = 4,
            Floor = 2,
            Elevator = true,
            Equipment = { equipment1, equipment2, equipment3, equipment6, equipment8 }
        };
        
        dbContext.Flats.AddRange(flat1, flat2, flat3, flat4, flat5, flat6, flat7, flat8, flat9, flat10);

        ImageUtility.SeedPropertyImage(flat1.ImagesPath, flat1.VerificationStatus);  
        ImageUtility.SeedPropertyImage(flat2.ImagesPath, flat2.VerificationStatus);
        ImageUtility.SeedPropertyImage(flat3.ImagesPath, flat3.VerificationStatus);
        ImageUtility.SeedPropertyImage(flat4.ImagesPath, flat4.VerificationStatus);
        ImageUtility.SeedPropertyImage(flat5.ImagesPath, flat5.VerificationStatus);
        ImageUtility.SeedPropertyImage(flat6.ImagesPath, flat6.VerificationStatus);
        ImageUtility.SeedPropertyImage(flat7.ImagesPath, flat7.VerificationStatus);
        ImageUtility.SeedPropertyImage(flat8.ImagesPath, flat8.VerificationStatus);
        ImageUtility.SeedPropertyImage(flat9.ImagesPath, flat9.VerificationStatus);
        ImageUtility.SeedPropertyImage(flat10.ImagesPath, flat10.VerificationStatus);

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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 6, 23),
            DateForVerificationSorting = new DateTime(2023, 6, 23),
            Owner = owner1,
            Floor = 4,
            Elevator = false,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 7, 22),
            DateForVerificationSorting = new DateTime(2023, 7, 22),
            Owner = owner1,
            Floor = 3,
            Elevator = false,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.NotVerified,
            CreationDate = new DateTime(2023, 10, 3),
            DateForVerificationSorting = new DateTime(2023, 10, 3),
            Owner = owner2,
            Floor = 1,
            Elevator = false,
            Equipment = { equipment3, equipment6, equipment8, equipment11 }
        };
        var room4 = new Room
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.NotVerified,
            CreationDate = new DateTime(2023, 8, 30),
            DateForVerificationSorting = new DateTime(2023, 8, 30),
            Owner = owner3,
            Floor = 4,
            Elevator = false,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 3, 17),
            DateForVerificationSorting = new DateTime(2023, 3, 17),
            Owner = owner4,
            Floor = 3,
            Elevator = false,
            Equipment = { equipment3, equipment7, equipment9, equipment10 }
        };

        dbContext.Rooms.AddRange(room1, room2, room3, room4, room5);

        ImageUtility.SeedPropertyImage(room1.ImagesPath, room1.VerificationStatus);
        ImageUtility.SeedPropertyImage(room2.ImagesPath, room2.VerificationStatus);
        ImageUtility.SeedPropertyImage(room3.ImagesPath, room3.VerificationStatus);
        ImageUtility.SeedPropertyImage(room4.ImagesPath, room4.VerificationStatus);
        ImageUtility.SeedPropertyImage(room5.ImagesPath, room5.VerificationStatus);

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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 7, 14),
            DateForVerificationSorting = new DateTime(2023, 7, 14),
            Owner = owner1,
            NumberOfRooms = 5,
            NumberOfFloors = 3,
            PlotArea= 200,
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 8, 9),
            DateForVerificationSorting = new DateTime(2023, 8, 9),
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 8, 14),
            DateForVerificationSorting = new DateTime(2023, 8, 14),
            Owner = owner2,
            NumberOfRooms = 7,
            NumberOfFloors = 4,
            PlotArea = 210,
            Equipment = { equipment2, equipment4, equipment6, equipment9 }
        };        
        var house4 = new House
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.NotVerified,
            CreationDate = new DateTime(2023, 4, 5),
            DateForVerificationSorting = new DateTime(2023, 4, 5),
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
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            CreationDate = new DateTime(2023, 7, 12),
            DateForVerificationSorting = new DateTime(2023, 7, 12),
            Owner = owner4,
            NumberOfRooms = 3,
            NumberOfFloors = 1,
            PlotArea = 140,
            Equipment = { equipment2, equipment6, equipment10, equipment11 }
        };
        

        dbContext.Houses.AddRange(house1, house2, house3, house4, house5);

        ImageUtility.SeedPropertyImage(house1.ImagesPath, house1.VerificationStatus);
        ImageUtility.SeedPropertyImage(house2.ImagesPath, house2.VerificationStatus);
        ImageUtility.SeedPropertyImage(house3.ImagesPath, house3.VerificationStatus);
        ImageUtility.SeedPropertyImage(house4.ImagesPath, house4.VerificationStatus);
        ImageUtility.SeedPropertyImage(house5.ImagesPath, house5.VerificationStatus);

        #endregion

        #region Offers

        var offer1 = new Offer
        {
            Date = new DateTime(2023, 10, 10),
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
            Price = 1800,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 12, 10),
            EndDate = new DateTime(2024, 6, 10),
            NumberOfInterested = 8,
            Regulations = "placeholder",
            Property = flat4
        };
        var offer4 = new Offer
        {
            Date = new DateTime(2023, 9, 20),
            Status = OfferStatus.Current,
            Price = 2100,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2023, 12, 1),
            EndDate = new DateTime(2024, 4, 1),
            NumberOfInterested = 9,
            Regulations = "placeholder",
            Property = flat6
        };
        var offer5 = new Offer
        {
            Date = new DateTime(2023, 9, 15),
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
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
            Status = OfferStatus.Current,
            Price = 2000,
            Deposit = 1500,
            Description = "placeholder",
            StartDate = new DateTime(2024, 2, 1),
            EndDate = new DateTime(2024, 8, 1),
            NumberOfInterested = 10,
            Regulations = "placeholder",
            Property = room5
        };

        dbContext.Offers.AddRange(offer1, offer2, offer3, offer4, offer5, offer6, offer7, offer8, offer9, offer10, offer11, offer12, offer13, offer14, offer15);

        #endregion
          
        #region OfferPromotions

        var offerPromotion1 = new OfferPromotion
        {
            StartDate = new DateTime(2023, 11, 26),
            EndDate = new DateTime(2024, 11, 30),
            Price = 50,
            Offer = offer1
        };
        var offerPromotion2 = new OfferPromotion
        {
            StartDate = new DateTime(2023, 11, 26),
            EndDate = new DateTime(2024, 11, 30),
            Price = 50,
            Offer = offer2
        };
        var offerPromotion3 = new OfferPromotion
        {
            StartDate = new DateTime(2022, 11, 26),
            EndDate = new DateTime(2023, 11, 30),
            Price = 50,
            Offer = offer3
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

        dbContext.OwnerOfferSurveys.AddRange(surveyOwnerOffer1, surveyOwnerOffer2, surveyOwnerOffer3, surveyOwnerOffer4, surveyOwnerOffer5, surveyOwnerOffer6, surveyOwnerOffer7, surveyOwnerOffer8, surveyOwnerOffer9, surveyOwnerOffer10, surveyOwnerOffer11, surveyOwnerOffer12, surveyOwnerOffer13, surveyOwnerOffer14, surveyOwnerOffer15);

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

        dbContext.SaveChanges();
    }
}