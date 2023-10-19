using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Flats4us.Services
{
    public class OfferService : IOfferService
    {
        public readonly Flats4usContext _context;

        public OfferService(Flats4usContext context)
        {
            _context = context;
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
                    Decription = o.Decription,
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
            var allowedSorts =  new List<string>{ "Price ASC", "Price DSC", "Year ASC", "Year DSC" };

            var query = _context.Offers.AsQueryable();

            query = query.Where(o => o.Property.Province.Equals(input.Province) &&
                o.Property.City.Equals(input.City));

            // DISTANCE

            if (input.MinPrice.HasValue)
            {
                query = query.Where(o => o.Price >= input.MinPrice);
            }

            if (input.MaxPrice.HasValue)
            {
                query = query.Where(o => o.Price <= input.MaxPrice);
            }

            if (!string.IsNullOrEmpty(input.District))
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
                //query = query.Where(o => o.Property.ConstructionYear >= input.MinNumberOfRooms);
            }

            if (input.MaxNumberOfFloors.HasValue)
            {
                //query = query.Where(o => o.Property <= input.MaxNumberOfFloors);
            }

            if (allowedSorts.Contains(input.Sorting)) 
            {
                    // Poprawić (Area nie działa)
                query = query.OrderBy(input.Sorting);
            }

            var result = await query
                .Select(o => new OfferDto
                {
                    OfferId = o.OfferId,
                    Date = o.Date,
                    OfferStatus = o.OfferStatus,
                    Price = o.Price,
                    Decription = o.Decription,
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
                .Skip((input.PageNumber - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync();

            return result;
        }
    }
}
