using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        [HttpPost("send-email")]
        [SwaggerOperation(
            Summary = "Logs the user in, returns a token"
        )]
        public async Task<ActionResult> SendEmail([FromBody] SendEmailDto request)
        {
            var body = new StringBuilder();

            body.AppendLine(EmailHelper.HtmlHTag("Masz nową propozycje wynajmu!", 1))
                .AppendLine(EmailHelper.HtmlPTag($"Użytkownik {EmailHelper.HtmlBTag("Jan Kowalski")} zgłasza chęć wynajęcia twojego lokalu"))
                .AppendLine(EmailHelper.HtmlPTag($"Aby sprawdzić jego profil i podjąć decyzję naciśnij {EmailHelper.AddLinkToText("https://www.google.com/", "TUTAJ")}"));

            await _emailService.SendEmailAsync(request.To, "Nowa propozycja wynajmu", body.ToString());

            return Ok(new OutputDto<string>("Email sent"));
        }
    }

    public class SendEmailDto
    {
        [Required]
        public int To { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
