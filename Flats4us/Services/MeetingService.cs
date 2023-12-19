﻿using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Microsoft.EntityFrameworkCore;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services.Interfaces;
using AutoMapper;
using System;

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

        public async Task<List<MeetingDto>> GetMeetingsForCurrentUserAsync(int userId, int month, int year)
        {
            var user = await _context.Users.FindAsync(userId);
            var meetings = new List<MeetingDto>();

            if (user is Student)
            {
                meetings = await _context.Students
                    .Where(s =>  s.UserId == userId)
                    .SelectMany(s => s.Meetings)
                    .Where(m => m.Date.Month == month && m.Date.Year == year)
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
                    .Where(m => m.Date.Month == month && m.Date.Year == year)
                    .Select(meeting => _mapper.Map<MeetingDto>(meeting))
                    .ToListAsync();
            }
            else throw new Exception("Unable to fetch meetings for current user");

            return meetings;
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
