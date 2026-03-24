using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.PublicReviewComments;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.PublicReviewComments;
using NRC.Const.CodesAPI.Domain.Entities.PublicReviewComments;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesPublicReviewCommentProfile : Profile
    {
        public CodesPublicReviewCommentProfile()
        {
           
            CreateMap<CodesPublicReviewCommentsUnified, GetPublicReviewComments_Result>()
                .ForMember(
                    dest => dest.CommenterName,
                    opt => opt.MapFrom(src =>
                        src.CodesPublicReviewCommenter != null ?
                            $"{src.CodesPublicReviewCommenter!.FirstName} {src.CodesPublicReviewCommenter.LastName}"
                            : ""
                            ))
                .ForMember(
                    dest => dest.Province,
                    opt => opt.MapFrom(src =>
                        src.CodesPublicReviewCommenter != null ?
                          src.CodesPublicReviewCommenter.Province : ""
                            ))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src =>
                        src.PCF != null ?
                            $"{src.PCF.Description}"
                            : "" ))
                 .ForMember(
                    dest => dest.PublicReviewTitle,
                    opt => opt.MapFrom(src =>
                        src.CodesPublicReview != null ?
                            src.CodesPublicReview.PublicReviewTitle
                            : "" ));
            CreateMap<GetPublicReviewComments_Response, GetPublicReviewComments_Result>(); 
            CreateMap<GetPublicReviewComments_Result, GetPublicReviewComments_Response>();
            CreateMap<CodesPublicReviewCommentsUnified, UpdatePublicReviewComment_Result>(); 
            CreateMap<UpdatePublicReviewComment_Result, GetPublicReviewComments_Result>();

           CreateMap<PagedResult<CodesPublicReviewCommentsUnified>, PagedResult<GetPublicReviewComments_Result>>()
                    .ForMember(
                      dest => dest.Items, 
                      opt => opt.MapFrom(src => src.Items));
           
        }
    }
}

