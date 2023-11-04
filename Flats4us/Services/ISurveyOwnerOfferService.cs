using Flats4us.Entities;

namespace Flats4us.Services
{
    public interface ISurveyOwnerOfferService
    {
        public Task<List<SurveyOwnerOffer>> GetAllAsync();
        public Task<SurveyOwnerOffer> GetById(int id);
        public Task<SurveyOwnerOffer> Post(SurveyOwnerOffer surveyOwnerOffer);
    }
}
