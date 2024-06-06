using Flats4us.Entities;
using Flats4us.Hubs;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;
using Flats4us.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/chats")]
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

        // POST: api/chats/send-message
        [Authorize]
        [HttpPost("send-message")]
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
                SenderId = senderUserId,
                //Chat = chat
            };

            await _chatService.SaveMessage(chatMessage);

            // Optionally, use the chatHub to notify the receiver in real-time
            // await _chatHub.Clients.User(receiverUserId.ToString()).SendAsync("ReceiveMessage", chatMessage);

            return Ok();
        }

        // GET: api/chats/{chatId}/history
        [HttpGet("{chatId}/history")]
        public async Task<IActionResult> GetChatHistory(int chatId)
        {
            var history = await _chatService.GetChatHistory(chatId);
            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        // GET: api/chats/{chatId}/participants
        [Authorize]
        [HttpGet("{chatId}/participants")]
        [SwaggerOperation(
            Summary = "Returns list of chats for current user"
        )]
        public async Task<IActionResult> GetParticipantId(int chatId)
        {
            var senderUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(senderUserIdClaim) || !int.TryParse(senderUserIdClaim, out var senderUserId))
            {
                return BadRequest("Sender user ID is not available.");
            }

            var participantId = await _chatService.GetChatParticipant(chatId, senderUserId);
            if (participantId == null)
            {
                return NotFound();
            }

            return Ok(participantId);
        }

        // GET: api/chats/user
        [Authorize]
        [HttpGet("user")]
        [SwaggerOperation(
            Summary = "Returns list of chats for current user"
        )]
        public async Task<IActionResult> GetUserChats()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var verifiedUserId))
                {
                    return BadRequest("Sender user ID is not available.");
                }

                var chats = await _chatService.GetUserChatsAsync(verifiedUserId);
                return Ok(chats);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return BadRequest(ex.Message);
            }

            // Helper method to get the current user's ID

        }
    }

}
