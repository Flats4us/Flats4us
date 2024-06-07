using Flats4us.Entities;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class FileUploadService : IFileUploadService
    {
        public readonly Flats4usContext _context;
        private readonly IConfiguration _configuration;

        private string DefaultUploadFolderPath => _configuration["FileUploadSettings:UploadPath"];

        public FileUploadService(Flats4usContext context,
            IConfiguration configuration)
        {
            _context = context;
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

        public async Task DeleteFileByNameAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));

            var filePath = Path.Combine(DefaultUploadFolderPath, fileName);

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));

                var fileUpload = await _context.FileUploads.FirstOrDefaultAsync(f => f.Name == fileName);

                if (fileUpload != null)
                {
                    _context.FileUploads.Remove(fileUpload);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                throw new FileNotFoundException("File not found.", filePath);
            }
        }

        public async Task ClearDirectoryAsync(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                await Task.Run(() =>
                {
                    var files = Directory.GetFiles(directoryPath);
                    var directories = Directory.GetDirectories(directoryPath);

                    foreach (var file in files)
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                    }

                    foreach (var directory in directories)
                    {
                        ClearDirectoryAsync(directory).Wait();
                        Directory.Delete(directory);
                    }
                });
            }
        }

    }
}
