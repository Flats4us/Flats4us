using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserService userService,
            ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        // GET: api/Property/NotVerified
        [HttpGet("NotVerified")]
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

        [HttpPost("Verify/{id}")]
        public async Task<IActionResult> VerifyUser(int id)
        {
            try
            {
                await _userService.VerifyUserAsync(id);
                _logger.LogInformation($"Verifying user - id: {id}");
                return Ok("User verified successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Verifying user - id: {id}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
