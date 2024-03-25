namespace Flats4us.Services.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(string title, string body, int userId);
    }

}
