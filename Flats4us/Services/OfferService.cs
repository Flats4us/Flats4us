﻿using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Flats4us.Helpers.Enums;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using Flats4us.Helpers.Exceptions;
using AutoMapper;

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

        public async Task<List<OfferDto>> GetAllAsync()
        {
            var result = await _context.Offers
                .Include(o => o.Property)
                    .ThenInclude(p => p.Owner)
                .Include(o => o.Property)
                    .ThenInclude(p => p.Equipment)
                .Include(o => o.SurveyOwnerOffer)
                .Select(o => _mapper.Map<OfferDto>(o))
                .ToListAsync();

            return result;
        }

        public async Task<OfferDto> GetByIdAsync(int id)
        {
            var offer = await _context.Offers
                .Include(o => o.Property)
                    .ThenInclude(p => p.Owner)
                .Include(o => o.Property)
                    .ThenInclude(p => p.Equipment)
                .Include(o => o.SurveyOwnerOffer)
                .FirstOrDefaultAsync(o => o.OfferId == id);

            if (offer is null)
            {
                throw new ArgumentException($"Offer with ID {id} not found.");
            }

            var result = _mapper.Map<OfferDto>(offer);

            return result;
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
                .Include(o => o.Property)
                    .ThenInclude(p => p.Owner)
                .Include(o => o.Property)
                    .ThenInclude(p => p.Equipment)
                .Include(o => o.SurveyOwnerOffer)
                .Select(o => _mapper.Map<OfferDto>(o))
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

            if (property is null) throw new ArgumentException($"Property with ID {input.PropertyId} not found.");

            if (property.VerificationStatus == VerificationStatus.NotVerified) throw new ArgumentException($"Property with ID {input.PropertyId} is not verified.");

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
