using Flats4us.Entities;
using Flats4us.Helpers.Enums;
using System.Linq;

public static class DataSeeder
{
    public static void SeedData(Flats4usContext dbContext)
    {
        var owner1 = new Owner
        {
            Name = "Maciej",
            Surname = "Kowalski",
            Street = "Marszałkowska",
            Number = 54,
            Flat = 2,
            City = "Warszawa",
            PostalCode = "00-000",
            Email = "mkowalski@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 1, 12),
            LastLoginDate = new DateTime(2023, 10, 12),
            Password = "mkowalski123",
            PhotoPath = "placeholder",
            ActivityStatus = false,
            DocumentPath = "placeholder",
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 12, 8),
            BankAccount = "12341234123412341234123412"
        };
        var owner2 = new Owner
        {
            Name = "Barbara",
            Surname = "Nowak",
            Street = "Długa",
            Number = 12,
            City = "Kraków",
            PostalCode = "00-000",
            Email = "bnowak@gmail.com",
            PhoneNumber = "123456789",
            AccountCreationDate = new DateTime(2023, 3, 23),
            LastLoginDate = new DateTime(2023, 10, 10),
            Password = "bnowak123",
            PhotoPath = "placeholder",
            ActivityStatus = false,
            DocumentPath = "placeholder",
            DocumentType = DocumentType.ID,
            VerificationStatus = VerificationStatus.Verified,
            DocumentExpireDate = new DateTime(2025, 9, 8),
            BankAccount = "12341234123412341234123412"
        };

        dbContext.Owners.AddRange(owner1, owner2);

        var flat1 = new Flat
        {
            Province = "Mazowieckie",
            District = "Wilanów",
            Street = "Radosna",
            Number = "4",
            Flat = 3,
            City = "Warszawa",
            PostalCode = "00-000",
            Area = 40,
            MaxNumberOfInhabitants = 2,
            ConstructionYear = 2000,
            ImagesPath = Guid.NewGuid().ToString(),
            VerificationStatus = VerificationStatus.Verified,
            Owner = owner1,
            NumberOfRooms = 2,
            Floor = 2,
            Elevator = false
        };

        dbContext.Flats.AddRange(flat1);

        dbContext.SaveChanges();
    }
}