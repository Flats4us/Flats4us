namespace Flats4us.Services
{
    public interface ISurveyService
    {
        public Task<string> MakingSurvey(Type type, string title, string lang);
    }
}
