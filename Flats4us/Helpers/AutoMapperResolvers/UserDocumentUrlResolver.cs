using AutoMapper;
using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Helpers.AutoMapperResolvers
{
    public class UserDocumentUrlResolver : IValueResolver<OwnerStudent, UserForVerificationDto, string>
    {
        public string Resolve(OwnerStudent source, UserForVerificationDto destination, string destMember, ResolutionContext context)
        {
            var directoryPath = Path.Combine("Images", "Users", source.ImagesPath, "Documents");

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