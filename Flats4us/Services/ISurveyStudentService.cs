using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services
{
    public interface ISurveyStudentService
    {
        public Task<List<SurveyStudent>> GetAllAsync();
        public Task<SurveyStudent> GetById(int id);
        public Task AddSurveyStudentAsync(SurveyStudentPost input);
    }
}
