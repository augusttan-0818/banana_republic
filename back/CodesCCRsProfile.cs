using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesCCRsProfile : Profile
    {
        public CodesCCRsProfile()
        {
            CreateMap<CodesCCR, CodesCCRCreateRequest>().ReverseMap();
            CreateMap<CodesCCR, CodesCCRCreateResponse>().ReverseMap();

            CreateMap<CodesCCR, GetCodesCCRs_Result>()
                .ForMember(
                    dest => dest.LeadTAName,
                    opt => opt.MapFrom(src =>
                        src.LeadTAResource != null ?
                            $"{src.LeadTAResource!.User.FirstName} {src.LeadTAResource.User.LastName}"
                            : ""
                            ));
            CreateMap<CodesCCRUpdateRequest, CodesCCR>().ReverseMap();
            CreateMap<CodesCCR, CcrDetailDto>()
                .ForMember(dest => dest.ProponentFullName,
                           opt => opt.MapFrom(src => src.CodesCCRProponent.ProponentFullName));
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
            CreateMap<CodesCCRStatusHistory, CcrStatusDto>()
           .ForMember(d => d.StatusName,
               opt => opt.MapFrom(s => s.Status.StatusName))
           .ForMember(dest => dest.ChangedBy,
                opt => opt.MapFrom(src =>
                        src.ChangedBy != null
                ? src.ChangedBy.User != null
                    ? src.ChangedBy.User.FirstName + " " + src.ChangedBy.User.LastName
                    : ""
                : null
        ));
        }
    }
}
