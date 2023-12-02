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
                .ForMember(dest => dest.ImagesURLs, opt => opt.MapFrom<PropertyImagesUrlResolver>());

            CreateMap<Room, PropertyDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.Room))
                .ForMember(dest => dest.ImagesURLs, opt => opt.MapFrom<PropertyImagesUrlResolver>());

            CreateMap<House, PropertyDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.House))
                .ForMember(dest => dest.ImagesURLs, opt => opt.MapFrom<PropertyImagesUrlResolver>());

            CreateMap<Flat, PropertyForVerificationDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.Flat))
                .ForMember(dest => dest.ImagesURLs, opt => opt.MapFrom<PropertyImagesUrlResolver>())
                .ForMember(dest => dest.DocumentURL, opt => opt.MapFrom<PropertyDocumentUrlResolver>());

            CreateMap<Room, PropertyForVerificationDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.Room))
                .ForMember(dest => dest.ImagesURLs, opt => opt.MapFrom<PropertyImagesUrlResolver>())
                .ForMember(dest => dest.DocumentURL, opt => opt.MapFrom<PropertyDocumentUrlResolver>());

            CreateMap<House, PropertyForVerificationDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.House))
                .ForMember(dest => dest.ImagesURLs, opt => opt.MapFrom<PropertyImagesUrlResolver>())
                .ForMember(dest => dest.DocumentURL, opt => opt.MapFrom<PropertyDocumentUrlResolver>());

            CreateMap<Offer, OfferDto>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Property.Owner));

            CreateMap<Owner, OwnerStudentDto>();

            CreateMap<SurveyOwnerOffer, SurveyOwnerOfferDto>();

            CreateMap<Student, UserForVerificationDto>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Student))
                .ForMember(dest => dest.ProfilePictureURL, opt => opt.MapFrom<UserProfilePictureUrlResolver>())
                .ForMember(dest => dest.DocumentURL, opt => opt.MapFrom<UserDocumentUrlResolver>());

            CreateMap<Owner, UserForVerificationDto>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Owner))
                .ForMember(dest => dest.ProfilePictureURL, opt => opt.MapFrom<UserProfilePictureUrlResolver>())
                .ForMember(dest => dest.DocumentURL, opt => opt.MapFrom<UserDocumentUrlResolver>());
        }
    }
}
