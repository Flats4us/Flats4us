using Flats4us.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Flats4us.Hubs
{
    public class NotificationHub : Hub
    {
        public readonly Flats4usContext _context;

        public NotificationHub(Flats4usContext context)
        {
            _context = context;
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
                    HubName = nameof(NotificationHub)
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
                    .FirstOrDefaultAsync(c => c.ContextConnectionId == Context.ConnectionId && c.UserId == userId.Value && c.HubName == nameof(NotificationHub));

                if (connection != null)
                {
                    _context.Connections.Remove(connection);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
