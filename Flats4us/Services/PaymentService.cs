using AutoMapper;
using Flats4us.Entities;
using Flats4us.Services.Interfaces;

namespace Flats4us.Services
{
    public class PaymentService : IPaymentService
    {
        public readonly Flats4usContext _context;
        private readonly IMapper _mapper;

        public PaymentService(
            Flats4usContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task PayPaymentAsync(int id)
        {
            
        }
    }
}
