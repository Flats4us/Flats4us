using AutoMapper;
using Flats4us.Entities;
using Flats4us.Helpers.Exceptions;
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

        public async Task PayPaymentAsync(int paymentId, int requestUserId)
        {
            var payment = await _context.Payments
                .Include(p => p.Rent)
            .FirstOrDefaultAsync(o => o.PaymentId == paymentId);

            if (payment is null) throw new ArgumentException($"Offer with ID {paymentId} not found.");

            if (payment.Rent.StudentId != requestUserId) throw new ForbiddenException($"You are not a main student in this rent, you cannot pay");

            if (payment.IsPaid) throw new ArgumentException($"Payment ID: {paymentId} is already paid");

            payment.IsPaid = true;

            await _context.SaveChangesAsync();
        }
    }
}
