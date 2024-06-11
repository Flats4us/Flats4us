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
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Exceptions;

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

                await _chatService.SendMessageAsync(requestUserId, input.UserId, input.Message);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        // GET: api/chats/{chatId}/history
        [HttpGet("{chatId}/history")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Returns chat history by id",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> GetChatHistory(int chatId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var history = await _chatService.GetChatHistoryAsync(chatId, requestUserId);

                if (history == null) return NotFound();

                return Ok(history);
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }

        }

        // GET: api/chats/{chatId}/participant
        [HttpGet("{chatId}/participant")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Returns chat otherParticipantId by id",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> GetParticipantId(int chatId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var participantId = await _chatService.GetChatParticipantAsync(chatId, requestUserId);

                if (participantId == null) return NotFound();

                return Ok(new OutputDto<int?>(participantId));
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        // GET: api/chats/user
        [HttpGet("user")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Returns chats info for current user",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> GetUserChats()
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var chats = await _chatService.GetUserChatsAsync(requestUserId);

                return Ok(chats);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }
    }

}
