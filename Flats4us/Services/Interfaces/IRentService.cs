using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IRentService
    {
        Task ProposeRentAsync(ProposeRentDto input, int studentId, int offerId);
        Task AcceptRentAsync(bool decision, int requestUserId, int offerId);
        Task AddRentOpinionAsync(RentOpinionDto input, int UserId, int RentId);
    }
}