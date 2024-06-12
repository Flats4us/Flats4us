using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(string title, string body, int userId, bool chatNotification = false);
        Task<List<AlertDto>> GetAllAlertAsync(int userId);
        Task<List<AlertDto>> GetUnreadAlertAsync(int userId);
        Task<bool> ReadAlertAsync (List<int> alertIds, int userId);
    }

}
