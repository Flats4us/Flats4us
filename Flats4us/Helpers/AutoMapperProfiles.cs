using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;

namespace Flats4us.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Equipment, EquipmentDto>();

            CreateMap<Flat, PropertyDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.Flat));

            CreateMap<Room, PropertyDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.Room));

            CreateMap<House, PropertyDto>()
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => PropertyType.House));

            CreateMap<Offer, OfferDto>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Property.Owner));

            CreateMap<Owner, OwnerStudentDto>();

            CreateMap<SurveyOwnerOffer, SurveyOwnerOfferDto>();

            CreateMap<Student, UserForVerificationDto>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Student));

            CreateMap<Owner, UserForVerificationDto>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => UserType.Owner));
        }
    }
}
