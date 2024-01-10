using AutoMapper;
using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Helpers.AutoMapperResolvers
{
    public class UserDocumentUrlResolver : IValueResolver<OwnerStudent, UserForVerificationDto, FileDto>, IValueResolver<OwnerStudent, UserProfileFullDto, FileDto>
    {
        public FileDto Resolve(OwnerStudent source, UserForVerificationDto destination, FileDto destMember, ResolutionContext context)
        {
            return GetDocumentUrl(source.ImagesPath);
        }

        public FileDto Resolve(OwnerStudent source, UserProfileFullDto destination, FileDto destMember, ResolutionContext context)
        {
            return GetDocumentUrl(source.ImagesPath);
        }

        private FileDto GetDocumentUrl(string directoryId)
        {
            var directoryPath = Path.Combine("Images", "Users", directoryId, "Documents");

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