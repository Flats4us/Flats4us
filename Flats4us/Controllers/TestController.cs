using Flats4us.Entities;
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
    }
}
