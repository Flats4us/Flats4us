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

        [HttpPost]





    }
}
