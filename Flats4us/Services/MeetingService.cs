using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Microsoft.EntityFrameworkCore;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services.Interfaces;

namespace Flats4us.Services
{
    public class MeetingService : IMeetingService
    {
        public readonly Flats4usContext _context;

        public MeetingService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task AddMeetingAsync(AddMeetingDto input, int userId)
        {
            var offer = await _context.Offers
                .Include(o => o.Property)
                .FirstOrDefaultAsync(o => o.OfferId == input.OfferId);

            if (offer is null) throw new ArgumentException($"Offer with ID {input.OfferId} not found.");

            if (offer.Property.OwnerId != userId) throw new ForbiddenException($"You do not own this offer");

            var students = await _context.Students
                .Where(s => input.StudentIds.Contains(s.UserId))
                .ToListAsync();

            var meeting = new Meeting
            {
                Date = input.Date,
                Place = input.Place,
                Reason = input.Reason,
                OfferId = input.OfferId,
                Students = students
            };

            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();
        }
    }
}
