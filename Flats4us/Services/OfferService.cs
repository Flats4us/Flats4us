using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Flats4us.Helpers.Enums;
using System.Linq.Dynamic.Core;
using Flats4us.Helpers.Exceptions;
using AutoMapper;
using System;

namespace Flats4us.Services
{
    public class OfferService : IOfferService
    {
        public readonly Flats4usContext _context;
        private readonly IOpenStreetMapService _openStreetMapService;
        private readonly IMapper _mapper;

        public OfferService(Flats4usContext context,
            IOpenStreetMapService openStreetMapService,
            IMapper mapper)
        {
            _context = context;
            _openStreetMapService = openStreetMapService;
            _mapper = mapper;
        }

        public async Task<OfferDto> GetByIdAsync(int id, int requestUserId)
        {
            var offer = await _context.Offers
                .Include(o => o.Property)
                    .ThenInclude(p => p.Owner)
                .Include(o => o.Property)
                    .ThenInclude(p => p.Equipment)
                .Include(o => o.SurveyOwnerOffer)
                .Include(o => o.Rent)
                    .ThenInclude(r => r.OtherStudents)
                .Include(o => o.Rent)
                    .ThenInclude(r => r.Student)
                .Include(o => o.OfferPromotions)
                .FirstOrDefaultAsync(o => o.OfferId == id);

            if (offer is null) throw new ArgumentException($"Offer with ID {id} not found.");

            var result = _mapper.Map<OfferDto>(offer);

            if (requestUserId != 0)
            {
                result.IsInterest = await _context.OfferInterests.AnyAsync(oi => oi.StudentId == requestUserId && oi.OfferId == offer.OfferId);
            }

            if (result.Owner.UserId == requestUserId &&
                 result.OfferStatus == OfferStatus.Waiting)
            {
                result.RentPropositionToShow = offer.Rent.RentId;
            }

            if (offer.Rent != null &&
                (result.OfferStatus == OfferStatus.Rented || result.OfferStatus == OfferStatus.Old) &&
                (result.Owner.UserId == requestUserId || offer.Rent.StudentId == requestUserId || offer.Rent.OtherStudents.Any(os => os.UserId == requestUserId)))
            {
                result.RentId = offer.Rent.RentId;
            }

            return result;
        }

