using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IGroupChatService
    {
        Task<GroupChat> CreateGroupChatAsync(string groupName, IEnumerable<int> userIds);
        Task AddUserToGroupChatAsync(int groupChatId, int userId);
        Task SendMessageToGroupChatAsync(int groupChatId, int userId, string message);
        Task SetGroupNameAsync(int groupChatId, string newName);
        Task<GroupChatDto> GetGroupChatAsync(int groupChatId);
        Task<IEnumerable<ChatMessageDto>> GetGroupChatMessagesAsync(int groupChatId);
        // Additional methods as needed (e.g., remove users, send message, etc.)
    }

}
