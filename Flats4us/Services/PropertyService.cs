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

        public async Task<PropertyDto> GetPropertyByIdAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property is null) throw new ArgumentException($"Property with ID {id} not found.");

            PropertyDto result;

            switch (property)
            {
                case Flat:
                    var flat = await _context.Flats
                        .Include(f => f.Offers)
                        .Include(f => f.RentOpinions)
                        .Include(f => f.Equipment)
                        .FirstOrDefaultAsync(f => f.PropertyId == id);
                    result = _mapper.Map<PropertyDto>(flat);
                    result.OfferIds = flat.Offers.Select(o => o.OfferId).ToList();
                    break;
                case Room:
                    var room = await _context.Rooms
                        .Include(r => r.Offers)
                        .Include(r => r.RentOpinions)
                        .Include(r => r.Equipment)
                        .FirstOrDefaultAsync(r => r.PropertyId == id);
                    result = _mapper.Map<PropertyDto>(room);
                    result.OfferIds = room.Offers.Select(o => o.OfferId).ToList();
                    break;
                case House:
                    var house = await _context.Houses
                        .Include(h => h.Offers)
                        .Include(h => h.RentOpinions)
                        .Include(h => h.Equipment)
                        .FirstOrDefaultAsync(h => h.PropertyId == id);
                    result = _mapper.Map<PropertyDto>(house);
                    result.OfferIds = house.Offers.Select(o => o.OfferId).ToList();
                    break;
                default:
                    throw new ArgumentException($"Cannot get this property");
            }

            return result;
        }

        public async Task<List<PropertyDto>> GetPropertiesForCurrentUserAsync(int ownerId, bool showOnlyVerified)
        {
            IQueryable<Flat> flatQuery = _context.Flats
                .Include(x => x.Equipment)
                .Where(p => p.OwnerId == ownerId);

            IQueryable<House> houseQuery = _context.Houses
                .Include(x => x.Equipment)
                .Where(p => p.OwnerId == ownerId);

            IQueryable<Room> roomQuery = _context.Rooms
                .Include(x => x.Equipment)
                .Where(p => p.OwnerId == ownerId);

            if (showOnlyVerified)
            {
                flatQuery = flatQuery.Where(p => p.VerificationStatus == VerificationStatus.Verified);
                houseQuery = houseQuery.Where(p => p.VerificationStatus == VerificationStatus.Verified);
                roomQuery = roomQuery.Where(p => p.VerificationStatus == VerificationStatus.Verified);
            }

            var flats = await flatQuery.ToListAsync();
            var houses = await houseQuery.ToListAsync();
            var rooms = await roomQuery.ToListAsync();

            var flatDtos = _mapper.Map<List<PropertyDto>>(flats);
            var houseDtos = _mapper.Map<List<PropertyDto>>(houses);
            var roomDtos = _mapper.Map<List<PropertyDto>>(rooms);

            var result = new List<PropertyDto>();

            result.AddRange(flatDtos);
            result.AddRange(houseDtos);
            result.AddRange(roomDtos);

            return result;
        }

        public async Task<CountedListDto<PropertyForVerificationDto>> GetNotVerifiedPropertiesAsync(PaginatorDto input)
        {
            var flats = await _context.Flats
                .Include(f => f.Owner)
                .Include(f => f.TitleDeed)
                .Include(f => f.Images)
                .Where(f => f.VerificationStatus == VerificationStatus.NotVerified)
                .ToListAsync();

            var houses = await _context.Houses
                .Include(h => h.Owner)
                .Include(h => h.TitleDeed)
                .Include(h => h.Images)
                .Where(h => h.VerificationStatus == VerificationStatus.NotVerified)
                .ToListAsync();

            var rooms = await _context.Rooms
                .Include(r => r.Owner)
                .Include(r => r.TitleDeed)
                .Include(r => r.Images)
                .Where(r => r.VerificationStatus == VerificationStatus.NotVerified)
                .ToListAsync();

            var flatDtos = _mapper.Map<List<PropertyForVerificationDto>>(flats);
            var houseDtos = _mapper.Map<List<PropertyForVerificationDto>>(houses);
            var roomDtos = _mapper.Map<List<PropertyForVerificationDto>>(rooms);

            var properties = new List<PropertyForVerificationDto>();

            properties.AddRange(flatDtos);
            properties.AddRange(houseDtos);
            properties.AddRange(roomDtos);

            var totalCount = properties.Count();

            properties = properties
                .OrderBy(property => property.DateForVerificationSorting)
                .Skip((input.PageNumber - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToList();

            var result = new CountedListDto<PropertyForVerificationDto>(properties, totalCount);

            return result;
        }

        public async Task<OutputDto<int>> AddPropertyAsync(AddEditPropertyDto input, int ownerId)
        {
            var geoInfo = await _openStreetMapService.GetCoordinatesAsync(input.Province, input.District, input.Street, input.Number, input.City, input.PostalCode);

            var equipmentList = await _context.Equipment
                .Where(e => input.EquipmentIds
                    .Contains(e.EquipmentId)
                )
                .ToListAsync();

            int propertyId = 0;

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
                        GeoLat = geoInfo.Lat,
                        GeoLon = geoInfo.Lon,
                        Area = input.Area,
                        MaxNumberOfInhabitants = input.MaxNumberOfInhabitants,
                        ConstructionYear = input.ConstructionYear,
                        VerificationStatus = VerificationStatus.NotVerified,
                        CreationDate = DateTime.Now,
                        DateForVerificationSorting = DateTime.Now,
                        NumberOfRooms = (int)input.NumberOfRooms,
                        Floor = (int)input.Floor,
                        OwnerId = ownerId,
                        Equipment = equipmentList
                    };
                    await _context.Flats.AddAsync(flat);

                    await _context.SaveChangesAsync();

                    propertyId = flat.PropertyId;

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
                        GeoLat = geoInfo.Lat,
                        GeoLon = geoInfo.Lon,
                        Area = input.Area,
                        MaxNumberOfInhabitants = input.MaxNumberOfInhabitants,
                        ConstructionYear = input.ConstructionYear,
                        VerificationStatus = VerificationStatus.NotVerified,
                        CreationDate = DateTime.Now,
                        DateForVerificationSorting = DateTime.Now,
                        Floor = (int)input.Floor,
                        OwnerId = ownerId,
                        Equipment = equipmentList
                    };
                    await _context.Rooms.AddAsync(room);

                    await _context.SaveChangesAsync();

                    propertyId = room.PropertyId;

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
                        GeoLat = geoInfo.Lat,
                        GeoLon = geoInfo.Lon,
                        Area = input.Area,
                        MaxNumberOfInhabitants = input.MaxNumberOfInhabitants,
                        ConstructionYear = input.ConstructionYear,
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

                    await _context.SaveChangesAsync();

                    propertyId = house.PropertyId;

                    break;
            }

            if (propertyId == 0)
            {
                throw new Exception("Property ID has not been properly set.");
            }

            var result = new OutputDto<int>(propertyId);

            return result;
        }

        public async Task DeletePropertyFileAsync(int propertyId, string fileId, int requestUserId)
        {
            try
            {
                var property = await _context.Properties.FindAsync(propertyId);

                if (property is null) throw new Exception($"Cannot find property ID: {propertyId}");
                
                if (property.OwnerId != requestUserId) throw new ForbiddenException($"You do not own this property");

                // TODO: Images refactor
                //await ImageUtility.DeletePropertyFileAsync(property.ImagesPath, fileId);
            }
            catch (IOException ex)
            {
                throw new IOException($"File operation failed: {ex.Message}");
            }
        }

        public async Task AddPropertyFilesAsync(PropertyFilesDto input, int propertyId, int requestUserId)
        {
            var property = await _context.Properties.FindAsync(propertyId);

            if (property is null) throw new Exception($"Cannot find property ID: {propertyId}");

            if (property.OwnerId != requestUserId) throw new ForbiddenException($"You do not own this property");

            if (input.TitleDeed != null && input.TitleDeed.Length > 0)
            {
                property.TitleDeed = new Entities.File(input.TitleDeed);
            }

            if (input.Images != null && input.Images.Count > 0)
            {
                foreach (var image in input.Images)
                {
                    property.Images.Add(new Entities.File(image));
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePropertyAsync(int id, AddEditPropertyDto input, int requestUserId)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property is null) throw new ArgumentException($"Property with ID {id} not found.");
            
            if (property.OwnerId != requestUserId) throw new ForbiddenException($"You do not own this property");

            var geoInfo = await _openStreetMapService.GetCoordinatesAsync(input.Province, input.District, input.Street, input.Number, input.City, input.PostalCode);

            var equipmentList = await _context.Equipment
                .Where(e => input.EquipmentIds
                    .Contains(e.EquipmentId)
                )
                .ToListAsync();

            switch (input.PropertyType)
            {
                case PropertyType.Flat:
                    if (input.NumberOfRooms is null) throw new ArgumentException("Property: NumberOfRooms is required when adding Flat");

                    if (input.Floor is null) throw new ArgumentException("Property: Floor is required when adding Flat");

                    var flat = await _context.Flats
                        .Include(f => f.Equipment)
                        .FirstOrDefaultAsync(f => f.PropertyId == id);

                    if (flat is null) throw new ArgumentException($"Flat with ID {id} not found.");

                    flat.Province = input.Province;
                    flat.District = input.District;
                    flat.Street = input.Street;
                    flat.Number = input.Number;
                    flat.Flat = input.Flat;
                    flat.City = input.City;
                    flat.PostalCode = input.PostalCode;
                    flat.GeoLat = geoInfo.Lat;
                    flat.GeoLon = geoInfo.Lon;
                    flat.Area = input.Area;
                    flat.MaxNumberOfInhabitants = input.MaxNumberOfInhabitants;
                    flat.ConstructionYear = input.ConstructionYear;
                    flat.NumberOfRooms = (int)input.NumberOfRooms;
                    flat.Floor = (int)input.Floor;
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

                    room.Province = input.Province;
                    room.District = input.District;
                    room.Street = input.Street;
                    room.Number = input.Number;
                    room.Flat = input.Flat;
                    room.City = input.City;
                    room.PostalCode = input.PostalCode;
                    room.GeoLat = geoInfo.Lat;
                    room.GeoLon = geoInfo.Lon;
                    room.Area = input.Area;
                    room.MaxNumberOfInhabitants = input.MaxNumberOfInhabitants;
                    room.ConstructionYear = input.ConstructionYear;
                    room.Floor = (int)input.Floor;
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

                    house.Province = input.Province;
                    house.District = input.District;
                    house.Street = input.Street;
                    house.Number = input.Number;
                    house.Flat = input.Flat;
                    house.City = input.City;
                    house.PostalCode = input.PostalCode;
                    house.GeoLat = geoInfo.Lat;
                    house.GeoLon = geoInfo.Lon;
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
            //string imageDirectoryPath = Path.Combine("Images/Properties", propertyToDelete.ImagesPath);

            //await ImageUtility.DeleteDirectory(imageDirectoryPath);
        }

        public async Task VerifyPropertyAsync(int id, bool decision)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property is null) throw new ArgumentException($"Property with ID {id} not found.");

            if (property.VerificationStatus == VerificationStatus.Verified) throw new ArgumentException($"Property with ID {id} is already verified.");

            if (decision)
            {
                property.VerificationStatus = VerificationStatus.Verified;

                // TODO: Images refactor
                //var titleDeedDirectoryPath = Path.Combine("Images/Properties", property.ImagesPath, "TitleDeed");

                //await ImageUtility.DeleteDirectory(titleDeedDirectoryPath);
            }
            else
            {
                property.VerificationStatus = VerificationStatus.Rejected;
            }

            await _context.SaveChangesAsync();
        }
    }
}
