using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
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

        public OfferController(
            IOfferService offerService,
            ILogger<OfferController> logger)
        {
            _offerService = offerService;
            _logger = logger;
        }

        // GET: api/Offer
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var offers = await _offerService.GetAll();
                return Ok(offers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // GET: api/Offer
        [HttpPost("filtered")]
        public async Task<IActionResult> GetFilteredAndSorted([FromQuery] GetFilteredAndSortedOffersDto input)
        {
            try
            {
                var offers = await _offerService.GetFilteredAndSortedOffers(input);
                return Ok(offers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
