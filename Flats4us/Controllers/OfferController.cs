using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                var offers = await _offerService.GetAllAsync();
                return Ok(offers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // GET: api/Offer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var offer = await _offerService.GetByIdAsync(id);
                return Ok(offer);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured: {ex.Message}");
            }
            

        }

        // GET: api/Offer
        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredAndSorted([FromQuery] GetFilteredAndSortedOffersDto input)
        {
            try
            {
                var offers = await _offerService.GetFilteredAndSortedOffersAsync(input);
                return Ok(offers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // POST: api/Property
        [HttpPost]
        public async Task<IActionResult> AddProperty([FromForm] AddEditOfferDto input)
        {
            try
            {
                await _offerService.AddOfferAsync(input);
                _logger.LogInformation($"Adding offer - body: {input}");
                return Ok("Property added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding offer - body: {input}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // POST: api/Equipment
        [HttpPost("Promotion")]
        [Authorize(Policy = "VerifiedOwner")]
        public async Task<IActionResult> AddOfferPromotion([FromForm] AddOfferPromotionDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _offerService.AddOfferPromotionAsync(input, requestUserId);
                _logger.LogInformation($"Adding offer promotion for offer ID: {input.OfferId}");
                return Ok("Offer promotion added successfully");
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Interest")]
        [Authorize(Policy = "VerifiedStudent")]
        public async Task<IActionResult> AddOfferInterest(int offerId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _offerService.AddOfferInterest(offerId, requestUserId);
                _logger.LogInformation($"Adding offer interest for offer ID: {offerId}");
                return Ok("Interest addded");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("Interest")]
        public async Task<IActionResult> RemoveOfferInterest(int offerId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _offerService.RemoveOfferInterest(offerId, requestUserId);
                _logger.LogInformation($"Removing offer interest for offer ID: {offerId}");
                return Ok("Interest removed");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
