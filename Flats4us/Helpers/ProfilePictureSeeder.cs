using Flats4us.Helpers.Enums;

namespace Flats4us.Helpers
{
    public class ProfilePictureSeeder
    {
        public static async Task<string> GetRandomProfilePicturePath(Gender gender)
        {
            using (var httpClient = new HttpClient())
            {
                var imageUrl = await GetRandomProfilePictureUrl(httpClient, gender);

                var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                var fileName = $"{Guid.NewGuid()}.jpg";
                var filePath = Path.Combine(Path.GetTempPath(), fileName);

                File.WriteAllBytes(filePath, imageBytes);

                return filePath;
            }
        }

        static async Task<string> GetRandomProfilePictureUrl(HttpClient httpClient, Gender gender)
        {
            var apiUrl = $"https://randomuser.me/api/?gender={(gender == Gender.Male ? "male" : "female")}";
            var response = await httpClient.GetStringAsync(apiUrl);

            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
            var imageUrl = json.results[0].picture.large;

            return imageUrl;
        }
    }
}
