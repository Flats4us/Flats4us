using Flats4us.Entities;
using Flats4us.Hubs;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _chatHub;
        public readonly Flats4usContext _context;


        public ChatController(IChatService chatService, IHubContext<ChatHub> chatHub, Flats4usContext context)
        {
            _chatService = chatService;
            _chatHub = chatHub;
            _context = context;
        }

        // POST: chat/sendmessage
        [Authorize]
        [HttpPost("sendmessage")]
        public async Task<IActionResult> SendMessage(int receiverUserId, string message)
        {
            var senderUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(senderUserIdClaim) || !int.TryParse(senderUserIdClaim, out var senderUserId))
            {
                return BadRequest("Sender user ID is not available.");
            }

            var senderUser = await _context.Users.FindAsync(senderUserId);



            var chat = await _chatService.EnsureChatSession(senderUserId, receiverUserId);
            if (chat == null)
            {
                return BadRequest("Unable to create or find a chat session.");
            }

            var chatMessage = new ChatMessage
            {
                Content = message,
                DateTime = DateTime.UtcNow,
                Sender = senderUser,
                Chat = chat
            };

            await _chatService.SaveMessage(chatMessage);

            // Optionally, use the chatHub to notify the receiver in real-time
            // await _chatHub.Clients.User(receiverUserId.ToString()).SendAsync("ReceiveMessage", chatMessage);

            return Ok();
        }

        // GET: chat/history/{chatId}
        [HttpGet("history/{chatId}")]
        public async Task<IActionResult> GetChatHistory(int chatId)
        {
            var history = await _chatService.GetChatHistory(chatId);
            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        // Helper method to get the current user's ID
        
    }

}
