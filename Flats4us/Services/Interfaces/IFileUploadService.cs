using Flats4us.Entities;

namespace Flats4us.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task<FileUpload> CreateFileFromIFormFileAsync(IFormFile file);
        Task<FileUpload> CreateFileFromSourceFilePathAsync(string sourceFilePath);
        Task DeleteFileByNameAsync(string fileName);
    }
}
