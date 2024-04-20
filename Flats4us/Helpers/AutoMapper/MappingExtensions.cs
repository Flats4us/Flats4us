using AutoMapper;
using Flats4us.Entities.Dto;
using Flats4us.Entities;


namespace Flats4us.Helpers.AutoMapper
{
    public static class MappingExtensions
    {
        public static IMappingExpression<TSrc, TDest> MapBaseUserProfile<TSrc, TDest>(this IMappingExpression<TSrc, TDest> mapping)
            where TSrc : OwnerStudent
            where TDest : BaseUserProfileDto
        {
            return mapping
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom<UserProfilePictureUrlResolver>())
                .ForMember(dest => dest.AvgRating, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Select(x => x.Rating).DefaultIfEmpty().Average()))
                .ForMember(dest => dest.SumHelpful, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.Helpful).Count()))
                .ForMember(dest => dest.SumCooperative, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.Cooperative).Count()))
                .ForMember(dest => dest.SumTidy, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.Tidy).Count()))
                .ForMember(dest => dest.SumFriendly, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.Friendly).Count()))
                .ForMember(dest => dest.SumRespectingPrivacy, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.RespectingPrivacy).Count()))
                .ForMember(dest => dest.SumCommunicative, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.Communicative).Count()))
                .ForMember(dest => dest.SumUnfair, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.Unfair).Count()))
                .ForMember(dest => dest.SumLackOfHygiene, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.LackOfHygiene).Count()))
                .ForMember(dest => dest.SumUntidy, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.Untidy).Count()))
                .ForMember(dest => dest.SumConflicting, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.Conflicting).Count()))
                .ForMember(dest => dest.SumNoisy, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.Noisy).Count()))
                .ForMember(dest => dest.SumNotFollowingTheArrangements, opt => opt.MapFrom(src => src.ReceivedUserOpinions.Where(x => x.NotFollowingTheArrangements).Count()))
                .ForMember(dest => dest.UserOpinions, opt => opt.MapFrom(src => src.ReceivedUserOpinions));
        }

        public static IMappingExpression<TSrc, PropertyForVerificationDto> MapBasePropertyForVerification<TSrc>(this IMappingExpression<TSrc, PropertyForVerificationDto> mapping)
            where TSrc : Property
        {
            return mapping
                .ForMember(dest => dest.Images, opt => opt.MapFrom<PropertyImagesUrlResolver>())
                .ForMember(dest => dest.Document, opt => opt.MapFrom<PropertyDocumentUrlResolver>())
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => $"{src.Owner.Name} {src.Owner.Surname}"))
                .ForMember(dest => dest.OwnerEmail, opt => opt.MapFrom(src => src.Owner.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => (src.Flat != null) ?
                    $"{src.Street} {src.Number}/{src.Flat}, {src.PostalCode} {src.City}" :
                    $"{src.Street} {src.Number}, {src.PostalCode} {src.City}"));
        }
    }
}
