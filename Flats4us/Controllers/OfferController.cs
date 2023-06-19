using Flats4us.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly ILogger<OfferController> _logger;

        public OfferController(IOfferService offerService,
                                ILogger<OfferController> logger)
        {
            _offerService = offerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Getting offers list");
            var offers = await _offerService.GetOffersAsync();

            return Ok(offers);
        }

        [HttpGet]
        [Route("Filtered")]
        public async Task<IActionResult> Get(int maxPerPage, int skip, int minPrice, int maxPrice, int minArea, int maxArea)
        {
            _logger.LogInformation("Getting filtered offers list");
            var offers = await _offerService.GetOffersFilteredAsync(maxPerPage, skip, minPrice, maxPrice, minArea, maxArea);

            return Ok(offers);
        }

    }
}
