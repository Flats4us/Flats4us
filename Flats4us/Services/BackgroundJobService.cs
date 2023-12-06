using Flats4us.Controllers;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
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

        // EXAMPLE
        public async Task TestAsync()
        {
            _logger.LogInformation("Test job1 executed at: " + DateTime.Now);

            var result = await _context.Equipment
                .Select(e => new EquipmentDto
                {
                    EquipmentId = e.EquipmentId,
                    Name = e.Name
                })
                .ToListAsync();
        }
        public async Task PaymentAsync()
        {
            _logger.LogInformation("Payment executed at: " + DateTime.Now);

            var result = await _context.Rents
                .Where(rent =>
                    rent.PaymentDate.Date == DateTime.Now.AddDays(-7).Date
                )
                .ToListAsync();

            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    item.Student.Payments.Add(new Payment
                    {
                        Amount = 100,
                        Student = item.Student,
                        Rent = item,
                        PaymentPurpose = Helpers.Enums.PaymentPurpose.Rent
                    });
                }
                await _context.SaveChangesAsync();
            }


        }
    }
}
