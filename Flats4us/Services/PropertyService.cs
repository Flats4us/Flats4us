using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Flats4us.Services
{
    public class PropertyService : IPropertyService
    {
        public readonly Flats4usContext _context;
        private readonly IOpenStreetMapService _openStreetMapService;

        public PropertyService(Flats4usContext context,
            IOpenStreetMapService openStreetMapService)
        {
            _context = context;
            _openStreetMapService = openStreetMapService;
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

        public async Task AddPropertyAsync(AddEditPropertyDto input)
        {
            var imageFolder = Guid.NewGuid().ToString();

            var geoInfo = await _openStreetMapService.GetCoordinatesAsync(input.Province, input.District, input.Street, input.Number, input.City, input.PostalCode);

            var equipmentDtoList = JsonConvert.DeserializeObject<List<EquipmentDto>>(input.EquipmentJson);

            var equipmentList = await _context.Equipment
                .Where(e => equipmentDtoList
                    .Select(e => e.EquipmentId)
                    .Contains(e.EquipmentId)
                )
                .ToListAsync();

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
                        GeoLat = geoInfo.Latitude,
                        GeoLon = geoInfo.Longitude,
                        Area = input.Area,
                        MaxNumberOfInhabitants = input.MaxNumberOfInhabitants,
                        ConstructionYear = input.ConstructionYear,
                        ImagesPath = imageFolder,
                        VerificationStatus = VerificationStatus.NotVerified,
                        NumberOfRooms = input.NumberOfRooms,
                        Floor = input.Floor,
                        Elevator = input.Elevator,
                        OwnerId = input.OwnerId,
                        Equipment = equipmentList
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
                        GeoLat = geoInfo.Latitude,
                        GeoLon = geoInfo.Longitude,
                        Area = input.Area,
                        MaxNumberOfInhabitants = input.MaxNumberOfInhabitants,
                        ConstructionYear = input.ConstructionYear,
                        ImagesPath = imageFolder,
                        VerificationStatus = VerificationStatus.NotVerified,
                        Floor = input.Floor,
                        Elevator = input.Elevator,
                        OwnerId = input.OwnerId,
                        Equipment = equipmentList
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
                        GeoLat = geoInfo.Latitude,
                        GeoLon = geoInfo.Longitude,
                        Area = input.Area,
                        MaxNumberOfInhabitants = input.MaxNumberOfInhabitants,
                        ConstructionYear = input.ConstructionYear,
                        ImagesPath = imageFolder,
                        VerificationStatus = VerificationStatus.NotVerified,
                        NumberOfRooms = input.NumberOfRooms,
                        NumberOfFloors = input.NumberOfFloors,
                        PlotArea = input.PlotArea,
                        OwnerId = input.OwnerId,
                        Equipment = equipmentList
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

        public async Task UpdateProperyAsync(int id, AddEditPropertyDto input)
        {
            if (await _context.Properties.FindAsync(id) is null)
            {
                throw new ArgumentException($"Property with ID {id} not found.");
            }

            var geoInfo = await _openStreetMapService.GetCoordinatesAsync(input.Province, input.District, input.Street, input.Number, input.City, input.PostalCode);

            var equipmentDtoList = JsonConvert.DeserializeObject<List<EquipmentDto>>(input.EquipmentJson);

            var equipmentList = await _context.Equipment
                .Where(e => equipmentDtoList
                    .Select(e => e.EquipmentId)
                    .Contains(e.EquipmentId)
                )
                .ToListAsync();

            string imageDirectoryPath = string.Empty;

            switch (input.PropertyType)
            {
                case PropertyType.Flat:
                    var flat = await _context.Flats
                        .Include(f => f.Equipment)
                        .FirstOrDefaultAsync(f => f.PropertyId == id);
                    if (flat is null) throw new ArgumentException($"Flat with ID {id} not found.");

                    imageDirectoryPath = Path.Combine("Images/Properties", flat.ImagesPath);

                    flat.Province = input.Province;
                    flat.District = input.District;
                    flat.Street = input.Street;
                    flat.Number = input.Number;
                    flat.Flat = input.Flat;
                    flat.City = input.City;
                    flat.PostalCode = input.PostalCode;
                    flat.GeoLat = geoInfo.Latitude;
                    flat.GeoLon = geoInfo.Longitude;
                    flat.Area = input.Area;
                    flat.MaxNumberOfInhabitants = input.MaxNumberOfInhabitants;
                    flat.ConstructionYear = input.ConstructionYear;
                    flat.NumberOfRooms = input.NumberOfRooms;
                    flat.Floor = input.Floor;
                    flat.Elevator = input.Elevator;
                    flat.Equipment = equipmentList;
                    break;
                case PropertyType.Room:
                    var room = await _context.Rooms
                        .Include(f => f.Equipment)
                        .FirstOrDefaultAsync(f => f.PropertyId == id);
                    if (room is null) throw new ArgumentException($"Room with ID {id} not found.");

                    imageDirectoryPath = Path.Combine("Images/Properties", room.ImagesPath);

                    room.Province = input.Province;
                    room.District = input.District;
                    room.Street = input.Street;
                    room.Number = input.Number;
                    room.Flat = input.Flat;
                    room.City = input.City;
                    room.PostalCode = input.PostalCode;
                    room.GeoLat = geoInfo.Latitude;
                    room.GeoLon = geoInfo.Longitude;
                    room.Area = input.Area;
                    room.MaxNumberOfInhabitants = input.MaxNumberOfInhabitants;
                    room.ConstructionYear = input.ConstructionYear;
                    room.Floor = input.Floor;
                    room.Elevator = input.Elevator;
                    room.Equipment = equipmentList;
                    break;
                case PropertyType.House:
                    var house = await _context.Houses
                        .Include(f => f.Equipment)
                        .FirstOrDefaultAsync(f => f.PropertyId == id);
                    if (house is null) throw new ArgumentException($"House with ID {id} not found.");

                    imageDirectoryPath = Path.Combine("Images/Properties", house.ImagesPath);

                    house.Province = input.Province;
                    house.District = input.District;
                    house.Street = input.Street;
                    house.Number = input.Number;
                    house.Flat = input.Flat;
                    house.City = input.City;
                    house.PostalCode = input.PostalCode;
                    house.GeoLat = geoInfo.Latitude;
                    house.GeoLon = geoInfo.Longitude;
                    house.Area = input.Area;
                    house.MaxNumberOfInhabitants = input.MaxNumberOfInhabitants;
                    house.ConstructionYear = input.ConstructionYear;
                    house.NumberOfRooms = input.NumberOfRooms;
                    house.NumberOfFloors = input.NumberOfFloors;
                    house.PlotArea = input.PlotArea;
                    house.Equipment = equipmentList;
                    break;
            }

                await _context.SaveChangesAsync();            

            await ImageUtility.DeleteDirectory(imageDirectoryPath);

            if (input.TitleDeed != null && input.TitleDeed.Length > 0)
            {
                await ImageUtility.ProcessAndSaveImage(input.TitleDeed, $"{imageDirectoryPath}/TitleDeed");
            }

            if (input.Images != null && input.Images.Count > 0)
            {
                foreach (var image in input.Images)
                {
                    await ImageUtility.ProcessAndSaveImage(image, $"{imageDirectoryPath}/Images");
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
