using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Microsoft.EntityFrameworkCore;
using Flats4us.Services.Interfaces;
using AutoMapper;
using Flats4us.Helpers.Enums;
using Flats4us.Helpers.Exceptions;
using System;

namespace Flats4us.Services
{
    public class MeetingService : IMeetingService
    {
        public readonly Flats4usContext _context;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public MeetingService(Flats4usContext context,
            INotificationService notificationService,
            IMapper mapper)
        {
            _context = context;
            _notificationService = notificationService;
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

                foreach (var meeting in meetings)
                {
                    meeting.NeedsAction = meeting.StudentAcceptDate == null ? true : false;
                }
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

                foreach(var meeting in meetings)
                {
                    meeting.NeedsAction = meeting.OwnerAcceptDate == null ? true : false;
                }
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

                    await _notificationService.SendNotificationAsync("New meeting proposition",
                        $"You have a new meeting proposition for the day {input.Date} from {user.Name} {user.Surname}. Check it and answer in calendar",
                        offer.Property.OwnerId);

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

                    await _notificationService.SendNotificationAsync("New meeting proposition",
                        $"You have a new meeting proposition for the day {input.Date} from {user.Name} {user.Surname}. Check it and answer in calendar",
                        offer.Rent.StudentId);

                    break;
                default:
                    throw new ArgumentException($"Wrong user type");
            }

            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();
        }

        public async Task ConfirmMeetingAsync(bool decision, int userId, int meetingId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user is null) throw new ArgumentException($"User with ID {userId} not found.");

            var meeting = await _context.Meetings
                .Include(m => m.Offer)
                    .ThenInclude(o => o.Rent)
                .Include(m => m.Offer)
                    .ThenInclude(o => o.Property)
                .FirstOrDefaultAsync(o => o.MeetingId == meetingId);

            if (meeting is null) throw new ArgumentException($"Meeting with ID {meetingId} not found.");

            switch (user)
            {
                case Student:
                    if (meeting.StudentId != userId) throw new ForbiddenException("You dont have access to this entity");

                    if (meeting.StudentAcceptDate != null) throw new ArgumentException("Already confirmed or denied");

                    if (decision)
                    {
                        meeting.StudentAcceptDate = DateTime.Now;

                        await _notificationService.SendNotificationAsync("Meeting accepted",
                            $"Your offer for meeting with {user.Name} {user.Surname} has been accepted ",
                            meeting.Offer.Property.OwnerId);
                    }
                    else
                    {
                        _context.Meetings.Remove(meeting);

                        await _notificationService.SendNotificationAsync("Meeting denied",
                            $"Your offer for meeting with {user.Name} {user.Surname} has been denied ",
                            meeting.Offer.Property.OwnerId);
                    }

                    await _context.SaveChangesAsync();

                    break;
                case Owner:
                    if (meeting.Offer.Property.OwnerId != userId) throw new ForbiddenException("You dont have access to this entity");

                    if (meeting.OwnerAcceptDate != null) throw new ArgumentException("Already confirmed or denied");

                    if (decision)
                    {
                        meeting.OwnerAcceptDate = DateTime.Now;

                        await _notificationService.SendNotificationAsync("Meeting accepted",
                            $"Your offer for meeting with {user.Name} {user.Surname} has been accepted ",
                            meeting.StudentId);
                    }
                    else
                    {
                        _context.Meetings.Remove(meeting);

                        await _notificationService.SendNotificationAsync("Meeting denied",
                            $"Your offer for meeting with {user.Name} {user.Surname} has been denied ",
                            meeting.StudentId);
                    }

                    await _context.SaveChangesAsync();

                    break;
                default:
                    throw new ArgumentException($"Wrong user type");
            }

        }
    }
}
