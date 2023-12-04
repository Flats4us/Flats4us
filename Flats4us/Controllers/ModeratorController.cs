using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
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

        // GET: api/Moderator/Property
        [HttpGet("Property")]
        [Authorize(Policy = "Moderator")]
        public async Task<IActionResult> GetNotVerifiedProperties()
        {
            try
            {
                var notVerifiedProperties = await _propertyService.GetNotVerifiedPropertiesAsync();
                return Ok(notVerifiedProperties);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // POST: api/Moderator/Property/Verify/{id}/{decision}
        [HttpPost("Property/Verify/{id}/{decision}")]
        [Authorize(Policy = "Moderator")]
        public async Task<IActionResult> VerifyProperty(int id, bool decision)
        {
            try
            {
                await _propertyService.VerifyPropertyAsync(id, decision);
                _logger.LogInformation($"Verifying property - id: {id}");
                return Ok("Property verification status changed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Verifying property - id: {id}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // GET: api/Moderator/User
        [HttpGet("User")]
        [Authorize(Policy = "Moderator")]
        public async Task<IActionResult> GetNotVerifiedUsers()
        {
            try
            {
                var notVerifiedUsers = await _userService.GetNotVerifiedUsersAsync();
                return Ok(notVerifiedUsers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // POST: api/Moderator/User/Verify/{id}/{decision}
        [HttpPost("User/Verify/{id}/{decision}")]
        [Authorize(Policy = "Moderator")]
        public async Task<IActionResult> VerifyUser(int id, bool decision)
        {
            try
            {
                await _userService.VerifyUserAsync(id, decision);
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
