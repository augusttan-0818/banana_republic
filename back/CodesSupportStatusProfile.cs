using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.SupportStatuses;
using NRC.Const.CodesAPI.Domain.Entities.CodesSupportStatuses;
namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesSupportStatusProfile : Profile
    {
        public CodesSupportStatusProfile()
        {
            CreateMap<CodesPRCommentSupportStatus, GetSupportStatuses_Result>();
        }
    }
}