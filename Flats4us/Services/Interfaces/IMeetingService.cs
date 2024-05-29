﻿using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IMeetingService
    {
        Task<List<MeetingDto>> GetMeetingsForCurrentUserAsync(int userId);
        Task AddMeetingAsync(AddMeetingDto input, int userId);
        Task ConfirmMeetingAsync(bool decision, int userId, int meetingId);
    }
}
