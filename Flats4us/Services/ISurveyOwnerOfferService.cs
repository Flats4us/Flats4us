using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services
{
    public interface ISurveyOwnerOfferService
    {
        public Task<List<SurveyOwnerOffer>> GetAllAsync();
        public Task<SurveyOwnerOffer> GetById(int id);
        public Task AddSurveyOwnerOfferAsync(SurveyOwnerOfferDto input);
    }
}