        public async Task<CountedListDto<OfferDto>> GetFilteredAndSortedOffersAsync(GetFilteredAndSortedOffersDto input, int requestUserId)
        {
            var allowedSorts = new List<string>{ "Price ASC", "Price DSC", "NumberOfRooms ASC", "NumberOfRooms DSC", "Area ASC", "Area DSC" };

            var currentDate = DateTime.Now;

            var random = new Random();

            var promotedQuery = _context.Offers.AsQueryable();

            promotedQuery = promotedQuery.Where(o => o.OfferStatus == OfferStatus.Current &&
                    o.OfferPromotions.Any(op => op.StartDate <= currentDate && currentDate <= op.EndDate));

            if (!string.IsNullOrEmpty(input.Province))
            {
                promotedQuery = promotedQuery.Where(o => o.Property.Province.Equals(input.Province));
            }

            if (!string.IsNullOrEmpty(input.City))
            {
                promotedQuery = promotedQuery.Where(o => o.Property.City.Equals(input.City));
            }

            var promotedOffers = await promotedQuery
                .Include(o => o.Property)
                    .ThenInclude(p => p.Owner)
                .Include(o => o.Property)
                    .ThenInclude(p => p.Equipment)
                .Include(o => o.SurveyOwnerOffer)
                .Include(o => o.OfferPromotions)
                .ToListAsync();

            var promotedOffersCount = promotedOffers.Count();

            var randomPromotedOffers = promotedOffers
                .OrderBy(o => random.Next())
                .Take(3)
                .Select(o => _mapper.Map<OfferDto>(o))
                .ToList();

            var randomPromotedOffersCount = randomPromotedOffers.Count();

            var query = _context.Offers.AsQueryable();

            query = query.Where(o => o.OfferStatus == OfferStatus.Current &&
                (!o.OfferPromotions.Any(op => op.StartDate <= currentDate && currentDate <= op.EndDate)));

            if (!input.Distance.HasValue)
            {
                if (!string.IsNullOrEmpty(input.Province))
                {
                    query = query.Where(o => o.Property.Province.Equals(input.Province));
                }

                if (!string.IsNullOrEmpty(input.City))
                {
                    query = query.Where(o => o.Property.City.Equals(input.City));
                }

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
                query = query.Where(o => o.Property.ConstructionYear <= input.MaxYear);
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

            var notPromotedOffers = await query
                .Include(o => o.Property)
                    .ThenInclude(p => p.Owner)
                .Include(o => o.Property)
                    .ThenInclude(p => p.Equipment)
                .Include(o => o.SurveyOwnerOffer)
                .Include(o => o.OfferPromotions)
                .Select(o => _mapper.Map<OfferDto>(o))
                .ToListAsync();

            if (!input.PropertyTypes.IsNullOrEmpty())
            {
                notPromotedOffers = notPromotedOffers.Where(o => input.PropertyTypes.Contains(o.Property.PropertyType)).ToList();
            }

            if (input.Distance.HasValue && !string.IsNullOrEmpty(input.Province) && !string.IsNullOrEmpty(input.City))
            {
                var geoInfo = await _openStreetMapService.GetCoordinatesAsync(input.Province, input.District, null, null, input.City, null);

                notPromotedOffers = notPromotedOffers
                    .Where(o => _openStreetMapService.CalculateDistance(geoInfo.Lat, geoInfo.Lon, o.Property.GeoLat, o.Property.GeoLon) <= input.Distance)
                    .ToList();
            }

            if (input.Equipment != null)
            {
                var equipmentList = await _context.Equipment
                    .Where(e => input.Equipment
                        .Contains(e.EquipmentId))
                    .ToListAsync();

                notPromotedOffers = notPromotedOffers.Where(o => equipmentList
                    .All(ie => o.Property.Equipment
                    .Any(e => e.EquipmentId == ie.EquipmentId)))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(input.Sorting))
            {
                if (allowedSorts.Contains(input.Sorting))
                {
                    switch (input.Sorting)
                    {
                        case "Price ASC":
                            notPromotedOffers = notPromotedOffers.OrderBy(o => o.Price).ToList();
                            break;
                        case "Price DSC":
                            notPromotedOffers = notPromotedOffers.OrderByDescending(o => o.Price).ToList();
                            break;
                        case "Area ASC":
                            notPromotedOffers = notPromotedOffers.OrderBy(o => o.Property.Area).ToList();
                            break;
                        case "Area DSC":
                            notPromotedOffers = notPromotedOffers.OrderByDescending(o => o.Property.Area).ToList();
                            break;
                        case "NumberOfRooms ASC":
                            notPromotedOffers = notPromotedOffers.OrderBy(o => o.Property.NumberOfRooms).ToList();
                            break;
                        case "NumberOfRooms DSC":
                            notPromotedOffers = notPromotedOffers.OrderByDescending(o => o.Property.NumberOfRooms).ToList();
                            break;
                        default:
                            notPromotedOffers = notPromotedOffers.OrderBy(o => o.OfferId).ToList();
                            break;
                    }
                }
            }

            var notPromotedOffersCount = notPromotedOffers.Count();
            var totalCount = promotedOffersCount + notPromotedOffersCount;

            notPromotedOffers = notPromotedOffers.Skip((input.PageNumber - 1) * (input.PageSize-randomPromotedOffersCount))
                .Take(input.PageSize-randomPromotedOffersCount)
                .ToList();

            var joinedOffers = new List<OfferDto>(notPromotedOffers);
            foreach (var promoted in randomPromotedOffers)
            {
                int insertIndex = random.Next(0, joinedOffers.Count);
                joinedOffers.Insert(insertIndex, promoted);
            }

            if (requestUserId != 0)
            {
                var userOfferInterestIds = await _context.OfferInterests
                .Where(oi => oi.StudentId == requestUserId)
                .Select(oi => oi.OfferId)
                .ToListAsync();

                foreach (var offerDto in joinedOffers)
                {
                    offerDto.IsInterest = userOfferInterestIds.Contains(offerDto.OfferId);
                }
            }

            var result = new CountedListDto<OfferDto>(joinedOffers, totalCount);

            return result;
        }

        public async Task<CountedListDto<SimpleOfferForMapDto>> GetFilteredOffersForMapAsync(GetFilteredOffersDto input)
        {
            var currentDate = DateTime.Now;

            var promotedQuery = _context.Offers.AsQueryable();

            promotedQuery = promotedQuery.Where(o => o.OfferStatus == OfferStatus.Current &&
                    o.OfferPromotions.Any(op => op.StartDate <= currentDate && currentDate <= op.EndDate));

            if (!string.IsNullOrEmpty(input.Province))
            {
                promotedQuery = promotedQuery.Where(o => o.Property.Province.Equals(input.Province));
            }

            if (!string.IsNullOrEmpty(input.City))
            {
                promotedQuery = promotedQuery.Where(o => o.Property.City.Equals(input.City));
            }

            var promotedOffers = await promotedQuery
                .Include(o => o.Property)
                    .ThenInclude(p => p.Owner)
                .Include(o => o.Property)
                    .ThenInclude(p => p.Equipment)
                .Include(o => o.SurveyOwnerOffer)
                .Include(o => o.OfferPromotions)
                .Select(o => _mapper.Map<SimpleOfferForMapDto>(o))
                .ToListAsync();

            var query = _context.Offers.AsQueryable();

            query = query.Where(o => o.OfferStatus == OfferStatus.Current &&
                (!o.OfferPromotions.Any(op => op.StartDate <= currentDate && currentDate <= op.EndDate)));

            if (!input.Distance.HasValue)
            {
                if (!string.IsNullOrEmpty(input.Province))
                {
                    query = query.Where(o => o.Property.Province.Equals(input.Province));
                }

                if (!string.IsNullOrEmpty(input.City))
                {
                    query = query.Where(o => o.Property.City.Equals(input.City));
                }

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
                query = query.Where(o => o.Property.ConstructionYear <= input.MaxYear);
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

            var notPromotedOffers = await query
                .Include(o => o.Property)
                    .ThenInclude(p => p.Owner)
                .Include(o => o.Property)
                    .ThenInclude(p => p.Equipment)
                .Include(o => o.SurveyOwnerOffer)
                .Include(o => o.OfferPromotions)
                .Select(o => _mapper.Map<SimpleOfferForMapDto>(o))
                .ToListAsync();

            if (!input.PropertyTypes.IsNullOrEmpty())
            {
                notPromotedOffers = notPromotedOffers.Where(o => input.PropertyTypes.Contains(o.Property.PropertyType)).ToList();
            }

            if (input.Distance.HasValue && !string.IsNullOrEmpty(input.Province) && !string.IsNullOrEmpty(input.City))
            {
                var geoInfo = await _openStreetMapService.GetCoordinatesAsync(input.Province, input.District, null, null, input.City, null);

                notPromotedOffers = notPromotedOffers
                    .Where(o => _openStreetMapService.CalculateDistance(geoInfo.Lat, geoInfo.Lon, o.Property.GeoLat, o.Property.GeoLon) <= input.Distance)
                    .ToList();
            }

            if (input.Equipment != null)
            {
                var equipmentList = await _context.Equipment
                    .Where(e => input.Equipment
                        .Contains(e.EquipmentId))
                    .ToListAsync();

                notPromotedOffers = notPromotedOffers.Where(o => equipmentList
                    .All(ie => o.Property.Equipment
                    .Any(e => e.EquipmentId == ie.EquipmentId)))
                    .ToList();
            }

            var joinedOffers = new List<SimpleOfferForMapDto>(promotedOffers);
            joinedOffers.AddRange(notPromotedOffers);

            var result = new CountedListDto<SimpleOfferForMapDto>(joinedOffers);

            return result;
        }

        public async Task<CountedListDto<OfferDto>> GetOffersForCurrentUserAsync(int ownerId)
        {
            var offers = await _context.Offers
                .Include(o => o.Property)
                    .ThenInclude(p => p.Owner)
                .Include(o => o.Property)
                    .ThenInclude(p => p.Equipment)
                .Include(o => o.SurveyOwnerOffer)
                .Include(o => o.Rent)
                .Include(o => o.OfferPromotions)
                .Where(o => o.Property.OwnerId == ownerId)
                .ToListAsync();

            var results = offers.Select(_mapper.Map<OfferDto>).ToList();

            foreach (var result in results)
            {
                if (result.OfferStatus == OfferStatus.Waiting)
                {
                    result.RentPropositionToShow = offers.Find(o => o.OfferId == result.OfferId).Rent.RentId;
                }
            }

            return new CountedListDto<OfferDto>(results);
        }

        public async Task AddOfferAsync(AddEditOfferDto input, int ownerId)
        {
            var property = await _context.Properties.FindAsync(input.PropertyId);

            if (property is null) throw new ArgumentException($"Property with ID {input.PropertyId} not found.");

            if (property.OwnerId != ownerId) throw new ForbiddenException($"You do not own this property");

            if (property.VerificationStatus == VerificationStatus.NotVerified) throw new ArgumentException($"Property with ID {input.PropertyId} is not verified.");

            var offer = new Offer
            {
                Date = DateTime.Now,
                OfferStatus = OfferStatus.Current,
                Price = input.Price,
                Deposit = input.Deposit,
                Description = input.Description,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                NumberOfInterested = 0,
                Regulations = input.Regulations,
                Property = property,
                SurveyOwnerOffer = new SurveyOwnerOffer
                {
                    SmokingAllowed = input.SmokingAllowed,
                    PartiesAllowed = input.PartiesAllowed,
                    AnimalsAllowed = input.AnimalsAllowed,
                    Gender = input.Gender
                }
            };

            await _context.Offers.AddAsync(offer);
            await _context.SaveChangesAsync();
        }

        public async Task CancelOfferAsync(int id, int requestUserId)
        {
            var offer = await _context.Offers
                .Include(o => o.Property)
                .FirstOrDefaultAsync(o => o.OfferId == id);

            if (offer is null) throw new ArgumentException($"Offer with ID {id} not found.");

            if (offer.Property.OwnerId != requestUserId) throw new ForbiddenException($"You do not own this offer");

            if (offer.OfferStatus != OfferStatus.Current) throw new ArgumentException($"Cannot cancel this offer due to offerStatus");

            offer.OfferStatus = OfferStatus.Old;

            await _context.SaveChangesAsync();
        }

        public async Task AddOfferPromotionAsync(int duration, int offerId, int userId)
        {
            var offer = await _context.Offers
                .Include(o => o.Property)
                .FirstOrDefaultAsync(o => o.OfferId == offerId);

            if (offer is null) throw new ArgumentException($"Offer with ID {offerId} not found.");

            if (offer.Property.OwnerId != userId) throw new ForbiddenException($"You do not own this offer");

            var offerPromotion = new OfferPromotion
            {
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(duration),
                Price = duration * OfferPromotion.PricePerDay,
            };

            offer.OfferPromotions.Add(offerPromotion);
            await _context.SaveChangesAsync();
        }

        public async Task<CountedListDto<OfferDto>> GetOffersByInterestAsync(PaginatorDto input, int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);

            if (student is null) throw new ArgumentException($"Student with ID {studentId} not found.");

            var offers = await _context.Students
                .Where(s => s.UserId == student.UserId)
                .SelectMany(s => s.OfferInterests)
                .Include(oi => oi.Offer)
                    .ThenInclude(o => o.Property)
                        .ThenInclude(p => p.Owner)
                .Include(oi => oi.Offer)
                    .ThenInclude(o => o.Property)
                        .ThenInclude(p => p.Equipment)
                .Include(oi => oi.Offer)
                    .ThenInclude(o => o.SurveyOwnerOffer)
                .Select(oi => oi.Offer)
                .Where(o => o.OfferStatus == OfferStatus.Current)
                .Select(offer => _mapper.Map<OfferDto>(offer))
                .ToListAsync();

            var totalCount = offers.Count();

            offers = offers.Skip((input.PageNumber - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToList();

            foreach (var offer in offers)
            {
                offer.IsInterest = true;
            }

            var result = new CountedListDto<OfferDto>(offers, totalCount);

            return result;
        }

        public async Task AddOfferInterestAsync(int offerId, int studentId)
        {
            var offer = await _context.Offers.FindAsync(offerId);

            if (offer is null) throw new ArgumentException($"Offer with ID {offerId} not found.");

            var student = await _context.Students.FindAsync(studentId);

            if (student is null) throw new ArgumentException($"Student with ID {studentId} not found.");

            var existingInterest = await _context.OfferInterests
                .FirstOrDefaultAsync(oi => oi.Offer.OfferId == offerId && oi.Student.UserId == studentId);

            if (existingInterest is not null) throw new ArgumentException($"Student with ID {studentId} is already following the offer with ID {offerId}.");

            offer.NumberOfInterested++;

            var offerInterest = new OfferInterest
            {
                Date = DateTime.Now,
                StudentId = student.UserId,
                OfferId = offer.OfferId
            };

            await _context.OfferInterests.AddAsync(offerInterest);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveOfferInterestAsync(int offerId, int studentId)
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
            offer.NumberOfInterested--;

            await _context.SaveChangesAsync();
        }
    }
}
