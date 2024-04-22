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
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public PaymentService(
            Flats4usContext context,
            IMapper mapper,
            IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
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
            payment.PaidAtDate = DateTime.Now;

            
            await _emailService.SendEmailAsync(requestUserId, "Payment confirmed!", "Thank you for using our service");

            await _context.SaveChangesAsync();
        }
    }
}
