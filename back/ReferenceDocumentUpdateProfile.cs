using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.ReferenceDocumentUpdate;
using NRC.Const.CodesAPI.Domain.Entities.ReferenceDocumentUpdate;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class ReferenceDocumentUpdateProfile : Profile
    {
        public ReferenceDocumentUpdateProfile()
        {
            // Agency mappings
            CreateMap<Agency, AgencyDto>().ReverseMap();

            // Standard mappings
            CreateMap<Standard, StandardDto>()
                .ForMember(dest => dest.Agency, opt => opt.MapFrom(src => src.Agency))
                .ReverseMap();

            // StandardUpdate mappings
            CreateMap<StandardUpdate, StandardUpdateDto>()
                .ForMember(dest => dest.Standard, opt => opt.MapFrom(src => src.Standard))
                .ReverseMap();

            // StandardUpdate ListDto mapping
            CreateMap<StandardUpdate, ReferenceDocumentUpdateListDto>()
                .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Standard != null && src.Standard.Agency != null ? src.Standard.Agency.Name : string.Empty))
                .ForMember(dest => dest.ReferencedIn, opt => opt.MapFrom(src => string.Empty)); // Empty for now

            // Status mappings
            CreateMap<StandardUpdateStatus, StandardUpdateStatusDto>();
            CreateMap<StandardUpdateSubStatus, StandardUpdateSubStatusDto>();
        }
    }
}

