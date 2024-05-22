
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class FileUpload
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileUploadId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }
    }
}
