using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(
            Summary = "Returns a list offers"
        )]
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
        [SwaggerOperation(
            Summary = "Returns offer by id"
        )]
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
        [SwaggerOperation(
            Summary = "Returns a filtered list of offers"
        )]
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
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Adds an offer",
            Description = "Requires verified owner privileges"
        )]
        public async Task<IActionResult> AddOffer([FromBody] AddEditOfferDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _offerService.AddOfferAsync(input, requestUserId);
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
        [HttpPost("promotion")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Adds an offer promotion",
            Description = "Requires verified owner privileges"
        )]
        public async Task<IActionResult> AddOfferPromotion([FromBody] AddOfferPromotionDto input)
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

        [HttpGet("interest")]
        [SwaggerOperation(
            Summary = "Returns a list of offers observed by the student",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> GetOffersByInterest()
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var observedOffers = await _offerService.GetOffersByInterestAsync(requestUserId);
                _logger.LogInformation($"Getting the list of offers observed by the student ID: {requestUserId}");
                return Ok(observedOffers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("interest")]
        [Authorize(Policy = "VerifiedStudent")]
        [SwaggerOperation(
            Summary = "Adds an offer interest",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> AddOfferInterest([FromBody] AddRemoveInterestDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _offerService.AddOfferInterestAsync(input.OfferId, requestUserId);
                _logger.LogInformation($"Adding offer interest for offer ID: {input.OfferId}");
                return Ok("Interest addded");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("interest")]
        [SwaggerOperation(
            Summary = "Removes an offer interest",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> RemoveOfferInterest([FromBody] AddRemoveInterestDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _offerService.RemoveOfferInterestAsync(input.OfferId, requestUserId);
                _logger.LogInformation($"Removing offer interest for offer ID: {input.OfferId}");
                return Ok("Interest removed");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
