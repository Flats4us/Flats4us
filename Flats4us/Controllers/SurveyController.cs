using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
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

        [HttpGet("student")]
        //[Route("GetSurveyStudent")]
        public async Task<IActionResult> GetSurveyStudents()
        {
            _logger.LogInformation("Getting SurveyStudent");
            var surveyStudent = await _surveyService.GetAllSurveyStudentsAsync();

            return Ok(surveyStudent);
        }


        [HttpGet("{id}")]
        //[Route("GetSurveyStudent")]
        public async Task<IActionResult> GetSurveyStudentById(int id)
        {
            _logger.LogInformation("Getting SurveyStudent by ID");
            var surveyStudent = await _surveyService.GetSurveyStudentById(id);

            return Ok(surveyStudent);
        }

        [HttpPost]
        //[Route("GetSurveyStudent")]
        public async Task<IActionResult> PostSurveyStudent(SurveyStudentPost input)
        {

            try
            {
                _logger.LogInformation("Getting SurveyStudent");
                await _surveyService.AddSurveyStudentAsync(input);
                return Ok("dodano ankiete");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding offar - body: {input}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }








        [HttpGet("surveyStudent")]
        public async Task<IActionResult> GetSurveyStudents(string lang)
        {
            _logger.LogInformation("Getting SurveyStudent");
            var surveyStudent = await _surveyService.MakingSurvey(typeof(SurveyStudent), "STUDENT", lang);
            
            return Ok(surveyStudent);
        }

        
        [HttpGet("SurveyOwnerOffer")]
        public async Task<IActionResult> GetSurveyOwnerOffer(string lang)
        {
            _logger.LogInformation("Getting SurveyOwnerOffer");
            var surveyOwnerOffer = await _surveyService.MakingSurvey(typeof(SurveyOwnerOffer), "OWNER", lang);
          
            return Ok(surveyOwnerOffer);
        }
    }
}
