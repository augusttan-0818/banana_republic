using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesCCRStatusProfile : Profile
    {
        public CodesCCRStatusProfile()
        {
            CreateMap<CodesCCRStatus, ChangeCcrStatusRequest>()
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId));
        }
    }
}
