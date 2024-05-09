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
    [Route("api/offers")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly IRentService _rentService;
        private readonly ILogger<OfferController> _logger;

        public OfferController(
            IOfferService offerService,
            IRentService rentService,
            ILogger<OfferController> logger)
        {
            _offerService = offerService;
            _rentService = rentService;
            _logger = logger;
        }

        // GET: api/offers/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Returns offer by id"
        )]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var requestUserId = 0;

                if (int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int parsedUserId))
                {
                    requestUserId = parsedUserId;
                }

                var offer = await _offerService.GetByIdAsync(id, requestUserId);
                return Ok(offer);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured: {ex.Message}");
            }
        }

        [HttpGet("mine")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Returns list of offers for current owner",
            Description = "Requires verified owner privileges"
        )]
        public async Task<ActionResult> GetOffersForCurrentOwner()
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var offers = await _offerService.GetOffersForCurrentUserAsync(requestUserId);
                return Ok(offers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // GET: api/offers
        [HttpGet]
        [SwaggerOperation(
            Summary = "Returns a filtered and sorted list of offers"
        )]
        public async Task<IActionResult> GetFilteredAndSorted([FromQuery] GetFilteredAndSortedOffersDto input)
        {
            try
            {
                var requestUserId = 0;

                if (int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int parsedUserId))
                {
                    requestUserId = parsedUserId;
                }

                var offers = await _offerService.GetFilteredAndSortedOffersAsync(input, requestUserId);
                return Ok(offers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // GET: api/offers/map
        [HttpGet("map")]
        [SwaggerOperation(
            Summary = "Returns a filtered list of offers for map"
        )]
        public async Task<IActionResult> GetFilteredForMap([FromQuery] GetFilteredOffersDto input)
        {
            try
            {
                var offers = await _offerService.GetFilteredOffersForMapAsync(input);
                return Ok(offers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // POST: api/offers
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
                return Ok(new OutputDto<string>("Property added successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding offer - body: {input}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // POST: api/offers/{id}/promotion
        [HttpPost("{id}/promotion")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Adds an offer promotion",
            Description = "Requires verified owner privileges"
        )]
        public async Task<IActionResult> AddOfferPromotion([FromBody] AddOfferPromotionDto input, int id)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _offerService.AddOfferPromotionAsync(input.Duration, id, requestUserId);
                _logger.LogInformation($"Adding offer promotion for offer ID: {id}");
                return Ok(new OutputDto<string>("Offer promotion added successfully"));
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

        // GET: api/offers/interest
        [HttpGet("interest")]
        [Authorize(Policy = "VerifiedStudent")]
        [SwaggerOperation(
            Summary = "Returns a list of offers observed by the student",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> GetOffersByInterest([FromQuery] PaginatorDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var observedOffers = await _offerService.GetOffersByInterestAsync(input, requestUserId);
                _logger.LogInformation($"Getting the list of offers observed by the student ID: {requestUserId}");
                return Ok(observedOffers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // POST: api/offers/{id}/interest
        [HttpPost("{id}/interest")]
        [Authorize(Policy = "VerifiedStudent")]
        [SwaggerOperation(
            Summary = "Adds an offer interest",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> AddOfferInterest(int id)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _offerService.AddOfferInterestAsync(id, requestUserId);
                _logger.LogInformation($"Adding offer interest for offer ID: {id}");
                return Ok(new OutputDto<string>("Interest addded"));
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // DELETE: api/offers/{id}/interest
        [HttpDelete("{id}/interest")]
        [Authorize(Policy = "VerifiedStudent")]
        [SwaggerOperation(
            Summary = "Removes an offer interest",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> RemoveOfferInterest(int id)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _offerService.RemoveOfferInterestAsync(id, requestUserId);
                _logger.LogInformation($"Removing offer interest for offer ID: {id}");
                return Ok(new OutputDto<string>("Interest removed"));
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // POST: api/offers/{offerId}/rent
        [HttpPost("{offerId}/rent")]
        [Authorize(Policy = "VerifiedStudent")]
        [SwaggerOperation(
            Summary = "Adds rent proposition to an offer",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> ProposeRent(int offerId, [FromBody] ProposeRentDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _rentService.ProposeRentAsync(input, requestUserId, offerId);
                _logger.LogInformation($"Adding rent proposition for offer ID: {offerId}");
                return Ok(new OutputDto<string>("Rent proposition added"));
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // POST: api/offers/{offerId}/rent
        [HttpPut("{offerId}/rent/accept")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Adds rent proposition to an offer",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> ProposeRent(int offerId, [FromBody] AcceptDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _rentService.AcceptRentAsync(input.Decision, requestUserId, offerId);
                _logger.LogInformation($"Accepting rent proposition for offer ID: {offerId}");
                return Ok(new OutputDto<string>("Rent accepted"));
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

        [HttpPost("{rentId}/rent/opinion")]
        [Authorize(Policy = "Student")]
        [SwaggerOperation(
            Summary = "Adds rent opinion",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> AddRentOpinion(int rentId, RentOpinionDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _rentService.AddRentOpinionAsync(input, requestUserId, rentId);
                _logger.LogInformation($"Adding rent opinion for rent ID: {rentId}");
                return Ok(new OutputDto<string>("Opinion added"));
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                //return BadRequest($"An error occurred: {ex.Message}");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }
    }

}
