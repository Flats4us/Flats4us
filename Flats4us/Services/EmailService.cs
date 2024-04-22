using Flats4us.Entities;
using Flats4us.Helpers;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace Flats4us.Services
{
    public class EmailService : IEmailService
    {
        public readonly Flats4usContext _context;
        private readonly IConfiguration _configuration;


        private string Server => _configuration["Smtp:Server"];
        private int Port => int.Parse(_configuration["Smtp:Port"]);
        private string SenderAddress => _configuration["Smtp:SenderAddress"];
        private string AppPassword => _configuration["Smtp:AppPassword"];
        private bool EnableSsl => bool.Parse(_configuration["Smtp:EnableSsl"]);
        private SmtpDeliveryMethod SmtpDeliveryMethod => Enum.Parse<SmtpDeliveryMethod>(_configuration["Smtp:SmtpDeliveryMethod"]);
        private bool UseDefaultCredentials => bool.Parse(_configuration["Smtp:UseDefaultCredentials"]);

        public EmailService(
            IConfiguration configuration,
            Flats4usContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task SendEmailAsync(int toUserId, string subject, string body)
        {
            
            var sender = await _context.Users.FindAsync(toUserId);
            if (sender == null)
                return;
            if (!sender.EmailConsent)
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
                        to: new MailAddress("flats4us.system@gmail.com", sender.Email)
                    );

                    message.Subject = subject;
                    message.Body = EmailHelper.PrepareEmail(body);
                    message.IsBodyHtml = true;

                    await client.SendMailAsync(message);
                }
            }
            


        }
        }

