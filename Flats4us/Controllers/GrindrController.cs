using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrindrController : ControllerBase
    {
        private readonly IGrindrService _grindrtService;
        private readonly ILogger<GrindrController> _logger;

        public GrindrController(
            IGrindrService grindrService,
            ILogger<GrindrController> logger)
        {
            _grindrtService = grindrService;
            _logger = logger;
        }


        [HttpGet("get_match")]
        public async Task<IActionResult> GetMatches(int studentId)
        {
            try
            {
                _logger.LogInformation("Getting Matches");
                return Ok(await _grindrtService.GetPotentialRoommate(studentId));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding argument - body: {studentId}");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }

        [HttpPost("Post_match")]
        public async Task<IActionResult> PostMatche(int student1Id, int student2Id, bool isAccept)
        {
            try
            {
                _logger.LogInformation("Posting Argument");
                await _grindrtService.AcceptStudent(student1Id, student2Id, isAccept);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding argument - body: {student1Id}");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }



    }
}
