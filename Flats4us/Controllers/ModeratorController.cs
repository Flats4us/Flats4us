using Flats4us.Entities.Dto;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/moderator")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPropertyService _propertyService;
        private readonly ILogger<ModeratorController> _logger;

        public ModeratorController(
            IUserService userService,
            IPropertyService propertyService,
            ILogger<ModeratorController> logger)
        {
            _userService = userService;
            _propertyService = propertyService;
            _logger = logger;
        }

        // GET: api/moderator/properties
        [HttpGet("properties")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Returns a list of properties requiring verification",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> GetNotVerifiedProperties([FromQuery] PaginatorDto input)
        {
            try
            {
                var notVerifiedProperties = await _propertyService.GetNotVerifiedPropertiesAsync(input);
                return Ok(notVerifiedProperties);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // PUT: api/moderator/properties/{id}/verify
        [HttpPut("properties/{id}/verify")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Verifies property",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> VerifyProperty(int id, [FromBody] VerifyDto input)
        {
            try
            {
                await _propertyService.VerifyPropertyAsync(id, input.Decision);
                _logger.LogInformation($"Verifying property - id: {id}");
                return Ok("Property verification status changed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Verifying property - id: {id}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // GET: api/moderator/users
        [HttpGet("users")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Returns a list of users requiring verification",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> GetNotVerifiedUsers([FromQuery] PaginatorDto input)
        {
            try
            {
                var notVerifiedUsers = await _userService.GetNotVerifiedUsersAsync(input);
                return Ok(notVerifiedUsers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // PUT: api/moderator/users/{id}/verify
        [HttpPut("users/{id}/verify")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Verifies user",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> VerifyUser(int id, [FromBody] VerifyDto input)
        {
            try
            {
                await _userService.VerifyUserAsync(id, input.Decision);
                _logger.LogInformation($"Verifying user - id: {id}");
                return Ok("User verification status changed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Verifying user - id: {id}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // TODO: Przenieść tutaj endpointy moderatora od interwencji
    }
}
