namespace Flats4us.Services.Interfaces
{
    public interface IBackgroundJobService
    {
        Task GeneratePaymentsAsync();
    }
}
