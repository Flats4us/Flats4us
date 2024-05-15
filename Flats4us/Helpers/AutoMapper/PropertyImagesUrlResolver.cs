using AutoMapper;
using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Helpers.AutoMapper
{
    public class PropertyImagesUrlResolver :
        IValueResolver<Property, PropertyDto, List<FileDto>>,
        IValueResolver<Property, PropertyForVerificationDto, List<FileDto>>,
        IValueResolver<Rent, RentDto, List<FileDto>>
    {
        public List<FileDto> Resolve(Property source, PropertyDto destination, List<FileDto> destMember, ResolutionContext context)
        {
            return GetImageUrls(source.ImagesPath);
        }

        public List<FileDto> Resolve(Property source, PropertyForVerificationDto destination, List<FileDto> destMember, ResolutionContext context)
        {
            return GetImageUrls(source.ImagesPath);
        }

        public List<FileDto> Resolve(Rent source, RentDto destination, List<FileDto> destMember, ResolutionContext context)
        {
            return GetImageUrls(source.Offer.Property.ImagesPath);
        }

        private List<FileDto> GetImageUrls(string directoryId)
        {
            var directoryPath = Path.Combine("Images", "Properties", directoryId, "Images");

            List<FileDto> imageFiles = new List<FileDto>();

            if (Directory.Exists(directoryPath))
            {
                var files = Directory.GetFiles(directoryPath);

                foreach (var file in files)
                {
                    var fileNameWithExtension = Path.GetFileName(file);
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                    imageFiles.Add(new FileDto { Name = fileNameWithoutExtension, Path = Path.Combine(directoryPath, fileNameWithExtension) });
                }
            }
            return imageFiles;
        }
    }
}