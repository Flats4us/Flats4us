namespace Flats4us.Services.Interfaces
{
    public interface IPaymentService
    {
        Task PayPaymentAsync(int id, int requestUserId);
    }
}
