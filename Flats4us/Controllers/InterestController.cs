using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/interests")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private readonly IInterestService _interestService;
        private readonly ILogger<InterestController> _logger;

        public InterestController(
            IInterestService interestService,
            ILogger<InterestController> logger)
        {
            _interestService = interestService;
            _logger = logger;
        }

        // GET: api/interests
        [HttpGet]
        [SwaggerOperation(
            Summary = "Returns list of interests"
        )]
        public async Task<IActionResult> GetAll([FromQuery] string? name = null)
        {
            try
            {
                var interests = await _interestService.GetAll(name);
                return Ok(interests);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}