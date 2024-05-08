using Flats4us.Services.Interfaces;
using Newtonsoft.Json;

public class OpenStreetMapService : IOpenStreetMapService
{
    private readonly HttpClient _httpClient;
    private const string OpenStreetMapApiUrl = "https://nominatim.openstreetmap.org/search";


    public OpenStreetMapService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Flats4Us-StudentProject");
    }

    public async Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string province, string? district, string? street, string? number, string city, string? postalCode)
    {

        string address = $"{number}, {street}, {district}, {city}, {province}, {postalCode}";
        string query = $"{OpenStreetMapApiUrl}?format=json&q={Uri.EscapeDataString(address)}";

        HttpResponseMessage response = await _httpClient.GetAsync(query);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<OpenStreetMapResponse[]>(json);

            if (results.Length > 0)
            {
                var firstResult = results[0];
                return (firstResult.Lat, firstResult.Lon);
            }
            else
            {
                throw new Exception("Cannot find geo cords of given address");
            }
        }
        else
        {
            throw new Exception("Nominatim error");
        }
    }

    public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double EarthRadiusKm = 6371;

        double lat1Rad = DegreeToRadian(lat1);
        double lon1Rad = DegreeToRadian(lon1);
        double lat2Rad = DegreeToRadian(lat2);
        double lon2Rad = DegreeToRadian(lon2);

        double dLat = lat2Rad - lat1Rad;
        double dLon = lon2Rad - lon1Rad;

        double a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Pow(Math.Sin(dLon / 2), 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = EarthRadiusKm * c;

        return distance;
    }

    private static double DegreeToRadian(double degree)
    {
        return degree * Math.PI / 180.0;
    }
}

public class OpenStreetMapResponse
{
    public double Lat { get; set; }
    public double Lon { get; set; }
}
