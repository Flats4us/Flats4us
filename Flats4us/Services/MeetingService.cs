using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Microsoft.EntityFrameworkCore;
using Flats4us.Services.Interfaces;
using AutoMapper;
using Flats4us.Helpers.Enums;
using Flats4us.Helpers.Exceptions;
using System;
using FirebaseAdmin.Messaging;
using Flats4us.Helpers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Flats4us.Services
{
    public class MeetingService : IMeetingService
    {
        public readonly Flats4usContext _context;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public MeetingService(Flats4usContext context,
            INotificationService notificationService,
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _notificationService = notificationService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<MeetingDto>> GetMeetingsForCurrentUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var meetings = new List<MeetingDto>();

            if (user is Student)
            {
                var studentMeetings = await _context.Students
                    .Where(s =>  s.UserId == userId)
                    .SelectMany(s => s.Meetings)
                        .Include(m => m.Offer)
                            .ThenInclude(o => o.Property)
                                .ThenInclude(p => p.Owner)
                                    .ThenInclude(ow => ow.ProfilePicture)
                    .Select(meeting => _mapper.Map<MeetingWithOwnerDto>(meeting))
                    .ToListAsync();

                foreach (var meeting in studentMeetings)
                {
                    meeting.NeedsAction = meeting.StudentAcceptDate == null ? true : false;
                }

                meetings.AddRange(studentMeetings);
            }
            else if (user is Owner)
            {
                var ownerMeetings = await _context.Owners
                    .Where(o => o.UserId == userId)
                    .SelectMany(o => o.Properties)
                    .SelectMany(p => p.Offers)
                    .SelectMany(o => o.Meetings)
                        .Include(m => m.Student)
                            .ThenInclude(s => s.ProfilePicture)
                    .Select(meeting => _mapper.Map<MeetingWithStudentDto>(meeting))
                    .ToListAsync();

                foreach(var meeting in ownerMeetings)
                {
                    meeting.NeedsAction = meeting.OwnerAcceptDate == null ? true : false;
                }

                meetings.AddRange(ownerMeetings);
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

            var baseUrl = _configuration.GetSection("AppBaseUrl").Value;
            var link = $"{baseUrl}/calendar";
            var emailBody = new StringBuilder();

            switch (user)
            {
                case Student:

                    if (offer.OfferStatus == OfferStatus.Old ) throw new ArgumentException($"Cannot add meeting to old offer");

                    if (offer.OfferStatus == OfferStatus.Rented &&
                        offer.Rent.StudentId != userId &&
                        !offer.Rent.OtherStudents.Any(os => os.UserId == userId)) throw new ArgumentException($"You cannot add meeting to this offer");

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
                    
                    emailBody.AppendLine(EmailHelper.HtmlHTag($"Użytkownik {user.Name} {user.Surname} wysłał propozycje spotkania {input.Date}", 1))
                        .AppendLine(EmailHelper.HtmlPTag($"Aby zaakceptować lub odrzucić naciśnij {EmailHelper.AddLinkToText(link, "TUTAJ")} lub przejdź pod poniższy link"))
                        .AppendLine(EmailHelper.HtmlPTag($"{link}"));

                    await _notificationService.SendNotificationAsync(EmailTitles.NewMeetingProposition, emailBody.ToString(), TranslateKeys.NewMeetingPropositionTitle, TranslateKeys.NewMeetingPropositionBody, offer.Property.OwnerId, false);

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

                    emailBody.AppendLine(EmailHelper.HtmlHTag($"Użytkownik {user.Name} {user.Surname} wysłał propozycje spotkania {input.Date}", 1))
                        .AppendLine(EmailHelper.HtmlPTag($"Aby zaakceptować lub odrzucić naciśnij {EmailHelper.AddLinkToText(link, "TUTAJ")} lub przejdź pod poniższy link"))
                        .AppendLine(EmailHelper.HtmlPTag($"{link}"));

                    await _notificationService.SendNotificationAsync(EmailTitles.NewMeetingProposition, emailBody.ToString(), TranslateKeys.NewMeetingPropositionTitle, TranslateKeys.NewMeetingPropositionBody, offer.Rent.StudentId, false);

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

            var baseUrl = _configuration.GetSection("AppBaseUrl").Value;
            var link = $"{baseUrl}/calendar";
            var emailBody = new StringBuilder();

            switch (user)
            {
                case Student:
                    if (meeting.StudentId != userId) throw new ForbiddenException("You dont have access to this entity");

                    if (meeting.StudentAcceptDate != null) throw new ArgumentException("Already confirmed or denied");

                    if (decision)
                    {
                        meeting.StudentAcceptDate = DateTime.Now;

                        emailBody.AppendLine(EmailHelper.HtmlHTag($"Użytkownik {user.Name} {user.Surname} zaakceptował spotkanie dnia {meeting.Date}", 1))
                            .AppendLine(EmailHelper.HtmlPTag($"Aby zobaczyć szczegóły w kalendarzu naciśnij tutaj {EmailHelper.AddLinkToText(link, "TUTAJ")} lub przejdź pod poniższy link"))
                            .AppendLine(EmailHelper.HtmlPTag($"{link}"));

                        await _notificationService.SendNotificationAsync(EmailTitles.MeetingAccepted, emailBody.ToString(), TranslateKeys.MeetingPropositionAcceptedTitle, TranslateKeys.MeetingPropositionAcceptedBody, meeting.Offer.Property.OwnerId, false);
                    }
                    else
                    {
                        _context.Meetings.Remove(meeting);

                        emailBody.AppendLine(EmailHelper.HtmlHTag($"Użytkownik {user.Name} {user.Surname} odrzucił spotkanie dnia {meeting.Date}", 1))
                            .AppendLine(EmailHelper.HtmlPTag($"Spróbuj umówić się na spotkanie jeszcze raz. Zachęcamy do skorzystania z czatu aby wcześniej ustalić szczegóły z drugą stroną."));

                        await _notificationService.SendNotificationAsync(EmailTitles.MeetingRejected, emailBody.ToString(), TranslateKeys.MeetingPropositionRejectedTitle, TranslateKeys.MeetingPropositionRejectedBody, meeting.Offer.Property.OwnerId, false);
                    }

                    await _context.SaveChangesAsync();

                    break;
                case Owner:
                    if (meeting.Offer.Property.OwnerId != userId) throw new ForbiddenException("You dont have access to this entity");

                    if (meeting.OwnerAcceptDate != null) throw new ArgumentException("Already confirmed or denied");

                    if (decision)
                    {
                        meeting.OwnerAcceptDate = DateTime.Now;

                        emailBody.AppendLine(EmailHelper.HtmlHTag($"Użytkownik {user.Name} {user.Surname} zaakceptował spotkanie dnia {meeting.Date}", 1))
                            .AppendLine(EmailHelper.HtmlPTag($"Aby zobaczyć szczegóły w kalendarzu naciśnij tutaj {EmailHelper.AddLinkToText(link, "TUTAJ")} lub przejdź pod poniższy link"))
                            .AppendLine(EmailHelper.HtmlPTag($"{link}"));

                        await _notificationService.SendNotificationAsync(EmailTitles.MeetingAccepted, emailBody.ToString(), TranslateKeys.MeetingPropositionAcceptedTitle, TranslateKeys.MeetingPropositionAcceptedBody, meeting.StudentId, false);
                    }
                    else
                    {
                        _context.Meetings.Remove(meeting);

                        emailBody.AppendLine(EmailHelper.HtmlHTag($"Użytkownik {user.Name} {user.Surname} odrzucił spotkanie dnia {meeting.Date}", 1))
                            .AppendLine(EmailHelper.HtmlPTag($"Spróbuj umówić się na spotkanie jeszcze raz. Zachęcamy do skorzystania z czatu aby wcześniej ustalić szczegóły z drugą stroną."));

                        await _notificationService.SendNotificationAsync(EmailTitles.MeetingRejected, emailBody.ToString(), TranslateKeys.MeetingPropositionRejectedTitle, TranslateKeys.MeetingPropositionRejectedBody, meeting.StudentId, false);
                    }

                    await _context.SaveChangesAsync();

                    break;
                default:
                    throw new ArgumentException($"Wrong user type");
            }
        }
    }
}
