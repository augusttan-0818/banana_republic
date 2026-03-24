using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesPublicReviewCommenters;
using NRC.Const.CodesAPI.Domain.Entities.CodesPublicReviewCommenters;
namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesPublicReviewCommenterProfile : Profile
    {
        public CodesPublicReviewCommenterProfile()
        {
            CreateMap<CodesPublicReviewCommenter, GetCodesPublicReviewCommenter_Result>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}