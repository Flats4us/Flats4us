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
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;

        public PropertyService(Flats4usContext context,
            IOpenStreetMapService openStreetMapService,
            IFileUploadService fileUploadService,
            IMapper mapper)
        {
            _context = context;
            _openStreetMapService = openStreetMapService;
            _fileUploadService = fileUploadService;
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
                            .ThenInclude(ro => ro.Student)
                                .ThenInclude(s => s.ProfilePicture)
                        .Include(f => f.Equipment)
                        .Include(f => f.Images)
                        .FirstOrDefaultAsync(f => f.PropertyId == id);
                    result = _mapper.Map<PropertyDto>(flat);
                    result.Offers = _mapper.Map<List<SimpleOfferForPropertyDetailsDto>>(flat.Offers);
                    break;
                case Room:
                    var room = await _context.Rooms
                        .Include(r => r.Offers)
                        .Include(r => r.RentOpinions)
                            .ThenInclude(ro => ro.Student)
                                .ThenInclude(s => s.ProfilePicture)
                        .Include(r => r.Equipment)
                        .Include(r => r.Images)
                        .FirstOrDefaultAsync(r => r.PropertyId == id);
                    result = _mapper.Map<PropertyDto>(room);
                    result.Offers = _mapper.Map<List<SimpleOfferForPropertyDetailsDto>>(room.Offers);
                    break;
                case House:
                    var house = await _context.Houses
                        .Include(h => h.Offers)
                        .Include(h => h.RentOpinions)
                            .ThenInclude(ro => ro.Student)
                                .ThenInclude(s => s.ProfilePicture)
                        .Include(h => h.Equipment)
                        .Include(h => h.Images)
                        .FirstOrDefaultAsync(h => h.PropertyId == id);
                    result = _mapper.Map<PropertyDto>(house);
                    result.Offers = _mapper.Map<List<SimpleOfferForPropertyDetailsDto>>(house.Offers);
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
                .Include(x => x.Images)
                .Include(x => x.RentOpinions)
                    .ThenInclude(ro => ro.Student)
                        .ThenInclude(s => s.ProfilePicture)
                .Where(p => p.OwnerId == ownerId);

            IQueryable<House> houseQuery = _context.Houses
                .Include(x => x.Equipment)
                .Include(x => x.Images)
                .Include(x => x.RentOpinions)
                    .ThenInclude(ro => ro.Student)
                        .ThenInclude(s => s.ProfilePicture)
                .Where(p => p.OwnerId == ownerId);

            IQueryable<Room> roomQuery = _context.Rooms
                .Include(x => x.Equipment)
                .Include(x => x.Images)
                .Include(x => x.RentOpinions)
                    .ThenInclude(ro => ro.Student)
                        .ThenInclude(s => s.ProfilePicture)
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
                .ToListAsync();

            var houses = await _context.Houses
                .Include(h => h.Owner)
                .Include(h => h.TitleDeed)
                .Include(h => h.Images)
                .ToListAsync();

            var rooms = await _context.Rooms
                .Include(r => r.Owner)
                .Include(r => r.TitleDeed)
                .Include(r => r.Images)
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
                .OrderBy(property =>
                     property.VerificationStatus == VerificationStatus.NotVerified ? 0 :
                     property.VerificationStatus == VerificationStatus.Rejected ? 1 :
                     property.VerificationStatus == VerificationStatus.Verified ? 2 : 3)
                .ThenBy(property =>
                    property.VerificationStatus == VerificationStatus.NotVerified ? property.DateForVerificationSorting :
                    property.VerificationOrRejectionDate)
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
            var property = await _context.Properties
                .Include(u => u.Images)
                .Include(u => u.TitleDeed)
                .FirstOrDefaultAsync(x => x.PropertyId == propertyId);

            if (property is null) throw new Exception($"Cannot find property ID: {propertyId}");
                
            if (property.OwnerId != requestUserId) throw new ForbiddenException($"You do not own this property");

            if (fileId == property.TitleDeed?.Name)
            {
                await _fileUploadService.DeleteFileByNameAsync(fileId);
                property.TitleDeed = null;
            }
            else if (property.Images.Any(x => x.Name == fileId))
            {
                var imageToDelete = property.Images.FirstOrDefault(x => x.Name == fileId);
                if (imageToDelete != null)
                {
                    await _fileUploadService.DeleteFileByNameAsync(fileId);
                    property.Images.Remove(imageToDelete);
                }
            }
            else
            {
                throw new Exception("Cannot delete or find file.");
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddPropertyFilesAsync(PropertyFilesDto input, int propertyId, int requestUserId)
        {
            var property = await _context.Properties.FindAsync(propertyId);

            if (property is null) throw new Exception($"Cannot find property ID: {propertyId}");

            if (property.OwnerId != requestUserId) throw new ForbiddenException($"You do not own this property");

            if (input.TitleDeed != null && input.TitleDeed.Length > 0)
            {
                if (property.TitleDeed != null)
                {
                    await _fileUploadService.DeleteFileByNameAsync(property.TitleDeed.Name);
                }

                property.TitleDeed = await _fileUploadService.CreateFileFromIFormFileAsync(input.TitleDeed);
            }

            if (input.Images != null && input.Images.Count > 0)
            {
                foreach (var image in input.Images)
                {
                    property.Images.Add(await _fileUploadService.CreateFileFromIFormFileAsync(image));
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
                    
                    if (flat.VerificationStatus == VerificationStatus.Rejected)
                    {
                        flat.VerificationStatus = VerificationStatus.NotVerified;
                        flat.VerificationOrRejectionDate = null;
                        flat.DateForVerificationSorting = DateTime.Now;
                    }

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

                    if (room.VerificationStatus == VerificationStatus.Rejected)
                    {
                        room.VerificationStatus = VerificationStatus.NotVerified;
                        room.VerificationOrRejectionDate = null;
                        room.DateForVerificationSorting = DateTime.Now;
                    }

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

                    if (house.VerificationStatus == VerificationStatus.Rejected)
                    {
                        house.VerificationStatus = VerificationStatus.NotVerified;
                        house.VerificationOrRejectionDate = null;
                        house.DateForVerificationSorting = DateTime.Now;
                    }

                    break;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeletePropertyAsync(int id, int requestUserId)
        {
            var propertyToDelete = await _context.Properties
                .Include(p => p.Offers)
                .Include(p => p.Images)
                .Include(p => p.TitleDeed)
                .FirstOrDefaultAsync(p => p.PropertyId == id);

            if (propertyToDelete is null) throw new ArgumentException($"Property with ID {id} not found.");

            if (propertyToDelete.OwnerId != requestUserId) throw new ForbiddenException($"You do not own this property");

            if (propertyToDelete.Offers.Any())
            {
                throw new InvalidOperationException($"Property with ID {id} cannot be deleted because it has associated offers.");
            }

            foreach (var image in propertyToDelete.Images)
            {
                await _fileUploadService.DeleteFileByNameAsync(image.Name);
            }

            if (propertyToDelete.TitleDeed != null)
            {
                await _fileUploadService.DeleteFileByNameAsync(propertyToDelete.TitleDeed.Name);
            }

            _context.Properties.Remove(propertyToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task VerifyPropertyAsync(int id, bool decision)
        {
            var property = await _context.Properties
                .Include(p => p.TitleDeed)
                .FirstOrDefaultAsync(p => p.PropertyId == id);

            if (property is null) throw new ArgumentException($"Property with ID {id} not found.");

            if (property.VerificationStatus == VerificationStatus.Verified) throw new ArgumentException($"Property with ID {id} is already verified.");

            if (decision)
            {
                property.VerificationStatus = VerificationStatus.Verified;
                property.VerificationOrRejectionDate = DateTime.Now;
                property.DateForVerificationSorting = null;

                await _fileUploadService.DeleteFileByNameAsync(property.TitleDeed.Name);

                property.TitleDeed = null;
            }
            else
            {
                property.VerificationStatus = VerificationStatus.Rejected;
                property.VerificationOrRejectionDate = DateTime.Now;
                property.DateForVerificationSorting = null;
            }

            await _context.SaveChangesAsync();
        }
    }
}
