using Flats4us.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Flats4us.Services.Interfaces;
using Flats4us.Helpers.Exceptions;
using Microsoft.AspNetCore.Cors;
using Swashbuckle.AspNetCore.Annotations;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IUserService userService, 
            ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // POST: api/auth/register/students
        [HttpPost("register/students")]
        [SwaggerOperation(
            Summary = "Registers new student, returns token"
        )]
        public async Task<ActionResult> RegisterStudentAsync([FromBody] StudentRegisterDto request)
        {
            try
            {
                var token = await _userService.RegisterStudentAsync(request);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/auth/register/owners
        [HttpPost("register/owners")]
        [SwaggerOperation(
            Summary = "Registers new owner, returns token"
        )]
        public async Task<ActionResult> RegisterOwnerAsync([FromBody] OwnerRegisterDto request)
        {
            try
            {
                var token = await _userService.RegisterOwnerAsync(request);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/auth/login
        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Logs the user in, returns a token"
        )]
        public async Task<ActionResult> Login([FromBody] UserLoginDto request)
        {
            try
            {
                var token = await _userService.AuthenticateAsync(request.Email, request.Password);

                return Ok(token);    
            }
            catch(AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // PUT: api/auth/change-password
        [HttpPut("change-password")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Changes user password",
            Description = "Requires registered user privileges"
        )]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto request)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _userService.ChangePasswordAsync(request.OldPassword, request.NewPassword, requestUserId);

                _logger.LogInformation($"Changing password for user ID: {requestUserId}");
                return Ok("Password changed");
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
