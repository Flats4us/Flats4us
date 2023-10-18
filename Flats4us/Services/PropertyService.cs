using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class PropertyService : IPropertyService
    {
        public readonly Flats4usContext _context;

        public PropertyService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<List<PropertyDto>> GetNotVerifiedPropertiesAsync()
        {
            var result = new List<PropertyDto>();

            result.AddRange(await _context.Flats
                .Where(p => p.VerificationStatus == VerificationStatus.NotVerified)
                .Select(p => new PropertyDto
                {
                    PropertyId = p.PropertyId,
                    PropertyType = PropertyType.Flat,
                    Province = p.Province,
                    District = p.District,
                    Street = p.Street,
                    Number = p.Number,
                    Flat = p.Flat,
                    City = p.City,
                    PostalCode = p.PostalCode,
                    Area = p.Area,
                    MaxNumberOfInhabitants = p.MaxNumberOfInhabitants,
                    ConstructionYear = p.ConstructionYear,
                    ImagesPath = p.ImagesPath,
                    VerificationStatus = VerificationStatus.NotVerified,
                    NumberOfRooms = p.NumberOfRooms,
                    Floor = p.Floor,
                    Elevator = p.Elevator
                })
                .ToListAsync()
            );

            result.AddRange(await _context.Houses
                .Where(p => p.VerificationStatus == VerificationStatus.NotVerified)
                .Select(p => new PropertyDto
                {
                    PropertyId = p.PropertyId,
                    PropertyType = PropertyType.House,
                    Province = p.Province,
                    District = p.District,
                    Street = p.Street,
                    Number = p.Number,
                    Flat = p.Flat,
                    City = p.City,
                    PostalCode = p.PostalCode,
                    Area = p.Area,
                    MaxNumberOfInhabitants = p.MaxNumberOfInhabitants,
                    ConstructionYear = p.ConstructionYear,
                    ImagesPath = p.ImagesPath,
                    VerificationStatus = VerificationStatus.NotVerified,
                    NumberOfRooms = p.NumberOfRooms,
                    NumberOfFloors = p.NumberOfFloors,
                    PlotArea = p.PlotArea,
                })
                .ToListAsync()
            );

            result.AddRange(await _context.Rooms
                .Where(p => p.VerificationStatus == VerificationStatus.NotVerified)
                .Select(p => new PropertyDto
                {
                    PropertyId = p.PropertyId,
                    PropertyType = PropertyType.Room,
                    Province = p.Province,
                    District = p.District,
                    Street = p.Street,
                    Number = p.Number,
                    Flat = p.Flat,
                    City = p.City,
                    PostalCode = p.PostalCode,
                    Area = p.Area,
                    MaxNumberOfInhabitants = p.MaxNumberOfInhabitants,
                    ConstructionYear = p.ConstructionYear,
                    ImagesPath = p.ImagesPath,
                    VerificationStatus = VerificationStatus.NotVerified,
                    Floor = p.Floor,
                    Elevator = p.Elevator
                })
                .ToListAsync()
            );

            return result;
        }

        public async Task AddPropertyAsync(NewPropertyDto input)
        {
            var imageFolder = Guid.NewGuid().ToString();

            switch (input.PropertyType)
            {
                case PropertyType.Flat:
                    var flat = new Flat
                    {
                        Province = input.Province,
                        District = input.District,
                        Street = input.Street,
                        Number = input.Number,
                        Flat = input.Flat,
                        City = input.City,
                        PostalCode = input.PostalCode,
                        Area = input.Area,
                        MaxNumberOfInhabitants = input.MaxNumberOfInhabitants,
                        ConstructionYear = input.ConstructionYear,
                        ImagesPath = imageFolder,
                        VerificationStatus = VerificationStatus.NotVerified,
                        NumberOfRooms = input.NumberOfRooms,
                        Floor = input.Floor,
                        Elevator = input.Elevator
                    };
                    await _context.Flats.AddAsync(flat);
                    break;
                case PropertyType.Room:
                    var room = new Room
                    {
                        Province = input.Province,
                        District = input.District,
                        Street = input.Street,
                        Number = input.Number,
                        Flat = input.Flat,
                        City = input.City,
                        PostalCode = input.PostalCode,
                        Area = input.Area,
                        MaxNumberOfInhabitants = input.MaxNumberOfInhabitants,
                        ConstructionYear = input.ConstructionYear,
                        ImagesPath = imageFolder,
                        VerificationStatus = VerificationStatus.NotVerified,
                        Floor = input.Floor,
                        Elevator = input.Elevator
                    };
                    await _context.Rooms.AddAsync(room);
                    break;
                case PropertyType.House:
                    var house = new House
                    {
                        Province = input.Province,
                        District = input.District,
                        Street = input.Street,
                        Number = input.Number,
                        Flat = input.Flat,
                        City = input.City,
                        PostalCode = input.PostalCode,
                        Area = input.Area,
                        MaxNumberOfInhabitants = input.MaxNumberOfInhabitants,
                        ConstructionYear = input.ConstructionYear,
                        ImagesPath = imageFolder,
                        VerificationStatus = VerificationStatus.NotVerified,
                        NumberOfRooms = input.NumberOfRooms,
                        NumberOfFloors = input.NumberOfFloors,
                        PlotArea = input.PlotArea
                    };
                    await _context.Houses.AddAsync(house);
                    break;
            }

            await _context.SaveChangesAsync();

            if (input.TitleDeed != null && input.TitleDeed.Length > 0)
            {
                await ImageUtility.ProcessAndSaveImage(input.TitleDeed, $"Images/Properties/{imageFolder}/TitleDeed");
            }

            if (input.Images != null && input.Images.Count > 0)
            {
                foreach (var image in input.Images)
                {
                    await ImageUtility.ProcessAndSaveImage(image, $"Images/Properties/{imageFolder}/Images");
                }
            }
        }

        public async Task DeletePropertyAsync(int id)
        {
            var propertyToDelete = await _context.Properties
                .Include(p => p.Offers)
                .FirstOrDefaultAsync(p => p.PropertyId == id);

            if (propertyToDelete != null)
            {
                if (propertyToDelete.Offers.Any())
                {
                    throw new InvalidOperationException($"Property with ID {id} cannot be deleted because it has associated offers.");
                }

                _context.Properties.Remove(propertyToDelete);
                await _context.SaveChangesAsync();

                string imageDirectoryPath = Path.Combine("Images/Properties", propertyToDelete.ImagesPath);

                await ImageUtility.DeleteDirectory(imageDirectoryPath);
            }
            else
            {
                throw new ArgumentException($"Property with ID {id} not found.");
            }
        }
    }
}
