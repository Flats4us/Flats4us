using Flats4us.Entities.Dto;
using Flats4us.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyOwnerOfferController : ControllerBase
    {
        private readonly ILogger<SurveyOwnerOfferController> _logger;
        private readonly ISurveyOwnerOfferService _surveyOwnerOfferService;

        public SurveyOwnerOfferController(
            ILogger<SurveyOwnerOfferController> logger,
            ISurveyOwnerOfferService surveyOwnerOfferService
        )
        {
            _logger = logger;
            _surveyOwnerOfferService = surveyOwnerOfferService;
        }

        [HttpGet]
        //[Route("GetSurveyOwnerOffer")]
        public async Task<IActionResult> GetSurveyOwnerOffer()
        {
            _logger.LogInformation("Getting SurveyOwnerOffer");
            var surveyOwnerOffer = await _surveyOwnerOfferService.GetAllAsync();

            return Ok(surveyOwnerOffer);
        }

        [HttpGet("{id}")]
        //[Route("GetSurveyStudent")]
        public async Task<IActionResult> GetSurveyOwnerOfferById(int id)
        {
            _logger.LogInformation("Getting SurveyOwnerOffer by ID");
            var surveyOwnerOffer = await _surveyOwnerOfferService.GetById(id);

            return Ok(surveyOwnerOffer);
        }


        [HttpPost]
        //[Route("GetSurveyStudent")]
        public async Task<IActionResult> PostSurveyOwnerOffer(SurveyOwnerOfferDto input)
        {

            try
            {
                _logger.LogInformation("Getting SurveyOwnerOffer");
                await _surveyOwnerOfferService.AddSurveyOwnerOfferAsync(input);
                return Ok("dodano ankiete OwnerOffer");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding offar - body: {input}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

    }
}
