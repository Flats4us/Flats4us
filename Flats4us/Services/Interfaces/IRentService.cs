using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IRentService
    {
        Task ProposeRentAsync(ProposeRentDto input, int studentId, int offerId);
        Task AcceptRentAsync(bool decision, int requestUserId, int offerId);
        Task<CountedListDto<RentDto>> GetRentsForCurrentUserAsync(int userId, int? pageSize, int? pageNumber);
        Task<RentPropositionDto> GetRentPropositionAsync(int rentId, int requestUserId);
        Task AddRentOpinionAsync(RentOpinionDto input, int UserId, int RentId);
    }
}