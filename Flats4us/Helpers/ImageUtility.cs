using Flats4us.Helpers.Enums;

namespace Flats4us.Helpers
{
    public class ImageUtility
    {
        public static async Task ProcessAndSaveImage(IFormFile file, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string extension = Path.GetExtension(file.FileName);
            if (extension == null)
            {
                throw new ArgumentException("Unsupported file format");
            }

            string uniqueFileName = Guid.NewGuid() + extension;
            string filePath = Path.Combine(directoryPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public static async Task DeletePropertyFileAsync(string propertyDirectory, string fileId)
        {
            string basePath = "Images/Properties";

            string[] possibleDirectories = {
                Path.Combine(basePath, propertyDirectory, "Images"),
                Path.Combine(basePath, propertyDirectory, "TitleDeed")
            };

            try
            {
                foreach (var directory in possibleDirectories)
                {
                    if (Directory.Exists(directory))
                    {
                        var files = await Task.Run(() => Directory.GetFiles(directory, fileId + ".*"));
                        foreach (var file in files)
                        {
                            await Task.Run(() => File.Delete(file));
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                throw new IOException("Error during file operation: " + ex.Message);
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
    }
}
