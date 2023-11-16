using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IOfferPromotionService
    {
        Task AddOfferPromotionAsync(AddOfferPromotionDto input);
    }
}
