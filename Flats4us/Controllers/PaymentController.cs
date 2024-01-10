using Flats4us.Entities.Dto;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(
            IPaymentService paymentService,
            ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPut("{id}/pay")]
        [Authorize(Policy = "VerifiedStudent")]
        [SwaggerOperation(
            Summary = "Changes payment isPaid value",
            Description = "Requires verified student privileges"
        )]
        public async Task<IActionResult> PayPayment(int id)
        {
            try
            {
                _logger.LogInformation($"Paying payment ID: {id}");
                await _paymentService.PayPaymentAsync(id);
                return Ok(new OutputDto<string>("Payment status changed"));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Paying payment ID: {id}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
