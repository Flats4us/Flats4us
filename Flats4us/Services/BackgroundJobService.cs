using Flats4us.Controllers;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Flats4us.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly Flats4usContext _context;
        private readonly ILogger<BackgroundJobService> _logger;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;

        public BackgroundJobService(Flats4usContext context,
            ILogger<BackgroundJobService> logger,
            INotificationService notificationService,
            IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _notificationService = notificationService;
            _configuration = configuration;
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

            var appBaseUrl = _configuration.GetSection("AppBaseUrl").Value;
            var baseLink = $"{appBaseUrl}/rents/student";

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

                var emailBody = new StringBuilder();
                var link = $"{baseLink}/{rent.RentId}";
                emailBody.AppendLine(EmailHelper.HtmlHTag($"Masz nową oczekującą płatność", 1))
                            .AppendLine(EmailHelper.HtmlPTag($"Aby ją wyświetlić naciśnij {EmailHelper.AddLinkToText(link, "TUTAJ")} lub przejdź pod poniższy link"))
                            .AppendLine(EmailHelper.HtmlPTag($"{link}"));

                await _notificationService.SendNotificationAsync(EmailTitles.NewPaymentGenerated, emailBody.ToString(), TranslateKeys.NewPaymentTitle, TranslateKeys.NewPaymentBody, rent.StudentId, false);

                if (rent.Payments.Where(p => p.PaymentPurpose == PaymentPurpose.Rent).Count() < rent.Duration ) 
                {
                    rent.NextPaymentGenerationDate = DateTime.Now.AddMonths(1).Date;
                }
                else
                {
                    rent.NextPaymentGenerationDate = null;
                }
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Payment generation ended at: " + DateTime.Now);
        }

        public async Task CheckAndChangeOfferStatusesAsync()
        {
            _logger.LogInformation("Offer status check executed at: " + DateTime.Now);

            var date = DateTime.Now.Date;

            var endingRentedOffers = await _context.Offers
                .Include(o => o.Rent)
                .Where(o => o.OfferStatus == OfferStatus.Rented &&
                    o.Rent.EndDate.Date < date)
                .ToListAsync();

            foreach (var offer in endingRentedOffers)
            {
                offer.OfferStatus = OfferStatus.Old;
            }

            var endingCurrentOffers = await _context.Offers
                .Where(o => o.OfferStatus == OfferStatus.Current &&
                    o.EndDate.Date < date)
                .ToListAsync();

            foreach (var offer in endingCurrentOffers)
            {
                offer.OfferStatus = OfferStatus.Old;
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Offer status check ended at: " + DateTime.Now);
        }
    }
}
