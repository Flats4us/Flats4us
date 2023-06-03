namespace Flats4us.Services
{
    public interface ISurveyStudentService
    {
        public Task<string> MakingSurvey(Type type);

    }
}
