using Flats4us.Entities;
using Flats4us.Services.Interfaces;

namespace Flats4us.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IConfiguration _configuration;

        private string DefaultUploadFolderPath => _configuration["FileUploadSettings:UploadPath"];

        public FileUploadService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<FileUpload> CreateFileFromIFormFileAsync(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            var path = await SaveFileAsync(file);
            var name = Path.GetFileName(path);

            return new FileUpload { Name = name, Path = path };
        }

        public async Task<FileUpload> CreateFileFromSourceFilePathAsync(string sourceFilePath)
        {
            if (string.IsNullOrEmpty(sourceFilePath)) throw new ArgumentNullException(nameof(sourceFilePath));

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(sourceFilePath);
            var path = Path.Combine(DefaultUploadFolderPath, fileName);

            if (!Directory.Exists(DefaultUploadFolderPath))
            {
                Directory.CreateDirectory(DefaultUploadFolderPath);
            }

            await CopyFileAsync(sourceFilePath, path);

            return new FileUpload { Name = fileName, Path = path };
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(DefaultUploadFolderPath, fileName);

            if (!Directory.Exists(DefaultUploadFolderPath))
            {
                Directory.CreateDirectory(DefaultUploadFolderPath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }

        private async Task CopyFileAsync(string sourceFilePath, string destinationFilePath)
        {
            using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            using (var destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                await sourceStream.CopyToAsync(destinationStream);
            }
        }
    }
}
