using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Web.Http.ModelBinding;

namespace Flats4us.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupChatController : ControllerBase
    {
        private readonly IGroupChatService _groupChatService;

        public GroupChatController(IGroupChatService groupChatService)
        {
            _groupChatService = groupChatService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateGroupChat([FromBody] CreateGroupChatDto dto)
        {
            

            try
            {
                var groupChat = await _groupChatService.CreateGroupChatAsync(dto.GroupName, dto.UserIds);
                return CreatedAtAction(nameof(GetGroupChat), new { groupChatId = groupChat.GroupChatId }, groupChat);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., user not found)
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{groupChatId:int}/users/{newUserId:int}")]
        [Authorize]
        public async Task<IActionResult> AddUserToGroupChat(int groupChatId, int newUserId)
        {
            try
            {
                int adderId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                await _groupChatService.AddUserToGroupChatAsync(adderId, groupChatId, newUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., group chat or user not found)
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{groupChatId:int}")]
        [Authorize]
        public async Task<IActionResult> GetGroupChat(int groupChatId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var groupChat = await _groupChatService.GetGroupChatAsync(userId, groupChatId);
            if (groupChat == null)
            {
                return NotFound();
            }
            return Ok(groupChat);
        }

        [HttpPost("{groupChatId:int}/messages")]
        [Authorize]
        public async Task<IActionResult> SendMessageToGroupChat(int groupChatId, [FromBody] SendMessageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            try
            {
                await _groupChatService.SendMessageToGroupChatAsync(groupChatId, userId, dto.Message);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{groupChatId:int}/name")]
        [Authorize]
        public async Task<IActionResult> SetGroupName(int groupChatId, [FromBody] SetGroupNameDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            try
            {
                await _groupChatService.SetGroupNameAsync(userId, groupChatId, dto.NewName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{groupChatId:int}/messages")]
        public async Task<IActionResult> GetGroupChatMessages(int groupChatId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var messageDtos = await _groupChatService.GetGroupChatMessagesAsync(userId, groupChatId);
                return Ok(messageDtos);
            }
            catch (Exception ex)
            {
                // Handle exceptions and add appropriate error responses
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet("user/groupchats")]
        public async Task<IActionResult> GetUserGroupChats()
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var groupChats = await _groupChatService.GetGroupChats(userId);
                return Ok(groupChats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{groupChatId:int}/users")]
        [Authorize(Policy = "Moderator")]
        public async Task<IActionResult> AddYourselfToGroupChat(int groupChatId)
        {
            try
            {
                int adderId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                await _groupChatService.AddYourselfToGroupChatAsync(groupChatId, adderId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., group chat or user not found)
                return BadRequest(ex.Message);
            }
        }
    }

    

}
