using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Flats4us.Helpers.Enums;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using Flats4us.Helpers.Exceptions;

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

        public async Task<List<OfferDto>> GetAllAsync()
        {
            var result = await _context.Offers
                .Select(o => new OfferDto
                {
                    OfferId = o.OfferId,
                    Date = o.Date,
                    OfferStatus = o.OfferStatus,
                    Price = o.Price,
                    Decription = o.Description,
                    StartDate = o.StartDate,
                    EndDate = o.EndDate,
                    NumberOfInterested = o.NumberOfInterested,
                    Regulations = o.Regulations,
                    Property = new PropertyDto
                    {
                        PropertyId = o.Property.PropertyId,
                        PropertyType = o.Property.GetType() == typeof(Room) ? PropertyType.Room : (o.Property.GetType() == typeof(House) ? PropertyType.House : PropertyType.Flat),
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
                        ImagesPath = o.Property.Owner.ImagesPath,
                        ActivityStatus = o.Property.Owner.ActivityStatus
                    },
                    SurveyOwnerOffer = new SurveyOwnerOfferDto
                    {
                        Smoking = o.SurveyOwnerOffer.Smoking,
                        Parties = o.SurveyOwnerOffer.Parties,
                        Animals = o.SurveyOwnerOffer.Animals,
                        Gender = o.SurveyOwnerOffer.Gender
                    }
                })
                .ToListAsync();

            return result;
        }

        public async Task<OfferDto> GetByIdAsync(int id)
        {
            if (await _context.Offers.FindAsync(id) is null)
            {
                throw new ArgumentException($"Offer with ID {id} not found.");
            }

            var offer = await _context.Offers
                .Where(o => o.OfferId == id)
                .Select(o => new OfferDto
                {
                    OfferId = o.OfferId,
                    Date = o.Date,
                    OfferStatus = o.OfferStatus,
                    Price = o.Price,
                    Decription = o.Description,
                    StartDate = o.StartDate,
                    EndDate = o.EndDate,
                    NumberOfInterested = o.NumberOfInterested,
                    Regulations = o.Regulations,
                    Property = new PropertyDto
                    {
                        PropertyId = o.Property.PropertyId,
                        PropertyType = o.Property.GetType() == typeof(Room) ? PropertyType.Room : (o.Property.GetType() == typeof(House) ? PropertyType.House : PropertyType.Flat),
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
                        ImagesPath = o.Property.Owner.ImagesPath,
                        ActivityStatus = o.Property.Owner.ActivityStatus
                    },
                    SurveyOwnerOffer = new SurveyOwnerOfferDto
                    {
                        Smoking = o.SurveyOwnerOffer.Smoking,
                        Parties = o.SurveyOwnerOffer.Parties,
                        Animals = o.SurveyOwnerOffer.Animals,
                        Gender = o.SurveyOwnerOffer.Gender
                    }
                })
                .FirstOrDefaultAsync();

            return offer;
        }

        public async Task<List<OfferDto>> GetFilteredAndSortedOffersAsync(GetFilteredAndSortedOffersDto input)
        {
            var allowedSorts =  new List<string>{ "Price ASC", "Price DSC", "NumberOfRooms ASC", "NumberOfRooms DSC", "Area ASC", "Area DSC" };

            var geoInfo = await _openStreetMapService.GetCoordinatesAsync(input.Province, input.District, null, null, input.City, null);

            var query = _context.Offers.AsQueryable();

            query = query.Where(o => o.OfferStatus == OfferStatus.Current);

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
                    StartDate = o.StartDate,
                    EndDate = o.EndDate,
                    NumberOfInterested = o.NumberOfInterested,
                    Regulations = o.Regulations,
                    Property = new PropertyDto
                    {
                        PropertyId = o.Property.PropertyId,
                        PropertyType = o.Property.GetType() == typeof(Room) ? PropertyType.Room : (o.Property.GetType() == typeof(House) ? PropertyType.House : PropertyType.Flat),
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
                        ImagesPath = o.Property.Owner.ImagesPath,
                        ActivityStatus = o.Property.Owner.ActivityStatus
                    },
                    SurveyOwnerOffer = new SurveyOwnerOfferDto
                    {
                        Smoking = o.SurveyOwnerOffer.Smoking,
                        Parties = o.SurveyOwnerOffer.Parties,
                        Animals = o.SurveyOwnerOffer.Animals,
                        Gender = o.SurveyOwnerOffer.Gender
                    }
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

        public async Task AddOfferAsync(AddEditOfferDto input)
        {
            var property = await _context.Properties.FindAsync(input.PropertyId);

            if(property is null) throw new ArgumentException($"Property with ID {input.PropertyId} not found.");

            var offer = new Offer
            {
                Date = DateTime.Now,
                OfferStatus = OfferStatus.Current,
                Price = input.Price,
                Description = input.Description,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                NumberOfInterested = 0,
                Regulations = input.Regulations,
                Property = property,
                SurveyOwnerOffer = new SurveyOwnerOffer
                {
                    Smoking = input.Smoking,
                    Parties = input.Parties,
                    Animals = input.Animals,
                    Gender = input.Gender
                }
            };

            await _context.Offers.AddAsync(offer);
            await _context.SaveChangesAsync();
        }

        public async Task AddOfferPromotionAsync(AddOfferPromotionDto input, int userId)
        {
            var offer = await _context.Offers
                .Include(o => o.Property)
                .FirstOrDefaultAsync(o => o.OfferId == input.OfferId);

            if (offer is null) throw new ArgumentException($"Offer with ID {input.OfferId} not found.");

            if (offer.Property.OwnerId != userId) throw new ForbiddenException($"You do not own this offer");

            var offerPromotion = new OfferPromotion
            {
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(input.Duration),
                Price = input.Duration * OfferPromotion.PricePerDay,
            };

            offer.OfferPromotions.Add(offerPromotion);
            await _context.SaveChangesAsync();
        }

        public async Task AddOfferInterest(int offerId, int studentId)
        {
            var offer = await _context.Offers.FindAsync(offerId);

            if (offer is null) throw new ArgumentException($"Offer with ID {offerId} not found.");

            var student = await _context.Students.FindAsync(studentId);

            if (student is null) throw new ArgumentException($"Student with ID {studentId} not found.");

            offer.NumberOfInterested = offer.NumberOfInterested++;

            var offerInterest = new OfferInterest
            {
                Date = DateTime.Now,
                Student = student,
                Offer = offer
            };

            await _context.OfferInterests.AddAsync(offerInterest);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveOfferInterest(int offerId, int studentId)
        {
            var offer = await _context.Offers.FindAsync(offerId);

            if (offer is null) throw new ArgumentException($"Offer with ID {offerId} not found.");

            var offerInterest = await _context.OfferInterests
                .FirstOrDefaultAsync(oi => oi.Offer.OfferId == offerId && oi.Student.UserId == studentId);

            if (offerInterest is null)
            {
                throw new ArgumentException($"Interest not found for Offer ID: {offerId} and Student ID: {studentId}");
            }

            _context.OfferInterests.Remove(offerInterest);
            offer.NumberOfInterested = offer.NumberOfInterested--;

            await _context.SaveChangesAsync();
        }
    }
}
