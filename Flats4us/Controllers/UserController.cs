using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPut("general")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Edit general user information",
            Description = "Requires registered user privileges"
        )]
        public async Task<ActionResult> EditUserGeneralInfo([FromBody] EditUserGeneral input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
                {
                    return BadRequest("Server error: Failed to get user id from token");
                }

                await _userService.EditUserGeneral(input, userId);
                return Ok("User general information updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("sensitive")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Edit sensitive user information",
            Description = "Requires registered user privileges"
        )]
        public async Task<ActionResult> EditUserSensitiveInfo([FromBody] EditUserSensitive input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
                {
                    return BadRequest("Server error: Failed to get user id from token");
                }

                await _userService.EditUserSensitive(input, userId);
                return Ok("User sensitive information updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("owner/sensitive")]
        [Authorize(Policy = "Owner")]
        [SwaggerOperation(
            Summary = "Edit sensitive owner information",
            Description = "Requires owner privileges"
        )]
        public async Task<ActionResult> EditOwnerSensitiveInfo([FromBody] EditOwnerSensitiveDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
                {
                    return BadRequest("Server error: Failed to get user id from token");
                }

                await _userService.EditOwnerSensitive(input, userId);
                return Ok("Owner sensitive information updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("student/sensitive")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Edit sensitive student information",
            Description = "Requires registered user privileges"
        )]
        public async Task<ActionResult> EditStudentSensitiveInfo([FromBody] EditStudentSensitiveDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
                {
                    return BadRequest("Server error: Failed to get user id from token");
                }

                await _userService.EditStudentSensitive(input, userId);
                return Ok("Student sensitive information updated successfully.");
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
                return Ok("User information updated successfully.");
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                return BadRequest(ex.Message);
            }
        }
    }
}
