using Flats4us.Controllers;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly Flats4usContext _context;
        private readonly ILogger<BackgroundJobService> _logger;
        private readonly IEmailService _emailService;
        private readonly INotificationService _notificationService;


        public BackgroundJobService(Flats4usContext context,
            ILogger<BackgroundJobService> logger,
            IEmailService emailService,
            INotificationService notificationService)
        {
            _context = context;
            _logger = logger;
            _emailService = emailService;
            _notificationService = notificationService;
        }

        public async Task GeneratePaymentsAsync()
        {
            _logger.LogInformation("Payment generation executed at: " + DateTime.Now);

            var rents = await _context.Rents
                .Include(r => r.Offer)
                .Where(r =>
                    r.NextPaymentGenerationDate != null &&
                    r.NextPaymentGenerationDate == DateTime.Now.Date
                )
                .ToListAsync();

            if (rents.Count > 0)
            {
                foreach (var rent in rents)
                {
                    
                    rent.Payments.Add(new Payment
                    {
                        PaymentPurpose = PaymentPurpose.Rent,
                        Amount = rent.Offer.Price,
                        IsPaid = false,
                        CreatedDate = DateTime.Now,
                        PaymentDate = DateTime.Now.AddDays(7).Date
                    });

                    if (rent.Payments.Where(p => p.PaymentPurpose == PaymentPurpose.Rent).Count() < rent.Duration ) 
                    {
                        rent.NextPaymentGenerationDate = DateTime.Now.AddMonths(1).Date;
                    }
                    else
                    {
                        rent.NextPaymentGenerationDate = null;
                    }
                    var reminderMessage = $"This is a reminder that your rent payment of ${rent.Offer.Price} is due on {DateTime.Now.AddDays(7).Date}. Please make the payment on time.";
                    await _notificationService.SendNotificationAsync("Rent Payment Reminder", reminderMessage, rent.StudentId);
                    await _emailService.SendEmailAsync(rent.StudentId, reminderMessage, "Rent Payment Reminder");

                    if (DateTime.Now.Date == rent.StartDate.Date)
                    {
                        foreach (var student in rent.OtherStudents)
                        {
                            var emailMessage = $"Dear {student.Name}, welcome to your new rental property. Your rental contract has officially started today. If you have any questions or concerns, feel free to contact us.";

                            await _emailService.SendEmailAsync(student.UserId, "Welcome to your new rental property!", emailMessage);
                            await _notificationService.SendNotificationAsync("Welcome to your new rental property!", emailMessage, rent.StudentId);

                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
