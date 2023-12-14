using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<PropertyDto>> GetPropertiesForCurrentUserAsync(int ownerId)
        {
            var flats = await _context.Flats
                .Include(x => x.Equipment)
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();

            var houses = await _context.Houses
                .Include(x => x.Equipment)
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();

            var rooms = await _context.Rooms
                .Include(x => x.Equipment)
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();

            var flatDtos = _mapper.Map<List<PropertyDto>>(flats);
            var houseDtos = _mapper.Map<List<PropertyDto>>(houses);
            var roomDtos = _mapper.Map<List<PropertyDto>>(rooms);

            var result = new List<PropertyDto>();

            result.AddRange(flatDtos);
            result.AddRange(houseDtos);
            result.AddRange(roomDtos);

            return result;
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

            result = result.OrderBy(property => property.DateForVerificationSorting).ToList();

            return result;
        }

        public async Task AddPropertyAsync(AddEditPropertyDto input, int ownerId)
        {
            var imageDirectory = Guid.NewGuid().ToString();

            var geoInfo = await _openStreetMapService.GetCoordinatesAsync(input.Province, input.District, input.Street, input.Number, input.City, input.PostalCode);

            var equipmentList = await _context.Equipment
                .Where(e => input.Equipment
                    .Select(e => e.EquipmentId)
                    .Contains(e.EquipmentId)
                )
                .ToListAsync();

            switch (input.PropertyType)
            {
                case PropertyType.Flat:
                    if (input.NumberOfRooms is null) throw new ArgumentException("Property: NumberOfRooms is required when adding Flat");

                    if (input.Floor is null) throw new ArgumentException("Property: Floor is required when adding Flat");

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
                        ImagesPath = imageDirectory,
                        VerificationStatus = VerificationStatus.NotVerified,
                        CreationDate = DateTime.Now,
                        DateForVerificationSorting = DateTime.Now,
                        NumberOfRooms = (int)input.NumberOfRooms,
                        Floor = (int)input.Floor,
                        Elevator = input.Elevator,
                        OwnerId = ownerId,
                        Equipment = equipmentList
                    };
                    await _context.Flats.AddAsync(flat);
                    break;
                case PropertyType.Room:
                    if (input.Floor is null) throw new ArgumentException("Property: Floor is required when adding Room");

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
                        ImagesPath = imageDirectory,
                        VerificationStatus = VerificationStatus.NotVerified,
                        CreationDate = DateTime.Now,
                        DateForVerificationSorting = DateTime.Now,
                        Floor = (int)input.Floor,
                        Elevator = input.Elevator,
                        OwnerId = ownerId,
                        Equipment = equipmentList
                    };
                    await _context.Rooms.AddAsync(room);
                    break;
                case PropertyType.House:
                    if (input.NumberOfRooms is null) throw new ArgumentException("Property: NumberOfRooms is required when adding House");

                    if (input.NumberOfFloors is null) throw new ArgumentException("Property: NumberOfFloors is required when adding House");

                    if (input.PlotArea is null) throw new ArgumentException("Property: PlotArea is required when adding House");

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
                        ImagesPath = imageDirectory,
                        VerificationStatus = VerificationStatus.NotVerified,
                        CreationDate = DateTime.Now,
                        DateForVerificationSorting = DateTime.Now,
                        NumberOfRooms = (int)input.NumberOfRooms,
                        NumberOfFloors = (int)input.NumberOfFloors,
                        PlotArea = (int)input.PlotArea,
                        OwnerId = ownerId,
                        Equipment = equipmentList
                    };
                    await _context.Houses.AddAsync(house);
                    break;
            }

            await _context.SaveChangesAsync();

            if (input.TitleDeed != null && input.TitleDeed.Length > 0)
            {
                // TODO: Images refactor
                await ImageUtility.ProcessAndSaveImage(input.TitleDeed, $"Images/Properties/{imageDirectory}/TitleDeed");
            }

            if (input.Images != null && input.Images.Count > 0)
            {
                foreach (var image in input.Images)
                {
                    // TODO: Images refactor
                    await ImageUtility.ProcessAndSaveImage(image, $"Images/Properties/{imageDirectory}/Images");
                }
            }
        }

        public async Task UpdatePropertyAsync(int id, AddEditPropertyDto input, int requestUserId)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property is null) throw new ArgumentException($"Property with ID {id} not found.");
            
            if (property.OwnerId != requestUserId) throw new ForbiddenException($"You do not own this property");

            var geoInfo = await _openStreetMapService.GetCoordinatesAsync(input.Province, input.District, input.Street, input.Number, input.City, input.PostalCode);

            var equipmentList = await _context.Equipment
                .Where(e => input.Equipment
                    .Select(e => e.EquipmentId)
                    .Contains(e.EquipmentId)
                )
                .ToListAsync();

            string imageDirectoryPath = string.Empty;

            switch (input.PropertyType)
            {
                case PropertyType.Flat:
                    if (input.NumberOfRooms is null) throw new ArgumentException("Property: NumberOfRooms is required when adding Flat");

                    if (input.Floor is null) throw new ArgumentException("Property: Floor is required when adding Flat");

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
                    flat.DateForVerificationSorting = DateTime.Now;

                    if (flat.VerificationStatus == VerificationStatus.Rejected) flat.VerificationStatus = VerificationStatus.NotVerified;

                    break;
                case PropertyType.Room:
                    if (input.Floor is null) throw new ArgumentException("Property: Floor is required when adding Room");

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
                    room.DateForVerificationSorting = DateTime.Now;

                    if (room.VerificationStatus == VerificationStatus.Rejected) room.VerificationStatus = VerificationStatus.NotVerified;

                    break;
                case PropertyType.House:
                    if (input.NumberOfRooms is null) throw new ArgumentException("Property: NumberOfRooms is required when adding House");

                    if (input.NumberOfFloors is null) throw new ArgumentException("Property: NumberOfFloors is required when adding House");

                    if (input.PlotArea is null) throw new ArgumentException("Property: PlotArea is required when adding House");

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
                    house.DateForVerificationSorting = DateTime.Now;

                    if (house.VerificationStatus == VerificationStatus.Rejected) house.VerificationStatus = VerificationStatus.NotVerified;

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

        public async Task DeletePropertyAsync(int id, int requestUserId)
        {
            var propertyToDelete = await _context.Properties
                .Include(p => p.Offers)
                .FirstOrDefaultAsync(p => p.PropertyId == id);

            if (propertyToDelete is null) throw new ArgumentException($"Property with ID {id} not found.");

            if (propertyToDelete.OwnerId != requestUserId) throw new ForbiddenException($"You do not own this property");

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

        public async Task VerifyPropertyAsync(int id, bool decision)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property is null) throw new ArgumentException($"Property with ID {id} not found.");

            if (property.VerificationStatus == VerificationStatus.Verified) throw new ArgumentException($"Property with ID {id} is already verified.");

            if (decision)
            {
                property.VerificationStatus = VerificationStatus.Verified;

                var titleDeedDirectoryPath = Path.Combine("Images/Properties", property.ImagesPath, "TitleDeed");

                await ImageUtility.DeleteDirectory(titleDeedDirectoryPath);
            }
            else
            {
                property.VerificationStatus = VerificationStatus.Rejected;
            }

            await _context.SaveChangesAsync();
        }
    }
}
