using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(
            ILogger<PaymentController> logger,
            IPaymentService argumentService
        )
        {
            _logger = logger;
            _paymentService = argumentService;
        }

        [HttpGet("get_by_rentId_payment")]
        public async Task<IActionResult> GetPaymentByRentId(int id)
        {
            //try
            //{
            //    _logger.LogInformation("Geting Payment by RentId");
            //    var payment = await _paymentService.GetPaymentByRentId(id);

            //    return Ok(payment);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogInformation($"FAILED: Editing argument");
            //    return BadRequest($"An error occurred: {ex.InnerException.Message}");
            //}
            try
            {
                _logger.LogInformation("Getting Payment by RentId");
                var payment = await _paymentService.GetPaymentByRentId(id);

                if (payment == null)
                {
                    _logger.LogInformation($"Payment with Id {id} not found");
                    return NotFound($"Payment with Id {id} not found");
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve payment by RentId. Error: {ex.Message}");
                return BadRequest($"An error occurred: {ex.InnerException?.Message}");
            }
        }

        [HttpPut("put_status_payment")]
        public async Task<IActionResult> PutPayment(int id, PaymentStatus status)
        {
            try
            {
                _logger.LogInformation("Put Payment");
                await _paymentService.EditStatusPaymentAsync(id, status);
                return Ok("Payment status changed");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Editing argument");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }
    }
}
