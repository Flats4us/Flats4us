using Flats4us.Entities;
using Flats4us.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ILogger<SurveyController> _logger;
        private readonly ISurveyService _surveyService;

        public SurveyController(ILogger<SurveyController> logger,
                                ISurveyService surveyStudentService)
        {
            _logger = logger;
            _surveyService = surveyStudentService;   
        }

        // GET: /api/Survey/GetSurveyStudent?lang=XX
        [HttpGet]
        [Route("GetSurveyStudent")]
        public async Task<IActionResult> GetSurveyStudents()
        {
            _logger.LogInformation("Getting SurveyStudent");
            string acceptLanguage = Request.Headers["Accept-Language"].ToString();
            var surveyStudent = await _surveyService.MakingSurvey(typeof(SurveyStudent), "STUDENT", acceptLanguage);
            
            return Ok(surveyStudent);
        }

        // GET: /api/Survey/GetSurveyOwnerOffer?lang=XX
        [HttpGet]
        [Route("GetSurveyOwnerOffer")]
        public async Task<IActionResult> GetSurveyOwnerOffer()
        {
            _logger.LogInformation("Getting SurveyOwnerOffer");
            string acceptLanguage = Request.Headers["Accept-Language"].ToString();
            var surveyOwnerOffer = await _surveyService.MakingSurvey(typeof(SurveyOwnerOffer), "OWNER", acceptLanguage);
          
            return Ok(surveyOwnerOffer);
        }
    }
}
