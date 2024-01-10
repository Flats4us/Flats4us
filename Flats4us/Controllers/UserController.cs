using Flats4us.Entities.Dto;
using Flats4us.Services;
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
    }
}
