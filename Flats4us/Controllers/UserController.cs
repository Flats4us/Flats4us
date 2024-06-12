using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public UserController(
            IUserService userService,
            ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("files")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Adds files for current user",
            Description = "Requires registered user privileges"
        )]
        public async Task<ActionResult> AddUserFiles([FromForm] UserFilesDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _userService.AddUserFilesAsync(input, requestUserId);
                return Ok(new OutputDto<string>("Files added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("files/{fileId}")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Deletes the file with the given id from the current user",
            Description = "Requires registered user privileges"
        )]
        public async Task<ActionResult> DeleteUserFile(string fileId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _userService.DeleteUserFileAsync(fileId, requestUserId);
                return Ok(new OutputDto<string>("Deleted user file successfully"));
            }
            catch (IOException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("my-profile")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Returns current user profiile",
            Description = "Requires registered user privileges"
        )]
        public async Task<ActionResult> GetCurrentUserProfile()
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var profile = await _userService.GetCurrentUserProfileAsync(requestUserId);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/profile")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Returns user profiile by id",
            Description = "Requires registered user privileges"
        )]
        public async Task<ActionResult> GetCurrentUserProfile(int id)
        {
            try
            {
                var profile = await _userService.GetUserProfileByIdAsync(id);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{email}")]
        [SwaggerOperation(
            Summary = "Checks if user exists by email"
        )]
        public async Task<ActionResult> CheckIfUserExistsById(string email)
        {
            try
            {
                var result = await _userService.CheckIfUserExistsByIdAsync(email);
                return Ok(new OutputDto<bool>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{targetUserId}/opinion")]
        [Authorize(Policy = "VerifiedOwnerOrStudent")]
        [SwaggerOperation(
            Summary = "Adds opinion to user",
            Description = "Requires verified owner or verified student privileges"
        )]
        public async Task<ActionResult> AddUserOpinion(AddUserOpinionDto input, int targetUserId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _userService.AddUserOpinionAsync(input, targetUserId, requestUserId);
                return Ok(new OutputDto<string>("Opinion added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("consent")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Change email and push consents of user"
        )]
        public async Task<ActionResult> UpdateConsent([FromForm] ConsentDto input)
        {

            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _userService.UpdateConsentAsync(requestUserId, input);
                return Ok(new OutputDto<string>("Consent updated!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("consent")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Get user notifications consent info",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> GetUserConsent()
        {

            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var consent = await _userService.GetUserConsentAsync(requestUserId);
                return Ok(consent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("current")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Edit user information",
            Description = "Allows editing general, sensitive, owner, and student information. Requires registered user privileges"
        )]
        public async Task<ActionResult> EditUserInfo([FromBody] EditUserDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
                {
                    return BadRequest("Server error: Failed to get user id from token");
                }

                await _userService.EditUser(input, userId);
                return Ok(new OutputDto<string>("User information updated successfully."));
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                return BadRequest(ex.Message);
            }
        }
    }
}