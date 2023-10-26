namespace Flats4us.Services.Interfaces
{
    public interface ISurveyService
    {
        public Task<string> MakingSurvey(Type type, string title, string lang);
    }
}
