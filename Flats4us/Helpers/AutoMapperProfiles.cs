using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.AutoMapperResolvers;
using Flats4us.Helpers.Enums;

namespace Flats4us.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Equipment, EquipmentDto>();

            CreateMap<Flat, PropertyDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.Flat))
                .ForMember(dest => dest.Images, opt => opt.MapFrom<PropertyImagesUrlResolver>());

            CreateMap<Room, PropertyDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.Room))
                .ForMember(dest => dest.Images, opt => opt.MapFrom<PropertyImagesUrlResolver>());

            CreateMap<House, PropertyDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.House))
                .ForMember(dest => dest.Images, opt => opt.MapFrom<PropertyImagesUrlResolver>());

            CreateMap<Flat, PropertyForVerificationDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.Flat))
                .ForMember(dest => dest.Images, opt => opt.MapFrom<PropertyImagesUrlResolver>())
                .ForMember(dest => dest.Document, opt => opt.MapFrom<PropertyDocumentUrlResolver>())
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => $"{src.Owner.Name} {src.Owner.Surname}"))
                .ForMember(dest => dest.OwnerEmail, opt => opt.MapFrom(src => src.Owner.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => (src.Flat != null) ?
                    $"{src.Street} {src.Number}/{src.Flat}, {src.PostalCode} {src.City}" :
                    $"{src.Street} {src.Number}, {src.PostalCode} {src.City}"));

            CreateMap<Room, PropertyForVerificationDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.Room))
                .ForMember(dest => dest.Images, opt => opt.MapFrom<PropertyImagesUrlResolver>())
                .ForMember(dest => dest.Document, opt => opt.MapFrom<PropertyDocumentUrlResolver>())
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => $"{src.Owner.Name} {src.Owner.Surname}"))
                .ForMember(dest => dest.OwnerEmail, opt => opt.MapFrom(src => src.Owner.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => (src.Flat != null) ?
                    $"{src.Street} {src.Number}/{src.Flat}, {src.PostalCode} {src.City}" :
                    $"{src.Street} {src.Number}, {src.PostalCode} {src.City}"));

            CreateMap<House, PropertyForVerificationDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.House))
                .ForMember(dest => dest.Images, opt => opt.MapFrom<PropertyImagesUrlResolver>())
                .ForMember(dest => dest.Document, opt => opt.MapFrom<PropertyDocumentUrlResolver>())
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => $"{src.Owner.Name} {src.Owner.Surname}"))
                .ForMember(dest => dest.OwnerEmail, opt => opt.MapFrom(src => src.Owner.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => (src.Flat != null) ?
                    $"{src.Street} {src.Number}/{src.Flat}, {src.PostalCode} {src.City}" :
                    $"{src.Street} {src.Number}, {src.PostalCode} {src.City}"));

            CreateMap<Offer, OfferDto>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Property.Owner))
                .ForMember(dest => dest.IsPromoted, opt => opt.MapFrom(src => src.OfferPromotions.Any(op => op.StartDate <= DateTime.Now && DateTime.Now <= op.EndDate)));

            CreateMap<Owner, OwnerStudentDto>();

            CreateMap<User, UserInfoDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"));

            CreateMap<SurveyOwnerOffer, SurveyOwnerOfferDto>();

            CreateMap<SurveyStudent, SurveyStudentDto>();

            CreateMap<Meeting, MeetingDto>();

            CreateMap<Student, UserForVerificationDto>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Student))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom<UserProfilePictureUrlResolver>())
                .ForMember(dest => dest.Document, opt => opt.MapFrom<UserDocumentUrlResolver>());

            CreateMap<Owner, UserForVerificationDto>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Owner))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom<UserProfilePictureUrlResolver>())
                .ForMember(dest => dest.Document, opt => opt.MapFrom<UserDocumentUrlResolver>());

            CreateMap<Student, UserProfileFullDto>()
               .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Student))
               .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom<UserProfilePictureUrlResolver>())
               .ForMember(dest => dest.Document, opt => opt.MapFrom<UserDocumentUrlResolver>())
               .ForMember(dest => dest.Links, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Links) ? src.Links.Split(new[] { '|' }, StringSplitOptions.None).ToList() : new List<string>()))
               .ForMember(dest => dest.UserOpinions, opt => opt.MapFrom(src => src.ReceivedUserOpinions));

            CreateMap<Owner, UserProfileFullDto>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Owner))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom<UserProfilePictureUrlResolver>())
                .ForMember(dest => dest.Document, opt => opt.MapFrom<UserDocumentUrlResolver>())
                .ForMember(dest => dest.UserOpinions, opt => opt.MapFrom(src => src.ReceivedUserOpinions));

            CreateMap<Student, UserProfilePublicDto>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Student))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.BirthDate.Year))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom<UserProfilePictureUrlResolver>())
                .ForMember(dest => dest.Links, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Links) ? src.Links.Split(new[] { '|' }, StringSplitOptions.None).ToList() : new List<string>()))
                .ForMember(dest => dest.UserOpinions, opt => opt.MapFrom(src => src.ReceivedUserOpinions));

            CreateMap<Owner, UserProfilePublicDto>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Owner))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom<UserProfilePictureUrlResolver>())
                .ForMember(dest => dest.UserOpinions, opt => opt.MapFrom(src => src.ReceivedUserOpinions));

            CreateMap<UserOpinion, UserOpinionDto>()
                .ForMember(dest => dest.SourceUserName, opt => opt.MapFrom(src => src.SourceUser.Name))
                .ForMember(dest => dest.SourceUserProfilePicture, opt => opt.MapFrom<UserProfilePictureUrlResolver>());

            CreateMap<OwnerRegisterDto, Owner>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
                .ForMember(dest => dest.AccountCreationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.DateForVerificationSorting, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => (DateTime?)null))
                .ForMember(dest => dest.ImagesPath, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.ActivityStatus, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.VerificationStatus, opt => opt.MapFrom(src => VerificationStatus.NotVerified));

            CreateMap<StudentRegisterDto, Student>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
                .ForMember(dest => dest.AccountCreationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.DateForVerificationSorting, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => (DateTime?)null))
                .ForMember(dest => dest.ImagesPath, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.ActivityStatus, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.VerificationStatus, opt => opt.MapFrom(src => VerificationStatus.NotVerified))
                .ForMember(dest => dest.IsTenant, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Links, opt => opt.MapFrom(src => src.Links != null ? string.Join("|", src.Links) : string.Empty));

            CreateMap<InterestDto, Interest>();

            CreateMap<Interest, InterestDto>();

            CreateMap<StudentRegisterDto, SurveyStudent>();

            CreateMap<Student, StudentForMatcherDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.BirthDate.Year))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom<UserProfilePictureUrlResolver>());

            CreateMap<TechnicalProblem, TechnicalProblemDto>();
        }
    }
}
