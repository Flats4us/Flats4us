using AutoMapper;
using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Helpers.AutoMapperResolvers
{
    public class PropertyDocumentUrlResolver : IValueResolver<Property, PropertyForVerificationDto, FileDto>
    {
        public FileDto Resolve(Property source, PropertyForVerificationDto destination, FileDto destMember, ResolutionContext context)
        {
            var directoryPath = Path.Combine("Images", "Properties", source.ImagesPath, "TitleDeed");

            if (Directory.Exists(directoryPath))
            {
                var files = Directory.GetFiles(directoryPath);

                if (files.Length > 0)
                {
                    var fileNameWithExtension = Path.GetFileName(files[0]);
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(files[0]);
                    return new FileDto { Name = fileNameWithoutExtension, Path = Path.Combine(directoryPath, fileNameWithExtension) };
                }
            }
            return new FileDto();
        }
    }
}