using AutoMapper;
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
        private readonly IMapper _mapper;

        public PropertyService(Flats4usContext context,
            IOpenStreetMapService openStreetMapService,
            IMapper mapper)
        {
            _context = context;
            _openStreetMapService = openStreetMapService;
            _mapper = mapper;
        }

        public async Task<List<PropertyForVerificationDto>> GetNotVerifiedPropertiesAsync()
        {
            var flats = await _context.Flats
                .Include(x => x.Owner)
                .Where(p => p.VerificationStatus == VerificationStatus.NotVerified)
                .ToListAsync();

            var houses = await _context.Houses
                .Include(x => x.Owner)
                .Where(p => p.VerificationStatus == VerificationStatus.NotVerified)
                .ToListAsync();

            var rooms = await _context.Rooms
                .Include(x => x.Owner)
                .Where(p => p.VerificationStatus == VerificationStatus.NotVerified)
                .ToListAsync();

            var flatDtos = _mapper.Map<List<PropertyForVerificationDto>>(flats);
            var houseDtos = _mapper.Map<List<PropertyForVerificationDto>>(houses);
            var roomDtos = _mapper.Map<List<PropertyForVerificationDto>>(rooms);

            var result = new List<PropertyForVerificationDto>();

            result.AddRange(flatDtos);
            result.AddRange(houseDtos);
            result.AddRange(roomDtos);

            result = result.OrderBy(user => user.CreationDate).ToList();

            return result;
        }

        public async Task AddPropertyAsync(AddEditPropertyDto input)
        {
            var imageFolder = Guid.NewGuid().ToString();

            var geoInfo = await _openStreetMapService.GetCoordinatesAsync(input.Province, input.District, input.Street, input.Number, input.City, input.PostalCode);

            var equipmentDtoList = JsonConvert.DeserializeObject<List<EquipmentDto>>(input.EquipmentJson);

            // TODO: Try change equipment mapping

            var equipmentList = await _context.Equipment
                .Where(e => equipmentDtoList
                    .Select(e => e.EquipmentId)
                    .Contains(e.EquipmentId)
                )
                .ToListAsync();

            // TODO: Use AutoMapper
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
                        CreationDate = DateTime.Now,
                        NumberOfRooms = (int)input.NumberOfRooms,
                        Floor = (int)input.Floor,
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
                        CreationDate = DateTime.Now,
                        Floor = (int)input.Floor,
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
                        CreationDate = DateTime.Now,
                        NumberOfRooms = (int)input.NumberOfRooms,
                        NumberOfFloors = (int)input.NumberOfFloors,
                        PlotArea = (int)input.PlotArea,
                        OwnerId = input.OwnerId,
                        Equipment = equipmentList
                    };
                    await _context.Houses.AddAsync(house);
                    break;
            }

            await _context.SaveChangesAsync();

            if (input.TitleDeed != null && input.TitleDeed.Length > 0)
            {
                // TODO: Images refactor
                await ImageUtility.ProcessAndSaveImage(input.TitleDeed, $"Images/Properties/{imageFolder}/TitleDeed");
            }

            if (input.Images != null && input.Images.Count > 0)
            {
                foreach (var image in input.Images)
                {
                    // TODO: Images refactor
                    await ImageUtility.ProcessAndSaveImage(image, $"Images/Properties/{imageFolder}/Images");
                }
            }
        }

        public async Task UpdatePropertyAsync(int id, AddEditPropertyDto input)
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
                    flat.NumberOfRooms = (int)input.NumberOfRooms;
                    flat.Floor = (int)input.Floor;
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
                    room.Floor = (int)input.Floor;
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
                    house.NumberOfRooms = (int)input.NumberOfRooms;
                    house.NumberOfFloors = (int)input.NumberOfFloors;
                    house.PlotArea = (int)input.PlotArea;
                    house.Equipment = equipmentList;
                    break;
            }

                await _context.SaveChangesAsync();            

            await ImageUtility.DeleteDirectory(imageDirectoryPath);

            if (input.TitleDeed != null && input.TitleDeed.Length > 0)
            {
                // TODO: Images refactor
                await ImageUtility.ProcessAndSaveImage(input.TitleDeed, $"{imageDirectoryPath}/TitleDeed");
            }

            if (input.Images != null && input.Images.Count > 0)
            {
                foreach (var image in input.Images)
                {
                    // TODO: Images refactor
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

                // TODO: Images refactor
                string imageDirectoryPath = Path.Combine("Images/Properties", propertyToDelete.ImagesPath);

                await ImageUtility.DeleteDirectory(imageDirectoryPath);
            }
            else
            {
                throw new ArgumentException($"Property with ID {id} not found.");
            }
        }

        public async Task VerifyPropertyAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property is null)
            {
                throw new ArgumentException($"Property with ID {id} not found.");
            }

            if (property.VerificationStatus == VerificationStatus.Verified)
            {
                throw new ArgumentException($"Property with ID {id} is already verified.");
            }

            property.VerificationStatus = VerificationStatus.Verified;

            var titleDeedDirectoryPath = Path.Combine("Images/Properties", property.ImagesPath, "TitleDeed");

            await ImageUtility.DeleteDirectory(titleDeedDirectoryPath);

            await _context.SaveChangesAsync();
        }
    }
}
