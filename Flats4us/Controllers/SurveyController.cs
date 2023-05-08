using Flats4us.Entities.Dto;
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
        private readonly ISurveyService _surveyService;
        private readonly ILogger<SurveyController> _logger;

        public SurveyController(ISurveyService surveyService,
                                ILogger<SurveyController> logger)
        {
            _surveyService = surveyService;
            _logger = logger;
        }

        // GET: api/Survey
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Getting surveys list");
            var surveys = await _surveyService.GetAllSurveysAsync();

            return Ok(surveys);
        }

        // GET api/Survey/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Getting survey - id: {id}", id);
            var survey = await _surveyService.GetSurveyByIdAsync(id);
            if (survey is null)
                return BadRequest("Survey not found");
            else 
                return Ok(survey);
        }

        // POST api/Survey
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SurveyDto body)
        {
            _logger.LogInformation("Posting survey - body: {@body}", body);
            var result = await _surveyService.AddSurveyAsync(body);
            if (result == 1)
                return Created("", "");
            else
                return Conflict();
        }

        // PUT api/Survey/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SurveyDto body)
        {
            _logger.LogInformation("Updating survey - id: {id}, body: {@body}", id, body);
            var survey = await _surveyService.UpdateSurveyAsync(id, body);
            if (survey is null)
                return BadRequest("Survey not found");
            else
                return Ok();
        }

        // DELETE api/Survey/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting survey - id: {id}", id);
            var survey = await _surveyService.DeleteSurveyAsync(id);
            if (survey is null)
                return BadRequest("Survey not found");
            else
                return Ok();
        }


    }
}
