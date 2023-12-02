using AutoMapper;
using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Helpers.AutoMapperResolvers
{
    public class PropertyImagesUrlResolver : IValueResolver<Property, PropertyDto, List<string>>, IValueResolver<Property, PropertyForVerificationDto, List<string>>
    {
        public List<string> Resolve(Property source, PropertyDto destination, List<string> destMember, ResolutionContext context)
        {
            return GetImageUrls(source.ImagesPath);
        }

        public List<string> Resolve(Property source, PropertyForVerificationDto destination, List<string> destMember, ResolutionContext context)
        {
            return GetImageUrls(source.ImagesPath);
        }

        private List<string> GetImageUrls(string directoryId)
        {
            var directoryPath = Path.Combine("Images", "Properties", directoryId, "Images");

            List<string> imageUrls = new List<string>();

            if (Directory.Exists(directoryPath))
            {
                var files = Directory.GetFiles(directoryPath);

                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    imageUrls.Add(Path.Combine(directoryPath, fileName));
                }
            }
            return imageUrls;
        }
    }
}