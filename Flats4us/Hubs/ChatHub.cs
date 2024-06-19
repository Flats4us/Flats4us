using Flats4us.Entities;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Text;

namespace Flats4us.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IGroupChatService _groupChatService;
        public readonly Flats4usContext _context;
        private readonly INotificationService _notificationService;

        public ChatHub(IChatService chatService,
            IGroupChatService groupChatService,
            Flats4usContext context,
            INotificationService notificationService)
        {
            _chatService = chatService;
            _groupChatService = groupChatService;
            _context = context;
            _notificationService = notificationService;
        }

        public async Task SendPrivateMessage(int receiverUserId, string message)
        {
            var senderUserId = GetUserId();
            if (!senderUserId.HasValue) return;

            await _chatService.SendMessageAsync(senderUserId.Value, receiverUserId, message);

            var receiverConnections = await _context.Connections
                .Where(c => c.UserId == receiverUserId && c.HubName == nameof(ChatHub))
                .Select(c => c.ContextConnectionId)
                .ToListAsync();

            if (receiverConnections.Any())
            {
                foreach (var connectionId in receiverConnections)
                {
                    await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", senderUserId, message, DateTime.UtcNow);
                }
            }
            else
            {
                var sender = await _context.Users.FindAsync(senderUserId.Value);
                if (sender == null) return;

                var notificationTitle = sender.Name + " " + sender.Surname;
                var notificationBody = message;

                var emailBody = new StringBuilder();
                emailBody.AppendLine(EmailHelper.HtmlHTag($"Użytkownik {sender.Name} {sender.Surname} wysłał ci prywatną wiadomość", 1))
                    .AppendLine(EmailHelper.HtmlPTag(message));

                await _notificationService.SendNotificationAsync(EmailTitles.NewMessage, emailBody.ToString(), notificationTitle, notificationBody, receiverUserId, true);
            }
        }

        public async Task SendGroupChatMessage(int groupChatId, string message)
        {
            var senderId = GetUserId();
            if (!senderId.HasValue) return;

            var isMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == senderId.Value && ugc.GroupChatId == groupChatId);
            if (!isMember) return;

            var sender = await _context.Users.FindAsync(senderId.Value);
            if (sender is null) return;

            var groupUsers = await _context.UserGroupChats
                .Where(ugc => ugc.GroupChatId == groupChatId &&
                    ugc.UserId != senderId.Value)
                .Select(ugc => ugc.UserId)
                .ToListAsync();

            await _groupChatService.SendMessageToGroupChatAsync(groupChatId, senderId.Value, message);

            foreach (var groupUserId in groupUsers)
            {
                var receiverConnections = await _context.Connections
                    .Where(c => c.UserId == groupUserId && c.HubName == nameof(ChatHub))
                    .Select(c => c.ContextConnectionId)
                    .ToListAsync();

                if (receiverConnections.Any())
                {
                    foreach (var connectionId in receiverConnections)
                    {
                        await Clients.Client(connectionId).SendAsync("ReceiveGroupMessage", groupChatId, senderId.Value, message, DateTime.UtcNow);
                    }
                }
                else
                {
                    var notificationTitle = "G: " + sender.Name + " " + sender.Surname;
                    var notificationBody = message;

                    var emailBody = new StringBuilder();
                    emailBody.AppendLine(EmailHelper.HtmlHTag($"Użytkownik {sender.Name} {sender.Surname} wysłał wiadomość do twojego czatu grupowego", 1))
                        .AppendLine(EmailHelper.HtmlPTag(message));

                    await _notificationService.SendNotificationAsync(EmailTitles.NewMessage, emailBody.ToString(), notificationTitle, notificationBody, groupUserId, true);
                }
            }
        }

        private int? GetUserId()
        {
            if (int.TryParse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            { 
                return userId;
            }
            return null;
        }

        public override async Task OnConnectedAsync()
        {
            await AddConnectionAsync();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await RemoveConnectionAsync();
            await base.OnDisconnectedAsync(exception);
        }

        private async Task AddConnectionAsync()
        {
            var userId = GetUserId();
            if (userId != null)
            {
                var connection = new Connection
                {
                    UserId = userId.Value,
                    ContextConnectionId = Context.ConnectionId,
                    HubName = nameof(ChatHub)
                };

                _context.Connections.Add(connection);
                await _context.SaveChangesAsync();
            }
        }

        private async Task RemoveConnectionAsync()
        {
            var userId = GetUserId();
            if (userId != null)
            {
                var connection = await _context.Connections
                    .FirstOrDefaultAsync(c => c.ContextConnectionId == Context.ConnectionId && c.UserId == userId.Value && c.HubName == nameof(ChatHub));

                if (connection != null)
                {
                    _context.Connections.Remove(connection);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}