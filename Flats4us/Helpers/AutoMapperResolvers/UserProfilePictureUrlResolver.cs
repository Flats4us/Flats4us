using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Helpers.AutoMapperResolvers
{
    public class UserProfilePictureUrlResolver : IValueResolver<OwnerStudent, UserForVerificationDto, List<FileDto>>, IValueResolver<Student, StudentForMatcherDto, List<FileDto>>
    {
        public List<FileDto> Resolve(OwnerStudent source, UserForVerificationDto destination, List<FileDto> destMember, ResolutionContext context)
        {
            return GetProfilePictureUrl(source.ImagesPath);
        }

        public List<FileDto> Resolve(Student source, StudentForMatcherDto destination, List<FileDto> destMember, ResolutionContext context)
        {
            return GetProfilePictureUrl(source.ImagesPath);
        }


        public List<FileDto> GetProfilePictureUrl(string directoryId)
        {
            var directoryPath = Path.Combine("Images", "Users", directoryId, "ProfilePicture");
            
            List<FileDto> imageFiles = new List<FileDto>();

            if (Directory.Exists(directoryPath))
            {
                var files = Directory.GetFiles(directoryPath);

                if (files.Length > 0)
                {
                    var fileNameWithExtension = Path.GetFileName(files[0]);
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(files[0]);
                    imageFiles.Add( new FileDto { Name = fileNameWithoutExtension, Path = Path.Combine(directoryPath, fileNameWithExtension) });
                }
            }
            return imageFiles; 
        }







    }
}