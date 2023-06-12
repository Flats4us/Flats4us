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

        public SurveyController( ILogger<SurveyController> logger,
                                        ISurveyService surveyStudentService
                                        )
        {
            _logger = logger;
            _surveyService = surveyStudentService;   
        }

      

        // GET: ankieta_surveyStudent 
        [HttpGet]
        [Route("GetSurveyStudent")]
        public async Task<IActionResult> GetSurveyStudents(string lang)
        {
            _logger.LogInformation("Getting SurveyStudent");
            var surveyStudent = await _surveyService.MakingSurvey(typeof(SurveyStudent), "STUDENT", lang);
            

            return Ok(surveyStudent);
        }

        // GET: ankieta_surveyOwnerOffer
        [HttpGet]
        [Route("GetSurveyOwnerOffer")]
        public async Task<IActionResult> GetSurveyOwnerOffer(string lang)
        {
            _logger.LogInformation("Getting SurveyOwnerOffer");
            var surveyOwnerOffer = await _surveyService.MakingSurvey(typeof(SurveyOwnerOffer), "OWNER", lang);
          

            return Ok(surveyOwnerOffer);
        }





    }
}
