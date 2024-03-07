using AutoMapper;
using Flats4us.Entities;
using Flats4us.Services.Interfaces;

namespace Flats4us.Services
{
    public class EmailService : IEmailService
    {
        public readonly Flats4usContext _context;

        public EmailService(
            Flats4usContext context)
        {
            _context = context;
        }

        public Task SendEmailAsync(string to, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
