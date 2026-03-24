using AutoMapper;
using NRC.Const.CodesAPI.Domain.Entities.CodesSavedSearches;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesSavedSearches;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesSavedSearches;
namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesSavedSearchProfile : Profile
    {
        public CodesSavedSearchProfile()
        {    
            CreateMap<CodesSavedSearch, GetCodesSavedSearch_Result>();
            CreateMap<GetCodesSavedSearch_Result, CodesSavedSearch>();

            CreateMap<CodesSavedSearch, CodesSavedSearchCreateRequest>();
            CreateMap<CodesSavedSearchCreateRequest, CodesSavedSearch>()
                .ForMember(d => d.SavedSearchId, o => o.Ignore());

            CreateMap<CodesSavedSearch, CodesSavedSearchUpdateRequest>();
            CreateMap<CodesSavedSearchUpdateRequest, CodesSavedSearch>();

            CreateMap<CodesSavedSearchUpdateRequest, GetCodesSavedSearch_Result>();
            CreateMap<GetCodesSavedSearch_Result, CodesSavedSearchUpdateRequest>();
        }
    }
}