using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IMeetingService
    {
        Task AddMeetingAsync(AddMeetingDto input, int userId);
    }
}
