using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OfferPromotionController : ControllerBase
    {
        private readonly IOfferPromotionService _offerPromotionService;
        private readonly ILogger<OfferPromotionController> _logger;

        public OfferPromotionController(
            IOfferPromotionService offerPromotionService,
            ILogger<OfferPromotionController> logger)
        {
            _offerPromotionService = offerPromotionService;
            _logger = logger;
        }

        // POST: api/Equipment
        [HttpPost]
        public async Task<IActionResult> AddOfferPromotion([FromForm] AddOfferPromotionDto input)
        {
            try
            {
                await _offerPromotionService.AddOfferPromotionAsync(input);
                _logger.LogInformation($"Adding offer promotion for offer ID: {input.OfferId}");
                return Ok("Offer promotion added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
