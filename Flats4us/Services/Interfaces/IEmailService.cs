namespace Flats4us.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(int toUserId, string subject, string body, bool chatEmail = false);
    }
}
