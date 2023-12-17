using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IMeetingService
    {
        Task<List<MeetingDto>> GetMeetingsForCurrentUserAsync(int userId, int month, int year);
        Task AddMeetingAsync(AddMeetingDto input, int studentId);
    }
}
