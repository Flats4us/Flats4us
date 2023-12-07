using Flats4us.Entities;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class PaymentService : IPaymentService
    {
        public readonly Flats4usContext _context;

        public PaymentService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentByRentId(int id)
        {
            return await _context.Payments.FirstAsync(x => x.RentId == id);
        }

        public async Task EditStatusPaymentAsync(int id, PaymentStatus status)
        {
            var payment = await _context.Payments.FirstAsync(x => x.PaymentId == id);
            if (payment == null)
                return;

            payment.PaymentStatus = status;

            //await _context.Payments.UpdateAsync(payment);
            await _context.SaveChangesAsync();
        }


    }
}
