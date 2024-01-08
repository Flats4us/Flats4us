using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IRentService
    {
        Task ProposeRentAsync(ProposeRentDto input, int studentId, int offerId);
    }
}
