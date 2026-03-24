using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesCycle;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Contact;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<UnifiedContact, ContactDto>();
            CreateMap<UnifiedContact, CodesCCRProponent>().ReverseMap();

        }
    }
}
