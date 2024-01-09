using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Flats4us.Entities;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;

namespace Flats4us.Controllers
{
    [Route("api/matcher")]
    [ApiController]
    public class MatcherController : ControllerBase
    {
        private readonly IMatcherService _matcherService;
        private readonly ILogger<MatcherController> _logger;

        public MatcherController(
            IMatcherService matcherService,
            ILogger<MatcherController> logger)
        {
            _matcherService = matcherService;
            _logger = logger;
        }

        //[HttpGet("All")]
        //[Authorize(Policy = "VerifiedStudent")]
        //public async Task<IActionResult> GetAllMatches()
        //{
        //    try
        //    {
        //        _logger.LogInformation("Getting Matches");
        //        return Ok(await _matcherService.GetAllMatches());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation($"FAILED: Adding argument - body: ");
        //        return BadRequest($"An error occurred: {ex.Message}");
        //    }
        //}

        [HttpGet("existing-by-id")]
        [Authorize(Policy = "VerifiedStudent")]
        [SwaggerOperation(
            Summary = "Return list of matches for current user",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> ByStudentId()
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }
                return Ok(await _matcherService.GetMatchByStudentId(requestUserId));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Getting Students for Match for current user");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        [HttpGet("potential-by-id")]
        [Authorize(Policy = "VerifiedStudent")]
        [SwaggerOperation(
            Summary = "Return list of potentail matches for current user",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> Potential()
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                    {
                        return BadRequest("Server error: Failed to get user id from request");
                    }
                
                _logger.LogInformation("Getting Matches");
                return Ok(await _matcherService.GetPotentialRoommateAsync(requestUserId));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Getting potential Matches for current user");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        [HttpPost("accept/students/{studentId}")]
        [Authorize(Policy = "VerifiedStudent")]
        [SwaggerOperation(
            Summary = "Posting new match or updating existing one",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> PostOrAccept(int studentId, [FromBody] bool isAccept)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                _logger.LogInformation("Posting Or Accepting Matcher");
                await _matcherService.AcceptStudentAsync(requestUserId, studentId, isAccept);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Accepting Match or Posting new one");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }



    }
}
