using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Flats4us.Services
{
    public class OfferService : IOfferService
    {
        public readonly Flats4usContext _context;
        private readonly IOpenStreetMapService _openStreetMapService;

        public OfferService(Flats4usContext context,
            IOpenStreetMapService openStreetMapService)
        {
            _context = context;
            _openStreetMapService = openStreetMapService;
        }

        public async Task<List<OfferDto>> GetAll()
        {
            var result = await _context.Offers
                .Select(o => new OfferDto
                {
                    OfferId = o.OfferId,
                    Date = o.Date,
                    OfferStatus = o.OfferStatus,
                    Price = o.Price,
                    Decription = o.Description,
                    RentalPeriod = o.RentalPeriod,
                    NumberOfInterested = o.NumberOfInterested,
                    Regulations = o.Regulations,
                    Property = new PropertyDto
                    {
                        PropertyId = o.Property.PropertyId,
                        PropertyType = o.Property.GetType() == typeof(Room) ? Helpers.Enums.PropertyType.Room : (o.Property.GetType() == typeof(House) ? Helpers.Enums.PropertyType.House : Helpers.Enums.PropertyType.Flat),
                        Province = o.Property.Province,
                        District = o.Property.District,
                        Street = o.Property.Street,
                        Number = o.Property.Number,
                        Flat = o.Property.Flat,
                        City = o.Property.City,
                        PostalCode = o.Property.PostalCode,
                        Area = o.Property.Area,
                        MaxNumberOfInhabitants = o.Property.MaxNumberOfInhabitants,
                        ConstructionYear = o.Property.ConstructionYear,
                        ImagesPath = o.Property.ImagesPath,
                        Elevator = o.Property.Elevator,
                        VerificationStatus = o.Property.VerificationStatus
                    },
                    Owner = new OwnerStudentDto
                    {
                        UserId = o.Property.Owner.UserId,
                        Name = o.Property.Owner.Name,
                        Surname = o.Property.Owner.Surname,
                        Email = o.Property.Owner.Email,
                        PhoneNumber = o.Property.Owner.PhoneNumber,
                        PhotoPath = o.Property.Owner.PhotoPath,
                        ActivityStatus = o.Property.Owner.ActivityStatus
                    },
                    SurveyOwnerOffer = o.SurveyOwnerOffer
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<OfferDto>> GetFilteredAndSortedOffers(GetFilteredAndSortedOffersDto input)
        {
            var allowedSorts =  new List<string>{ "Price ASC", "Price DSC", "NumberOfRooms ASC", "NumberOfRooms DSC", "Area ASC", "Area DSC" };

            var geoInfo = await _openStreetMapService.GetCoordinatesAsync(input.Province, input.District, null, null, input.City, null);

            var query = _context.Offers.AsQueryable();

            if (!input.Distance.HasValue)
            {
                query = query.Where(o => o.Property.Province.Equals(input.Province) &&
                    o.Property.City.Equals(input.City));

                if (!string.IsNullOrEmpty(input.District))
                {
                    query = query.Where(o => o.Property.District.Equals(input.District));
                }
            }

            if (input.MinPrice.HasValue)
            {
                query = query.Where(o => o.Price >= input.MinPrice);
            }

            if (input.MaxPrice.HasValue)
            {
                query = query.Where(o => o.Price <= input.MaxPrice);
            }

            if (!string.IsNullOrEmpty(input.District) && !input.Distance.HasValue)
            {
                query = query.Where(o => o.Property.District.Equals(input.District));
            }

            if (input.MinArea.HasValue)
            {
                query = query.Where(o => o.Property.Area >= input.MinArea);
            }

            if (input.MaxArea.HasValue)
            {
                query = query.Where(o => o.Property.Area <= input.MaxArea);
            }

            if (input.MinYear.HasValue)
            {
                query = query.Where(o => o.Property.ConstructionYear >= input.MinYear);
            }

            if (input.MaxYear.HasValue)
            {
                query = query.Where(o => o.Property.ConstructionYear <= input.MinYear);
            }

            if (input.MinNumberOfRooms.HasValue)
            {
                query = query.Where(o => (o.Property.GetType() == typeof(House) ? ((House)o.Property).NumberOfRooms : o.Property.GetType() == typeof(Flat) ? ((Flat)o.Property).NumberOfRooms : 1) >= input.MinNumberOfRooms);
            }

            if (input.Floor.HasValue)
            {
                query = query.Where(o => o.Property.GetType() == typeof(Flat) || o.Property.GetType() == typeof(Room));
                query = query.Where(o => (o.Property.GetType() == typeof(Flat) ? ((Flat)o.Property).Floor : o.Property.GetType() == typeof(Room) ? ((Room)o.Property).Flat : null) == input.Floor);
            }

            var result = await query
                .Select(o => new OfferDto
                {
                    OfferId = o.OfferId,
                    Date = o.Date,
                    OfferStatus = o.OfferStatus,
                    Price = o.Price,
                    Decription = o.Description,
                    RentalPeriod = o.RentalPeriod,
                    NumberOfInterested = o.NumberOfInterested,
                    Regulations = o.Regulations,
                    Property = new PropertyDto
                    {
                        PropertyId = o.Property.PropertyId,
                        PropertyType = o.Property.GetType() == typeof(Room) ? Helpers.Enums.PropertyType.Room : (o.Property.GetType() == typeof(House) ? Helpers.Enums.PropertyType.House : Helpers.Enums.PropertyType.Flat),
                        Province = o.Property.Province,
                        District = o.Property.District,
                        Street = o.Property.Street,
                        Number = o.Property.Number,
                        Flat = o.Property.Flat,
                        City = o.Property.City,
                        PostalCode = o.Property.PostalCode,
                        GeoLat = o.Property.GeoLat,
                        GeoLon = o.Property.GeoLon,
                        Area = o.Property.Area,
                        MaxNumberOfInhabitants = o.Property.MaxNumberOfInhabitants,
                        ConstructionYear = o.Property.ConstructionYear,
                        ImagesPath = o.Property.ImagesPath,
                        Elevator = o.Property.Elevator,
                        VerificationStatus = o.Property.VerificationStatus,
                        NumberOfRooms = o.Property.GetType() == typeof(House) ? ((House)o.Property).NumberOfRooms : o.Property.GetType() == typeof(Flat) ? ((Flat)o.Property).NumberOfRooms : null,
                        NumberOfFloors = o.Property.GetType() == typeof(House) ? ((House)o.Property).NumberOfFloors : null,
                        PlotArea = o.Property.GetType() == typeof(House) ? ((House)o.Property).PlotArea : null,
                        Floor = o.Property.GetType() == typeof(Flat) ? ((Flat)o.Property).Floor : o.Property.GetType() == typeof(Room) ? ((Room)o.Property).Floor : null,
                        Equipment = o.Property.Equipment.Select(e => new EquipmentDto
                        {
                            EquipmentId = e.EquipmentId,
                            Name = e.Name
                        }).ToList()
                    },
                    Owner = new OwnerStudentDto
                    {
                        UserId = o.Property.Owner.UserId,
                        Name = o.Property.Owner.Name,
                        Surname = o.Property.Owner.Surname,
                        Email = o.Property.Owner.Email,
                        PhoneNumber = o.Property.Owner.PhoneNumber,
                        PhotoPath = o.Property.Owner.PhotoPath,
                        ActivityStatus = o.Property.Owner.ActivityStatus
                    },
                    SurveyOwnerOffer = o.SurveyOwnerOffer
                })
                .ToListAsync();

            if (!input.PropertyTypes.IsNullOrEmpty())
            {
                result = result.Where(o => input.PropertyTypes.Contains(o.Property.PropertyType)).ToList();
            }

            if (input.Distance.HasValue)
            {
                result = result
                    .Where(o => _openStreetMapService.CalculateDistance(geoInfo.Latitude, geoInfo.Longitude, o.Property.GeoLat, o.Property.GeoLon) <= input.Distance)
                    .ToList();
            }

            if (input.Equipment != null && input.Equipment.Any())
            {
                result = result.Where(o => input.Equipment
                    .All(ie => o.Property.Equipment
                    .Any(e => e.EquipmentId == ie.EquipmentId)))
                    .ToList();
            }

            if (allowedSorts.Contains(input.Sorting))
            {
                switch (input.Sorting)
                {
                    case "Price ASC":
                        result = result.OrderBy(o => o.Price).ToList();
                        break;
                    case "Price DSC":
                        result = result.OrderByDescending(o => o.Price).ToList();
                        break;
                    case "Area ASC":
                        result = result.OrderBy(o => o.Property.Area).ToList();
                        break;
                    case "Area DSC":
                        result = result.OrderByDescending(o => o.Property.Area).ToList();
                        break;
                    case "NumberOfRooms ASC":
                        result = result.OrderBy(o => o.Property.NumberOfRooms).ToList();
                        break;
                    case "NumberOfRooms DSC":
                        result = result.OrderByDescending(o => o.Property.NumberOfRooms).ToList();
                        break;
                    default:
                        result = result.OrderBy(o => o.OfferId).ToList();
                        break;
                }
            }

            result = result.Skip((input.PageNumber - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToList();

            return result;
        }
    }
}
