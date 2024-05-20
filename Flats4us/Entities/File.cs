using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class File
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        public static readonly string DefaultUploadFolderPath = System.IO.Path.Combine("Images", "Files");

        public File() { }

        public File(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            Path = SaveFileAsync(file).GetAwaiter().GetResult();
            Name = System.IO.Path.GetFileName(Path);
        }

        public File(string sourceFilePath)
        {
            if (string.IsNullOrEmpty(sourceFilePath)) throw new ArgumentNullException(nameof(sourceFilePath));

            var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(sourceFilePath);
            Name = fileName;
            Path = System.IO.Path.Combine(DefaultUploadFolderPath, fileName);

            if (!Directory.Exists(DefaultUploadFolderPath))
            {
                Directory.CreateDirectory(DefaultUploadFolderPath);
            }

            System.IO.File.Copy(sourceFilePath, Path);
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
            var filePath = System.IO.Path.Combine(DefaultUploadFolderPath, fileName);

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
    }
}
