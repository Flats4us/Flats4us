using Flats4us.Helpers.Enums;

namespace Flats4us.Helpers
{
    public class ImageUtility
    {
        public static async Task ProcessAndSaveImage(byte[] imageData, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string extension = GetFileExtension(imageData);
            if (extension == null)
            {
                throw new ArgumentException("Unsupported file format");
            }

            string uniqueFileName = Guid.NewGuid() + extension;
            string filePath = Path.Combine(directoryPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await stream.WriteAsync(imageData, 0, imageData.Length);
            }
        }

        public static void SeedPropertyImage(string path, VerificationStatus status)
        {
            var directoryPath = $"Images/Properties/{path}";
            var sourceDirectory = "Images/PropertiesSeed";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);

                if ( status == VerificationStatus.NotVerified) {
                    var sourceTitleDeedDirectory = Path.Combine(sourceDirectory, "TitleDeed");
                    var targetTitleDeedDirectory = Path.Combine(directoryPath, "TitleDeed");

                    if (Directory.Exists(sourceTitleDeedDirectory))
                    {
                        Directory.CreateDirectory(targetTitleDeedDirectory);
                        var files = Directory.GetFiles(sourceTitleDeedDirectory);
                        foreach (var file in files)
                        {
                            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(file)}";
                            var destinationPath = Path.Combine(targetTitleDeedDirectory, newFileName);
                            File.Copy(file, destinationPath);
                        }
                    }
                }

                var sourceImagesDirectory = Path.Combine(sourceDirectory, "Images");
                var targetImagesDirectory = Path.Combine(directoryPath, "Images");

                if (Directory.Exists(sourceImagesDirectory))
                {
                    Directory.CreateDirectory(targetImagesDirectory);

                    var random = new Random();
                    var files = Directory.GetFiles(sourceImagesDirectory);
                    var selectedFiles = files.OrderBy(f => random.Next()).Take(4);

                    foreach (var file in selectedFiles)
                    {
                        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(file)}";
                        var destinationPath = Path.Combine(targetImagesDirectory, newFileName);
                        File.Copy(file, destinationPath);
                    }
                }
            }
        }

        public static async Task SeedUserImage(string path, VerificationStatus status, DocumentType documentType)
        {
            var directoryPath = $"Images/Users/{path}";
            var sourceDirectory = "Images/UsersSeed";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);

                if (status == VerificationStatus.NotVerified)
                {
                    var sourceDocumentsDirectory = string.Empty;

                    switch (documentType)
                    {
                        case DocumentType.StudentCard:
                            sourceDocumentsDirectory = Path.Combine(sourceDirectory, "StudentCard");
                            break;
                        case DocumentType.ID:
                            sourceDocumentsDirectory = Path.Combine(sourceDirectory, "ID");
                            break;
                        case DocumentType.Passport:
                            sourceDocumentsDirectory = Path.Combine(sourceDirectory, "Passport");
                            break;
                    }
                    
                    var targetDocumentsDirectory = Path.Combine(directoryPath, "Documents");

                    if (Directory.Exists(sourceDocumentsDirectory))
                    {
                        Directory.CreateDirectory(targetDocumentsDirectory);
                        var files = Directory.GetFiles(sourceDocumentsDirectory);
                        foreach (var file in files)
                        {
                            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(file)}";
                            var destinationPath = Path.Combine(targetDocumentsDirectory, newFileName);
                            File.Copy(file, destinationPath);
                        }
                    }
                }

                var sourceProfilePicturesDirectory = Path.Combine(sourceDirectory, "ProfilePicture");
                var targetProfilePicturesDirectory = Path.Combine(directoryPath, "ProfilePicture");

                if (Directory.Exists(sourceProfilePicturesDirectory))
                {
                    Directory.CreateDirectory(targetProfilePicturesDirectory);
                    string destinationPath;

                    try
                    {
                        using (var httpClient = new HttpClient())
                        {
                            var imageUrl = await GetRandomProfilePictureUrl(httpClient);
                            var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                            var newFileName = $"{Guid.NewGuid()}.jpg";
                            destinationPath = Path.Combine(targetProfilePicturesDirectory, newFileName);

                            File.WriteAllBytes(destinationPath, imageBytes);
                        }
                    }
                    catch (Exception)
                    {
                        var backupImageFile = Directory.GetFiles(sourceProfilePicturesDirectory).FirstOrDefault();
                        if (backupImageFile != null)
                        {
                            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(backupImageFile)}";
                            destinationPath = Path.Combine(targetProfilePicturesDirectory, newFileName);
                            File.Copy(backupImageFile, destinationPath);
                        }
                    }

                    await Task.Delay(1000);
                }
            }
        }

        public static async Task DeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                await Task.Run(() => Directory.Delete(directoryPath, true));
            }
        }

        static async Task<string> GetRandomProfilePictureUrl(HttpClient httpClient)
        {
            var apiUrl = "https://randomuser.me/api/";
            var response = await httpClient.GetStringAsync(apiUrl);

            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
            var imageUrl = json.results[0].picture.large;

            return imageUrl;
        }

        private static string GetFileExtension(byte[] imageData)
        {
            // JPG: FF D8 FF
            if (imageData.Length >= 3 && imageData[0] == 0xFF && imageData[1] == 0xD8 && imageData[2] == 0xFF)
            {
                return ".jpg";
            }
            // PNG: 89 50 4E 47
            else if (imageData.Length >= 4 && imageData[0] == 0x89 && imageData[1] == 0x50 && imageData[2] == 0x4E && imageData[3] == 0x47)
            {
                return ".png";
            }
            // PDF: 25 50 44 46
            else if (imageData.Length >= 4 && imageData[0] == 0x25 && imageData[1] == 0x50 && imageData[2] == 0x44 && imageData[3] == 0x46)
            {
                return ".pdf";
            }

            return null;
        }
    }
}
