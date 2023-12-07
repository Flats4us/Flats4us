using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        public async Task<IActionResult> CreateGroupChat([FromBody] CreateGroupChatDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

        [HttpPost("{groupChatId:int}/users/{userId:int}")]
        public async Task<IActionResult> AddUserToGroupChat(int groupChatId, int userId)
        {
            try
            {
                await _groupChatService.AddUserToGroupChatAsync(groupChatId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., group chat or user not found)
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{groupChatId:int}")]
        public async Task<IActionResult> GetGroupChat(int groupChatId)
        {
            var groupChat = await _groupChatService.GetGroupChatAsync(groupChatId);
            if (groupChat == null)
            {
                return NotFound();
            }
            return Ok(groupChat);
        }
        [HttpPost("{groupChatId:int}/messages")]
        public async Task<IActionResult> SendMessageToGroupChat(int groupChatId, [FromBody] SendMessageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _groupChatService.SendMessageToGroupChatAsync(groupChatId, dto.UserId, dto.Message);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{groupChatId:int}/name")]
        public async Task<IActionResult> SetGroupName(int groupChatId, [FromBody] SetGroupNameDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _groupChatService.SetGroupNameAsync(groupChatId, dto.NewName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{groupChatId:int}/messages")]
        public async Task<IActionResult> GetGroupChatMessages(int groupChatId)
        {
            try
            {
                var messageDtos = await _groupChatService.GetGroupChatMessagesAsync(groupChatId);
                return Ok(messageDtos);
            }
            catch (Exception ex)
            {
                // Handle exceptions and add appropriate error responses
                return BadRequest(ex.Message);
            }
        }
    }

    

}
