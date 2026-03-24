
using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.PublicReviews;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.PublicReviews;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.PublicReviews;
using NRC.Const.CodesAPI.Domain.Entities.PublicReviews;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesPublicReviewProfile : Profile
    {
        public CodesPublicReviewProfile()
        {
            //create
            CreateMap<CodesPublicReview, PublicReviewCreateRequest>();
            CreateMap<PublicReviewCreateRequest, CodesPublicReview>();

            CreateMap<PublicReviewCreateRequest, CreatePublicReview_Request>();
            CreateMap<CreatePublicReview_Request, PublicReviewCreateRequest>();

            CreateMap<CreatePublicReview_Result, CodesPublicReview>();
            CreateMap<CodesPublicReview, CreatePublicReview_Result>();

            CreateMap<CreatePublicReview_Response, CreatePublicReview_Result>();
            CreateMap<CreatePublicReview_Result, CreatePublicReview_Response>();

            CreateMap<PublicReviewCreateRequest, CreatePublicReview_Result>();

            //get public review
            CreateMap<GetPublicReviews_Response, GetPublicReviews_Result>(); 
            CreateMap<GetPublicReviews_Result, GetPublicReviews_Response>();

            //get public review phase
            CreateMap<GetPublicReviewsPhase_Response, GetPublicReviewsPhase_Result>(); 
            CreateMap<GetPublicReviewsPhase_Result, GetPublicReviewsPhase_Response>(); 

            //get single public review
            CreateMap<GetSinglePublicReview_Response, GetSinglePublicReview_Result>(); 
            CreateMap<GetSinglePublicReview_Result, GetSinglePublicReview_Response>(); 

            // update public review
            CreateMap<CodesPublicReview, UpdatePublicReview_Result>(); 
            CreateMap<UpdatePublicReview_Result, UpdatePublicReview_Response>(); 
            CreateMap<UpdatePublicReview_Result, CodesPublicReview>(); 
            CreateMap<UpdatePublicReview_Request, PublicReviewUpdateRequest>();

            // delete
            CreateMap<GetSinglePublicReview_Result, CodesPublicReview>();
            CreateMap<CodesPublicReview, GetSinglePublicReview_Result>();


        }
    }
}

