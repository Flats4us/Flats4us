using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

            if (offer.Status != OfferStatus.Current) throw new ArgumentException($"Offer with ID {offerId} is not currently available.");

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
                NextPaymentDate = input.StartDate.AddDays(10),
                EndDate = input.StartDate.AddMonths(input.Duration),
                Student = student,
                OtherStudents = verifiedRoommates
            };

            offer.Rent = newRent;
            offer.Status = OfferStatus.Waiting;

            await _context.SaveChangesAsync();
        }
    }
}
