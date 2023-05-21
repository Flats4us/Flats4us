using Flats4us.Entities;

namespace Flats4us.Services
{
    public interface ITestService
    {
        Task<IList<Student>> GetAllStudentsAsync();
        Task<SurveyStudent> GetSurveyOfStudentById(int id);
    }
}
