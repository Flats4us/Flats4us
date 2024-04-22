using Flats4us.Entities.Dto;
using Flats4us.Entities;
using Flats4us.Helpers;
using Flats4us.Services.Interfaces;
using System.Net;
using System.Net.Mail;
using AutoMapper;

namespace Flats4us.Services
{
    public class EmailService : IEmailService
    {
        public readonly Flats4usContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        private string Server => _configuration["Smtp:Server"];
        private int Port => int.Parse(_configuration["Smtp:Port"]);
        private string SenderAddress => _configuration["Smtp:SenderAddress"];
        private string AppPassword => _configuration["Smtp:AppPassword"];
        private bool EnableSsl => bool.Parse(_configuration["Smtp:EnableSsl"]);
        private SmtpDeliveryMethod SmtpDeliveryMethod => Enum.Parse<SmtpDeliveryMethod>(_configuration["Smtp:SmtpDeliveryMethod"]);
        private bool UseDefaultCredentials => bool.Parse(_configuration["Smtp:UseDefaultCredentials"]);

        public EmailService(
            Flats4usContext context,
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(int toUserId, string subject, string body)
        {
            var user = await _context.Users.FindAsync(toUserId);

            if (user is null) throw new ArgumentException($"User with ID {toUserId} not found.");

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
                    to: new MailAddress("flats4us.system@gmail.com", userInfo.FullName)
                );

                message.Subject = subject;
                message.Body = EmailHelper.PrepareEmail(body);
                message.IsBodyHtml = true;

                await client.SendMailAsync(message);
            }
        }
    }
}
