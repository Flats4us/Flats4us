﻿using Azure.Core;
using Flats4us.Entities;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using System;
using System.Linq;

public static class DataSeeder
{
    public static void SeedData(Flats4usContext dbContext)
    {
        #region Equipment

        var equipment1 = new Equipment { 
            Name = "Dishwasher"
        };
        var equipment2 = new Equipment
        {
            Name = "Washing Machine"
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
            Name = "Coffe Machine"
        };
        var equipment6 = new Equipment
        {
            Name = "Air Conditioning"
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

        dbContext.Equipment.AddRange(equipment1, equipment2, equipment3, equipment4, equipment5, equipment6, equipment7, equipment8, equipment9, equipment10, equipment11);

        #endregion

        #region Owner

        var owner1 = new Owner
        {
            Name = "Maciej",
            Surname = "Kowalski",
            Street = "Kaukaska",
            Number = "9",
            Flat = 2,
            City = "Warszawa",
            PostalCode = "02-760",
            Email = "mkowalski@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 1, 12),
            LastLoginDate = new DateTime(2023, 10, 12),
            Username = "mkowalski",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("mkowalski123"),
            ActivityStatus = false,
            ImagesPath = Guid.NewGuid().ToString(),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 12, 8),
            BankAccount = "12341234123412341234123412"
        };
        var owner2 = new Owner
        {
            Name = "Barbara",
            Surname = "Nowak",
            Street = "Tuchlińska",
            Number = "2",
            Flat = 2,
            City = "Warszawa",
            PostalCode = "02-695",
            Email = "bnowak@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 3, 23),
            LastLoginDate = new DateTime(2023, 10, 10),
            Username = "bnowak",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("bnowak123"),
            ActivityStatus = false,
            ImagesPath = Guid.NewGuid().ToString(),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 9, 8),
            BankAccount = "12341234123412341234123412"
        };
        var owner3 = new Owner
        {
            Name = "Robert",
            Surname = "Pawlak",
            Street = "Kormoranów",
            Number = "9",
            Flat = 5,
            City = "Warszawa",
            PostalCode = "02-836",
            Email = "bnowak@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 7, 13),
            LastLoginDate = new DateTime(2023, 10, 20),
            Username = "nowakowski",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("bnowak123"),
            ActivityStatus = false,
            ImagesPath = Guid.NewGuid().ToString(),
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2026, 4, 8),
            BankAccount = "12341234123412341234123412"
        };
        var owner4 = new Owner
        {
            Name = "Katarzyna",
            Surname = "Klik",
            Street = "Sanocka",
            Number = "11B",
            Flat = 1,
            City = "Warszawa",
            PostalCode = "02-110",
            Email = "bnowak@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 2, 8),
            LastLoginDate = new DateTime(2023, 9, 30),
            Username = "nowak3",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("bnowak123"),
            ImagesPath = Guid.NewGuid().ToString(),
            ActivityStatus = false,
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2029, 5, 14),
            BankAccount = "12341234123412341234123412"
        };
        ImageUtility.DeleteDirectory("Images/Users").Wait();

        ImageUtility.SeedUserImage(owner1.ImagesPath);
        ImageUtility.SeedUserImage(owner2.ImagesPath);
        ImageUtility.SeedUserImage(owner3.ImagesPath);
        ImageUtility.SeedUserImage(owner4.ImagesPath);
        dbContext.Owners.AddRange(owner1, owner2);
        dbContext.SaveChanges();

        #endregion

        ImageUtility.DeleteDirectory("Images/Properties").Wait();

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
            VerificationStatus = VerificationStatus.Verified,
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
            VerificationStatus = VerificationStatus.Verified,
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
            Owner = owner4,
            NumberOfRooms = 4,
            Floor = 2,
            Elevator = true,
            Equipment = { equipment1, equipment2, equipment3, equipment6, equipment8 }
        };
        
        dbContext.Flats.AddRange(flat1, flat2, flat3, flat4, flat5, flat6, flat7, flat8, flat9, flat10);

        ImageUtility.SeedPropertyImage(flat1.ImagesPath);  
        ImageUtility.SeedPropertyImage(flat2.ImagesPath);
        ImageUtility.SeedPropertyImage(flat3.ImagesPath);
        ImageUtility.SeedPropertyImage(flat4.ImagesPath);
        ImageUtility.SeedPropertyImage(flat5.ImagesPath);
        ImageUtility.SeedPropertyImage(flat6.ImagesPath);
        ImageUtility.SeedPropertyImage(flat7.ImagesPath);
        ImageUtility.SeedPropertyImage(flat8.ImagesPath);
        ImageUtility.SeedPropertyImage(flat9.ImagesPath);
        ImageUtility.SeedPropertyImage(flat10.ImagesPath);

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
            VerificationStatus = VerificationStatus.Verified,
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
            VerificationStatus = VerificationStatus.Verified,
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
            Owner = owner4,
            Floor = 3,
            Elevator = false,
            Equipment = { equipment3, equipment7, equipment9, equipment10 }
        };

        dbContext.Rooms.AddRange(room1, room2, room3, room4, room5);

        ImageUtility.SeedPropertyImage(room1.ImagesPath);
        ImageUtility.SeedPropertyImage(room2.ImagesPath);
        ImageUtility.SeedPropertyImage(room3.ImagesPath);
        ImageUtility.SeedPropertyImage(room4.ImagesPath);
        ImageUtility.SeedPropertyImage(room5.ImagesPath);

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
            VerificationStatus = VerificationStatus.Verified,
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
            Owner = owner4,
            NumberOfRooms = 3,
            NumberOfFloors = 1,
            PlotArea = 140,
            Equipment = { equipment2, equipment6, equipment10, equipment11 }
        };
        

        dbContext.Houses.AddRange(house1, house2, house3, house4, house5);

        ImageUtility.SeedPropertyImage(house1.ImagesPath);
        ImageUtility.SeedPropertyImage(house2.ImagesPath);
        ImageUtility.SeedPropertyImage(house3.ImagesPath);
        ImageUtility.SeedPropertyImage(house4.ImagesPath);
        ImageUtility.SeedPropertyImage(house5.ImagesPath);

        #endregion

        #region Offers

        var offer1 = new Offer
        {
            Date = new DateTime(2023, 10, 10),
            OfferStatus = OfferStatus.Current,
            Price = 2000,
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
            OfferStatus = OfferStatus.Current,
            Price = 2200,
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
            OfferStatus = OfferStatus.Current,
            Price = 1800,
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
            OfferStatus = OfferStatus.Current,
            Price = 2100,
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
            OfferStatus = OfferStatus.Current,
            Price = 1900,
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
            Description = "placeholder",
            StartDate = new DateTime(2024, 2, 1),
            EndDate = new DateTime(2024, 8, 1),
            NumberOfInterested = 10,
            Regulations = "placeholder",
            Property = room5
        };

        dbContext.Offers.AddRange(offer1, offer2, offer3, offer4, offer5, offer6, offer7, offer8, offer9, offer10, offer11, offer12, offer13, offer14, offer15);

        #endregion

        #region OwnerOfferSurveys

        var surveyOwnerOffer1 = new SurveyOwnerOffer
        {
            Smoking = false,
            Parties = true,
            Animals = true,
            Gender = Gender.Both,
            Offer = offer1
        };
        var surveyOwnerOffer2 = new SurveyOwnerOffer
        {
            Smoking = true,
            Parties = false,
            Animals = false,
            Gender = Gender.Male,
            Offer = offer2
        };
        var surveyOwnerOffer3 = new SurveyOwnerOffer
        {
            Smoking = true,
            Parties = true,
            Animals = false,
            Gender = Gender.Female,
            Offer = offer3
        };
        var surveyOwnerOffer4 = new SurveyOwnerOffer
        {
            Smoking = false,
            Parties = true,
            Animals = true,
            Gender = Gender.Both,
            Offer = offer4
        };
        var surveyOwnerOffer5 = new SurveyOwnerOffer
        {
            Smoking = true,
            Parties = true,
            Animals = false,
            Gender = Gender.Male,
            Offer = offer5
        };
        var surveyOwnerOffer6 = new SurveyOwnerOffer
        {
            Smoking = false,
            Parties = false,
            Animals = true,
            Gender = Gender.Both,
            Offer = offer6
        };
        var surveyOwnerOffer7 = new SurveyOwnerOffer
        {
            Smoking = true,
            Parties = false,
            Animals = false,
            Gender = Gender.Both,
            Offer = offer7
        };
        var surveyOwnerOffer8 = new SurveyOwnerOffer
        {
            Smoking = false,
            Parties = true,
            Animals = true,
            Gender = Gender.Both,
            Offer = offer8
        };
        var surveyOwnerOffer9 = new SurveyOwnerOffer
        {
            Smoking = true,
            Parties = true,
            Animals = false,
            Gender = Gender.Female,
            Offer = offer9
        };
        var surveyOwnerOffer10 = new SurveyOwnerOffer
        {
            Smoking = false,
            Parties = false,
            Animals = false,
            Gender = Gender.Both,
            Offer = offer10
        };
        var surveyOwnerOffer11 = new SurveyOwnerOffer
        {
            Smoking = true,
            Parties = true,
            Animals = true,
            Gender = Gender.Male,
            Offer = offer11
        };
        var surveyOwnerOffer12 = new SurveyOwnerOffer
        {
            Smoking = false,
            Parties = true,
            Animals = false,
            Gender = Gender.Female,
            Offer = offer12
        };
        var surveyOwnerOffer13 = new SurveyOwnerOffer
        {
            Smoking = true,
            Parties = false,
            Animals = true,
            Gender = Gender.Both,
            Offer = offer13
        };
        var surveyOwnerOffer14 = new SurveyOwnerOffer
        {
            Smoking = false,
            Parties = false,
            Animals = false,
            Gender = Gender.Both,
            Offer = offer14
        };
        var surveyOwnerOffer15 = new SurveyOwnerOffer
        {
            Smoking = true,
            Parties = true,
            Animals = true,
            Gender = Gender.Both,
            Offer = offer15
        };

        dbContext.OwnerOfferSurveys.AddRange(surveyOwnerOffer1, surveyOwnerOffer2, surveyOwnerOffer3, surveyOwnerOffer4, surveyOwnerOffer5, surveyOwnerOffer6, surveyOwnerOffer7, surveyOwnerOffer8, surveyOwnerOffer9, surveyOwnerOffer10, surveyOwnerOffer11, surveyOwnerOffer12, surveyOwnerOffer13, surveyOwnerOffer14, surveyOwnerOffer15);

        #endregion

        dbContext.SaveChanges();
    }
}