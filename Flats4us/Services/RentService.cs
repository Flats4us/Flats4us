﻿using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Linq;

namespace Flats4us.Services
{
    public class RentService : IRentService
    {
        public readonly Flats4usContext _context;
        private readonly IMapper _mapper;

        public RentService(Flats4usContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ProposeRentAsync(ProposeRentDto input, int studentId, int offerId)
        {
            var offer = await _context.Offers.FindAsync(offerId);

            if (offer is null) throw new ArgumentException($"Offer with ID {offerId} not found.");

            if (offer.OfferStatus != OfferStatus.Current) throw new ArgumentException($"Offer with ID {offerId} is not currently available.");

            var student = await _context.Students.FindAsync(studentId);

            if (student is null) throw new ArgumentException($"Student with ID {studentId} not found.");

            var uniqueEmails = new HashSet<string>();
            foreach (var email in input.RoommatesEmails)
            {
                if (!uniqueEmails.Add(email)) throw new ArgumentException($"Duplicate email found: {email}");
            }

            var verifiedRoommates = new List<Student>();

            foreach (var roommateEmail in uniqueEmails)
            {
                var roommate = await _context.Students.FirstOrDefaultAsync(s => s.Email == roommateEmail);
                if (roommate is null) throw new ArgumentException($"Student with Email {roommateEmail} not found.");

                if (roommate.VerificationStatus != VerificationStatus.Verified) throw new ArgumentException($"Student with Email {roommateEmail} is not verified.");

                verifiedRoommates.Add(roommate);
            }

            var newRent = new Rent
            {
                StartDate = input.StartDate,
                EndDate = input.StartDate.AddMonths(input.Duration),
                Duration = input.Duration,
                Student = student,
                OtherStudents = verifiedRoommates
            };

            offer.Rent = newRent;
            offer.OfferStatus = OfferStatus.Waiting;

            await _context.SaveChangesAsync();
        }

        public async Task AcceptRentAsync(bool decision, int requestUserId, int offerId)
        {
            var offer = await _context.Offers
                .Include(o => o.Property)
                .FirstOrDefaultAsync(o => o.OfferId == offerId);

            if (offer is null) throw new ArgumentException($"Offer with ID {offerId} not found.");

            if (offer.Property.OwnerId != requestUserId) throw new ForbiddenException($"You do not own this offer");

            if (offer.OfferStatus != OfferStatus.Waiting) throw new ArgumentException("Rent proposition already accepted or rejected");

            var rent = await _context.Rents.FirstOrDefaultAsync(r => r.OfferId == offerId);

            if (rent is null) throw new ArgumentException($"Rent with offer ID: {offerId} not found.");

            if (decision)
            {
                offer.OfferStatus = OfferStatus.Rented;

                var firstPayment = new Payment
                {
                    PaymentPurpose = PaymentPurpose.Rent,
                    Amount = offer.Price,
                    IsPaid = false,
                    CreatedDate = DateTime.Now,
                    PaymentDate = rent.StartDate.AddDays(7).Date
                };

                var depositPayment = new Payment
                {
                    PaymentPurpose = PaymentPurpose.Deposit,
                    Amount = offer.Price,
                    IsPaid = false,
                    CreatedDate = DateTime.Now,
                    PaymentDate = rent.StartDate.AddDays(7).Date
                };

                rent.Payments.Add(depositPayment);
                rent.Payments.Add(firstPayment);

                if ( rent.Duration > 1 ) rent.NextPaymentGenerationDate = rent.StartDate.AddMonths(1).Date;

                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Rents.Remove(rent);

                offer.OfferStatus = OfferStatus.Current;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<CountedListDto<RentDto>> GetRentsForCurrentUserAsync(int userId, int? pageSize, int? pageNumber)
        {
            var user = await _context.Users.FindAsync(userId);
            var rentDtos = new List<RentDto>();

            if (user is Student)
            {
                var mainStudentRents = await _context.Students
                    .Where(s => s.UserId == userId)
                    .SelectMany(s => s.Rents)
                        .Include(r => r.Payments)
                        .Include(r => r.Offer)
                            .ThenInclude(o => o.Property)
                                .ThenInclude(p => p.Images)
                        .Include(r => r.Student)
                            .ThenInclude(s => s.ProfilePicture)
                        .Include(r => r.OtherStudents)
                            .ThenInclude(os => os.ProfilePicture)
                        .Include(x=>x.Arguments)
                            .ThenInclude(x=>x.ArgumentInterventions)
                    .Select(rent => _mapper.Map<RentDto>(rent))
                    .ToListAsync();

                var roommateRents = await _context.Students
                    .Where(s => s.UserId == userId)
                    .SelectMany(s => s.RoommateInRents)
                        .Include(r => r.Payments)
                        .Include(r => r.Offer)
                            .ThenInclude(o => o.Property)
                                .ThenInclude(p => p.Images)
                        .Include(r => r.Student)
                            .ThenInclude(s => s.ProfilePicture)
                        .Include(r => r.OtherStudents)
                            .ThenInclude(os => os.ProfilePicture)
                        .Include(x => x.Arguments)
                            .ThenInclude(x => x.ArgumentInterventions)
                    .Select(rent => _mapper.Map<RentDto>(rent))
                    .ToListAsync();

                rentDtos = mainStudentRents;
                rentDtos.AddRange(roommateRents);
            }
            else if (user is Owner)
            {
                rentDtos = await _context.Rents
                    .Where(r => r.Offer.Property.OwnerId == userId &&
                        r.Offer.OfferStatus != OfferStatus.Waiting)
                    .Include(r => r.Payments)
                    .Include(r => r.Offer)
                        .ThenInclude(o => o.Property)
                            .ThenInclude(p => p.Images)
                    .Include(r => r.Student)
                        .ThenInclude(s => s.ProfilePicture)
                    .Include(r => r.OtherStudents)
                        .ThenInclude(os => os.ProfilePicture)
                    .Include(x => x.Arguments)
                            .ThenInclude(x => x.ArgumentInterventions)
                    .Select(rent => _mapper.Map<RentDto>(rent))
                    .ToListAsync();
            }
            else throw new Exception("Unable to fetch rents for current user");

            var totalCount = rentDtos.Count;

            if(pageNumber != null && pageSize != null )
            {
                rentDtos = rentDtos
                    .Skip(((int)pageNumber - 1) * (int)pageSize)
                    .Take((int)pageSize)
                    .ToList();
            }
            
            var result = new CountedListDto<RentDto>(rentDtos, totalCount);

            return result;
        }

        public async Task<RentDto> GetRentByIdAsync(int id, int requestUserId)
        {
            var rent = await _context.Rents
                .Include(r => r.Payments)
                .Include(r => r.Offer)
                    .ThenInclude(o => o.Property)
                        .ThenInclude(p => p.Images)
                .Include(r => r.Student)
                    .ThenInclude(s => s.ProfilePicture)
                .Include(r => r.OtherStudents)
                    .ThenInclude(os => os.ProfilePicture)
                .Include(x => x.Arguments)
                    .ThenInclude(x => x.ArgumentInterventions)
            .FirstOrDefaultAsync(o => o.RentId == id);

            if (rent == null) throw new ArgumentException($"Rent with ID {id} not found.");

            if (rent.Offer.Property.OwnerId != requestUserId &&
                rent.StudentId != requestUserId &&
                !rent.OtherStudents.Any(os => os.UserId == requestUserId))
            {
                throw new ForbiddenException($"You do not have permission to view this rent.");
            }

            return _mapper.Map<RentDto>(rent);
        }

        public async Task<RentPropositionDto> GetRentPropositionAsync(int rentId, int requestUserId)
        {
            var rent = await _context.Rents
                .Include(r => r.Offer)
                    .ThenInclude(o => o.Property)
                .Include(r => r.Student)
                    .ThenInclude(s => s.ProfilePicture)
                .Include(r => r.OtherStudents)
                    .ThenInclude(os => os.ProfilePicture)
                .SingleOrDefaultAsync(r => r.RentId == rentId);

            if (rent is null) throw new ArgumentException($"Rent with  ID: {rentId} not found.");

            if (rent.Offer.OfferStatus != OfferStatus.Waiting) throw new ArgumentException($"Unable to get proposition for this rent");
        
            if (rent.Offer.Property.OwnerId != requestUserId) throw new ForbiddenException($"You do not own associated offer");

            return _mapper.Map<RentPropositionDto>(rent);
        }

        public async Task AddRentOpinionAsync(AddRentOpinionDto input, int userId, int rentId)
        {
            var sourceUser = await _context.Users.
                FindAsync(userId);
            
            if (sourceUser is null) throw new ArgumentException($"User with ID {userId} not found.");

            var rent = _context.Rents
                .FirstOrDefault(r => r.RentId == rentId && (r.StudentId == userId ||
                                                            r.OtherStudents.Any(s => s.UserId == userId)));
            
            if (rent is null) throw new ArgumentException($"Rent with ID {rentId} not found.");

            if (DateTime.Now < rent.EndDate)
            {
                throw new InvalidOperationException("Opinion can only be added after the end of the rental period.");
            }

            var offer = await _context.Offers.FindAsync(rent.OfferId);
            
            if (offer is null) throw new ArgumentException($"Offer associated with Rent ID {rentId} not found.");

            var property = await _context.Properties.FindAsync(offer.PropertyId);
            
            if (property is null) throw new ArgumentException($"Property associated with Offer ID {offer.OfferId} not found.");


            var opinion = new RentOpinion
            {
                Rating = input.Rating,
                Date = DateTime.Now,
                Service = input.Service,
                Location = input.Location,
                Equipment = input.Equipment,
                QualityForMoney = input.QualityForMoney,
                Description = input.Description,
                StudentId = sourceUser.UserId,
                PropertyId = property.PropertyId
            };

            await _context.RentOpinions.AddAsync(opinion);
            await _context.SaveChangesAsync();
        }
    }
}