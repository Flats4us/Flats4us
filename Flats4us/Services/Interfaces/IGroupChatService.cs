using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IGroupChatService
    {
        Task<GroupChatDto> CreateGroupChatAsync(string groupName, IEnumerable<int> userIds);
        Task AddUserToGroupChatAsync(int adderId, int groupChatId, int newUserId);
        Task SendMessageToGroupChatAsync(int groupChatId, int userId, string message);
        Task SetGroupNameAsync(int userId, int groupChatId, string newName);
        Task<GroupChatDto> GetGroupChatAsync(int userId, int groupChatId);
        Task<IEnumerable<ChatMessageDto>> GetGroupChatMessagesAsync(int userId, int groupChatId);
        Task<List<GroupChatDto>> GetGroupChats(int userId);
        Task AddYourselfToGroupChatAsync(int groupChatId, int newUserId);
        // Additional methods as needed (e.g., remove users, send message, etc.)
    }

}
