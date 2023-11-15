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

        public static void SeedPropertyImage(string path)
        {
            var directoryPath = $"Images/Properties/{path}";
            var sourceDirectory = "Images/PropertiesSeed";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(directoryPath);

                var sourceTitleDeedDirectory = Path.Combine(sourceDirectory, "TitleDeed");
                var sourceImagesDirectory = Path.Combine(sourceDirectory, "Images");

                var targetTitleDeedDirectory = Path.Combine(directoryPath, "TitleDeed");
                var targetImagesDirectory = Path.Combine(directoryPath, "Images");

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

                if (Directory.Exists(sourceImagesDirectory))
                {
                    Directory.CreateDirectory(targetImagesDirectory);
                    var files = Directory.GetFiles(sourceImagesDirectory);
                    foreach (var file in files)
                    {
                        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(file)}";
                        var destinationPath = Path.Combine(targetImagesDirectory, newFileName);
                        File.Copy(file, destinationPath);
                    }
                }
            }
        }
        public static void SeedUserImage(string path)
        {
            var directoryPath = $"Images/Users/{path}";
            var sourceDirectory = "Images/UsersSeed";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(directoryPath);

                var sourceProfilePicturesDirectory = Path.Combine(sourceDirectory, "ProfilePictures");
                var sourceDocumentsDirectory = Path.Combine(sourceDirectory, "Documents");

                var targetProfilePicturesDirectory = Path.Combine(directoryPath, "ProfilePictures");
                var targetDocumentsDirectory = Path.Combine(directoryPath, "Documents");

                if (Directory.Exists(sourceProfilePicturesDirectory))
                {
                    Directory.CreateDirectory(targetProfilePicturesDirectory);
                    var files = Directory.GetFiles(sourceProfilePicturesDirectory);
                    foreach (var file in files)
                    {
                        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(file)}";
                        var destinationPath = Path.Combine(targetProfilePicturesDirectory, newFileName);
                        File.Copy(file, destinationPath);
                    }
                }

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
