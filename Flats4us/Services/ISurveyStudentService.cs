using Flats4us.Entities;

namespace Flats4us.Services
{
    public interface ISurveyStudentService
    {
        public Task<List<SurveyStudent>> GetAllAsync();
        public Task<SurveyStudent> GetById(int id);
        public Task<SurveyStudent> Post(SurveyStudent surveyStudent);
    }
}
