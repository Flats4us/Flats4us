using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IChatService
    {
        Task<List<ChatMessageDto>> GetChatHistoryAsync(int chatId, int requestUserId);
        Task<int?> GetChatParticipantAsync(int chatId, int senderUserId);
        Task SendMessageAsync(int senderId, int receiverId, string message);
        Task<List<ChatInfoDto>> GetUserChatsAsync(int userId);
    }
}
