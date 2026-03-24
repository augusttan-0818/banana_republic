using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodeResources;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodesResources;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesResources;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesResources;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Resources;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesResourcesProfile : Profile
    {
        public CodesResourcesProfile()
        {
            CreateMap<CodesResource, GetCodesResourcesResponse>();
            CreateMap<GetCodesResourcesResponse, CodesResource>();
            CreateMap<CreateCodesResourceRequest, CodesResource>();
            CreateMap<CodesResource, CreateCodesResourceRequest>();
            CreateMap<GetCodesResources_Result, CodesResource>();
            CreateMap<CodesResource, GetCodesResources_Result>();
            CreateMap<CodesResourceUpdateRequest, CodesResource>();
            CreateMap<CodesResource, CodesResourceUpdateRequest>();
            CreateMap<CodesResourceCreateRequest, CodesResource>();
            CreateMap<CodesResource, CodesResourceCreateRequest>();
            CreateMap<GetCodesResourcesResponse, GetCodesResources_Result>();
            CreateMap<GetCodesResources_Result, GetCodesResourcesResponse>();
            CreateMap<CodesResource, UpdateCodesResourceRequest>();
            CreateMap<UpdateCodesResourceRequest, CodesResource>();
            CreateMap<GetCodesResources_Result, CodesResource>();
            CreateMap<CodesResource, CreateCodesResource_Result>();
            CreateMap<CodesResource, CodesResourceCreateRequest>();
            CreateMap<CodesResourceCreateRequest, CodesResource>();
            CreateMap<CodesResource, CodesResourceWithoutUser>();
            CreateMap<CodesResourceWithoutUser, CodesResource>();
            CreateMap<GetCodesResourcesResponseWithoutUser, CodesResource>();
            CreateMap<CodesResource, GetCodesResourcesResponseWithoutUser>();
            CreateMap<CodesResourceWithoutUser, GetCodesResourcesResponseWithoutUser>();
            CreateMap<GetCodesResourcesResponseWithoutUser, CodesResourceWithoutUser>();
            CreateMap<CodesResource, CodesResourceDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User!.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User!.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User!.Email));
        }

    }
}

