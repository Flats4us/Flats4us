using Flats4us.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _surveyService;
        private readonly ILogger<TestController> _logger;

        public TestController(ITestService surveyService,
                                ILogger<TestController> logger)
        {
            _surveyService = surveyService;
            _logger = logger;
        }


        // GET: api/Test
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Getting students list");
            var students = await _surveyService.GetAllStudentsAsync();

            return Ok(students);
        }

        // GET api/Survey/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Getting survey - studentId: {id}", id);
            var survey = await _surveyService.GetSurveyOfStudentById(id);
            if (survey is null)
                return BadRequest("Survey not found");
            else
                return Ok(survey);
        }
    }
}
