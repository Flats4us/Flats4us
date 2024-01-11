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

        public BackgroundJobService(Flats4usContext context,
            ILogger<BackgroundJobService> logger)
        {
            _context = context;
            _logger = logger;
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
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
