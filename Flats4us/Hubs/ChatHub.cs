namespace Flats4us.Hubs
{
    using Flats4us.Entities;
    using Flats4us.Services.Interfaces;
    using Microsoft.AspNetCore.SignalR;
    using System.Collections.Concurrent;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    public class ChatHub : Hub
    {
        private readonly IUserService _userService;

        
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendPrivateMessage(int receiverId, string message)
        {
            var receiverConnectionId = GetConnectionIdByUserId(receiverId);
            if (!string.IsNullOrEmpty(receiverConnectionId))
            {
                // Use "ReceivePrivateMessage" to distinguish the method on the client-side.
                await Clients.Client(receiverConnectionId).SendAsync("ReceivePrivateMessage", Context.User.Identity.Name, message);
            } else
            {
                await Clients.All.SendAsync("ReceiveMessage", receiverId, message);
            }
        }


        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }


        public async Task SendMessageToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

        // This method sends a message to a specific user.
        

        private readonly static ConcurrentDictionary<int, string> _connections = new ConcurrentDictionary<int, string>();

        private string GetConnectionIdByUserId(int userId)
        {
            _connections.TryGetValue(userId, out string connectionId);
            return connectionId;
        }

        public override async Task OnConnectedAsync()
        {
            // When a user connects, add them to a group with their username
            var username = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, username);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // When a user disconnects, remove them from the group
            var username = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, username);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}


