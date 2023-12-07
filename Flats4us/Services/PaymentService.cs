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

        public async Task<List<Payment>> GetPaymentsByRentIdAsync(int id)
        {
            return await _context.Payments.Where(x => x.RentId == id).ToListAsync();
        }

        public async Task EditStatusPaymentAsync(int id, bool isPaid)
        {
            var payment = await _context.Payments.FirstAsync(x => x.PaymentId == id);
            if (payment == null)
                return;

            payment.IsPaid = isPaid;

            //await _context.Payments.UpdateAsync(payment);
            await _context.SaveChangesAsync();
        }


    }
}
