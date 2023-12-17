using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Helpers.AutoMapperResolvers
{
    public class UserProfilePictureUrlResolver : IValueResolver<OwnerStudent, UserForVerificationDto, string>
    {
        public string Resolve(OwnerStudent source, UserForVerificationDto destination, string destMember, ResolutionContext context)
        {
            var directoryPath = Path.Combine("Images", "Users", source.ImagesPath, "ProfilePicture");

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