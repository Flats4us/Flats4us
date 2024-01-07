using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Flats4us.Entities;
using System.Security.Claims;

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

        [HttpGet("getMatchersByStudentId")]
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
                _logger.LogInformation($"FAILED: Getting properties for current user");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        [HttpGet("possible")]
        [Authorize(Policy = "VerifiedStudent")]
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
                _logger.LogInformation($"FAILED: Getting properties for current user");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        [HttpPost("api/matcher/accept/students/{student2Id}")]
        [Authorize(Policy = "VerifiedStudent")]
        public async Task<IActionResult> PostOrAccept([FromBody] bool isAccept)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                _logger.LogInformation("Posting Or Accepting Matcher");
                await _matcherService.AcceptStudentAsync(requestUserId, student2Id, isAccept);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Getting properties for current user");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }



    }
}
