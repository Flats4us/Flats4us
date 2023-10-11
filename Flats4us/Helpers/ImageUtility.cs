namespace Flats4us.Helpers
{
    public class ImageUtility
    {
        public static async Task ProcessAndSaveImage(IFormFile image, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string uniqueFileName = Guid.NewGuid() + Path.GetExtension(image.FileName);

            string filePath = Path.Combine(directoryPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
        }

        public static async Task DeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                await Task.Run(() => Directory.Delete(directoryPath, true));
            }
        }
    }
}
