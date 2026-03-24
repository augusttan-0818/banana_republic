using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesCCRMappingProfile : Profile
    {
        public CodesCCRMappingProfile()
        {
            CreateMap<CodesCCRStatusHistory, StatusHistoryDto>();
        }
    }
}
