using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(string title, string body, string keyTitle, string keyBody, int userId, bool isChat);
        Task<List<NotificationDto>> GetAllAlertAsync(int userId);
        Task<List<NotificationDto>> GetUnreadAlertAsync(int userId);
        Task<bool> ReadAlertAsync(List<int> alertIds, int userId);

        Task<List<Connection>> GetConnectionsByUserIdAsync(int userId);
    }
}
