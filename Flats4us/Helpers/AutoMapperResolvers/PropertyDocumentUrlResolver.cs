using AutoMapper;
using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Helpers.AutoMapperResolvers
{
    public class PropertyDocumentUrlResolver : IValueResolver<Property, PropertyForVerificationDto, string>
    {
        public string Resolve(Property source, PropertyForVerificationDto destination, string destMember, ResolutionContext context)
        {
            var directoryPath = Path.Combine("Images", "Properties", source.ImagesPath, "TitleDeed");

            if (Directory.Exists(directoryPath))
            {
                var files = Directory.GetFiles(directoryPath);

                if (files.Length > 0)
                {
                    var fileName = Path.GetFileName(files[0]);
                    return Path.Combine(directoryPath, fileName);
                }
            }
            return string.Empty;
        }
    }
}