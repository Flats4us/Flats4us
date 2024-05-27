using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Microsoft.EntityFrameworkCore;
using Flats4us.Services.Interfaces;
using AutoMapper;
using Flats4us.Helpers.Enums;

namespace Flats4us.Services
{
    public class MeetingService : IMeetingService
    {
        public readonly Flats4usContext _context;
        private readonly IMapper _mapper;

        public MeetingService(Flats4usContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MeetingDto>> GetMeetingsForCurrentUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var meetings = new List<MeetingDto>();

            if (user is Student)
            {
                meetings = await _context.Students
                    .Where(s =>  s.UserId == userId)
                    .SelectMany(s => s.Meetings)
                    .Select(meeting => _mapper.Map<MeetingDto>(meeting))
                    .ToListAsync();
            }
            else if (user is Owner)
            {
                meetings = await _context.Owners
                    .Where(o => o.UserId == userId)
                    .SelectMany(o => o.Properties)
                    .SelectMany(p => p.Offers)
                    .SelectMany(o => o.Meetings)
                    .Select(meeting => _mapper.Map<MeetingDto>(meeting))
                    .ToListAsync();
            }
            else throw new Exception("Unable to fetch meetings for current user");

            return meetings;
        }

        public async Task AddMeetingAsync(AddMeetingDto input, int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user is null) throw new ArgumentException($"User with ID {userId} not found.");

            var offer = await _context.Offers
                .Include(o => o.Property)
                .Include(o => o.Rent)
                    .ThenInclude(r => r.OtherStudents)
                .FirstOrDefaultAsync(o => o.OfferId == input.OfferId);

            if (offer is null) throw new ArgumentException($"Offer with ID {input.OfferId} not found.");

            Meeting meeting;

            switch (user)
            {
                case Student:

                    if (offer.OfferStatus == OfferStatus.Old ) throw new ArgumentException($"Cannot add meeting to old offer");

                    if (offer.OfferStatus == OfferStatus.Rented &&
                        (offer.Rent.StudentId != userId ||
                        !offer.Rent.OtherStudents.Any(os => os.UserId == userId))) throw new ArgumentException($"You cannot add meeting to this offer");

                    meeting = new Meeting
                    {
                        Date = input.Date,
                        Place = input.Place,
                        Reason = input.Reason,
                        StudentAcceptDate = DateTime.Now,
                        OwnerAcceptDate = null,
                        OfferId = input.OfferId,
                        StudentId = userId
                    };

                    break;
                case Owner:

                    if (offer.OfferStatus != OfferStatus.Rented) throw new ArgumentException($"You cannot add meeting to this offer");

                    if (offer.OfferStatus == OfferStatus.Rented && offer.Property.OwnerId != userId) throw new ArgumentException($"You cannot add meeting to this offer");

                    meeting = new Meeting
                    {
                        Date = input.Date,
                        Place = input.Place,
                        Reason = input.Reason,
                        StudentAcceptDate = null,
                        OwnerAcceptDate = DateTime.Now,
                        OfferId = input.OfferId,
                        StudentId = offer.Rent.StudentId
                    };

                    break;
                default:
                    throw new ArgumentException($"Wrong user type");
            }

            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();
        }

        public async Task ConfirmMeetingAsync(AcceptDto input, int userId, int offerId)
        {

        }
    }
}
