namespace Flats4us.Hubs
{
    using Flats4us.Entities;
    using Flats4us.Services.Interfaces;
    using Microsoft.AspNetCore.SignalR;
    using System.Collections.Concurrent;
    using System.Security.Claims;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    public class ChatHub : Hub
    {
        private readonly IUserService _userService;

        
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendPrivateMessage(int receiverUserId, string message)
        {

            if (_connections.TryGetValue(receiverUserId, out var receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).SendAsync("ReceivePrivateMessage", Context.User.Identity.Name, message);
            }
            else
            {
                // Handle the case where the user is not connected or not found
            }
        }
        private int? GetUserId()
        {
            // Assuming the user ID claim is stored as "NameIdentifier"
            if (int.TryParse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return userId;
            }
            return null;
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
            var userId = GetUserId();
            if (userId != null)
            {
                _connections[userId.Value] = Context.ConnectionId;
            }


            
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetUserId();
            if (userId != null)
            {
                _connections.TryRemove(userId.Value, out _);
            }
            // When a user disconnects, remove them from the group
           
        }
    }
}


