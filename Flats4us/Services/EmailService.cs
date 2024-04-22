using Flats4us.Helpers;
using Flats4us.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Flats4us.Services
{
    public class EmailService : IEmailService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        private string Server => _configuration["Smtp:Server"];
        private int Port => int.Parse(_configuration["Smtp:Port"]);
        private string SenderAddress => _configuration["Smtp:SenderAddress"];
        private string AppPassword => _configuration["Smtp:AppPassword"];
        private bool EnableSsl => bool.Parse(_configuration["Smtp:EnableSsl"]);
        private SmtpDeliveryMethod SmtpDeliveryMethod => Enum.Parse<SmtpDeliveryMethod>(_configuration["Smtp:SmtpDeliveryMethod"]);
        private bool UseDefaultCredentials => bool.Parse(_configuration["Smtp:UseDefaultCredentials"]);

        public EmailService(
            IUserService userService, 
            IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(int toUserId, string subject, string body)
        {
            var userInfo = await _userService.GetUserInfo(toUserId);
            if (!userInfo.EmailConsent)
                return;

            using (var client = new SmtpClient())
            {
                client.Host = Server;
                client.Port = Port;
                client.DeliveryMethod = SmtpDeliveryMethod;
                client.UseDefaultCredentials = UseDefaultCredentials;
                client.EnableSsl = EnableSsl;
                client.Credentials = new NetworkCredential(SenderAddress, AppPassword);

                using var message = new MailMessage(
                    from: new MailAddress(SenderAddress, "Flats4us"),
                    to: new MailAddress(userInfo.Email, userInfo.FullName)
                );

                message.Subject = subject;
                message.Body = EmailHelper.PrepareEmail(body);
                message.IsBodyHtml = true;

                await client.SendMailAsync(message);
            }
        }
    }
}
