using Flats4us.Entities;
using Flats4us.Hubs;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Flats4us.Services;
using Swashbuckle.AspNetCore.Annotations;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _chatHub;
        public readonly Flats4usContext _context;

        public ChatController(
            IChatService chatService,
            IHubContext<ChatHub> chatHub,
            Flats4usContext context,
            ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _chatHub = chatHub;
            _context = context;
            _logger = logger;
        }

        // POST: api/chats/send-message
        [HttpPost("send-message")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Sends message to user",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                _logger.LogInformation("SendingMessage");
                var arguments = await _chatService.
                return Ok(arguments);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Sendiong message");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");

            }

            var chat = await _chatService.EnsureChatSession(senderUserId, input.UserId);
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
