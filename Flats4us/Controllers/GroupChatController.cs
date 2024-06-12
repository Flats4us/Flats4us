using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Web.Http.ModelBinding;

namespace Flats4us.Controllers
{
    [ApiController]
    [Route("api/group-chats")]
    public class GroupChatController : ControllerBase
    {
        private readonly IGroupChatService _groupChatService;

        public GroupChatController(IGroupChatService groupChatService)
        {
            _groupChatService = groupChatService;
        }

        // GET: api/group-chats/{groupChatId}
        [HttpGet("{groupChatId}")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Returns group chat info by id",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> GetGroupChatInfo(int groupChatId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var groupChat = await _groupChatService.GetGroupChatInfoAsync(requestUserId, groupChatId);
                if (groupChat == null)
                {
                    return NotFound();
                }
                return Ok(groupChat);
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // GET: api/group-chats/{groupChatId}/history
        [HttpGet("{groupChatId}/history")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Returns group chat history by id",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> GetGroupChatHistory(int groupChatId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var messages = await _groupChatService.GetGroupChatHistoryAsync(requestUserId, groupChatId);
                return Ok(messages);
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/group-chats/user
        [HttpGet("user")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Returns group chats for current user",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> GetUserGroupChats()
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var groupChats = await _groupChatService.GetGroupChats(requestUserId);
                return Ok(groupChats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/group-chats/{groupChatId}/moderator
        [HttpPost("{groupChatId:int}/moderator")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Adds current moderator to group chat",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> AddModeratorToGroupChat(int groupChatId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _groupChatService.AddModeratorToGroupChatAsync(groupChatId, requestUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
