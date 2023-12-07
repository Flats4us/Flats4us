using Flats4us.Entities.Dto;
using Flats4us.Entities;
using Flats4us.Helpers.Enums;

namespace Flats4us.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task<List<Payment>> GetPaymentByRentId(int id);
        public Task EditStatusPaymentAsync(int id, PaymentStatus status);
    }
}
