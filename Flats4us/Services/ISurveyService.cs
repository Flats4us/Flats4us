using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services
{
    public interface ISurveyService
    {
        Task<IList<Survey>> GetAllSurveysAsync();
        Task<Survey?> GetSurveyByIdAsync(int id);
        Task<int> AddSurveyAsync(SurveyDto survey);
        Task<Survey?> DeleteSurveyAsync(int id);
        Task<Survey?> UpdateSurveyAsync(int id, SurveyDto tenant);
    }
}
