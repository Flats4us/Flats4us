using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Helpers.AutoMapper
{
    public class UserProfilePictureUrlResolver : 
        IValueResolver<OwnerStudent, UserForVerificationDto, FileDto>,
        IValueResolver<Student, StudentForMatcherDto, FileDto>,
        IValueResolver<OwnerStudent, BaseUserProfileDto, FileDto>,
        IValueResolver<UserOpinion, UserOpinionDto, FileDto>
    {
        public FileDto Resolve(OwnerStudent source, UserForVerificationDto destination, FileDto destMember, ResolutionContext context)
        {
            return GetProfilePictureUrl(source.ImagesPath);
        }

        public FileDto Resolve(Student source, StudentForMatcherDto destination, FileDto destMember, ResolutionContext context)
        {
            return GetProfilePictureUrl(source.ImagesPath);
        }

        public FileDto Resolve(OwnerStudent source, BaseUserProfileDto destination, FileDto destMember, ResolutionContext context)
        {
            return GetProfilePictureUrl(source.ImagesPath);
        }

        public FileDto Resolve(UserOpinion source, UserOpinionDto destination, FileDto destMember, ResolutionContext context)
        {
            return GetProfilePictureUrl(source.SourceUser.ImagesPath);
        }

        private FileDto GetProfilePictureUrl(string directoryId)
        {
            var directoryPath = Path.Combine("Images", "Users", directoryId, "ProfilePicture");

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