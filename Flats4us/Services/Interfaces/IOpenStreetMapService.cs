namespace Flats4us.Services.Interfaces
{
    public interface IOpenStreetMapService
    {
        Task<OpenStreetMapResponse> GetCoordinatesAsync(string province, string? district, string? street, string? number, string city, string? postalCode);

        double CalculateDistance(double lat1, double lon1, double lat2, double lon2);
    }
}
