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




    }
}
