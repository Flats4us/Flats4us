using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/mailtest")]
    [ApiController]
    public class MailTestController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<MailTestController> _logger;

        public MailTestController(
            IEmailService emailService,
            ILogger<MailTestController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet("send-email")]
        [SwaggerOperation(
            Summary = "Logs the user in, returns a token"
        )]
        public async Task<ActionResult> SendEmail([FromBody] SendEmailDto request)
        {
            await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);

            return Ok(new OutputDto<string>("ok"));
        }
    }

    public class SendEmailDto
    {
        [Required]
        public string To { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
