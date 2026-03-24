using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesPRCommentPCAs;
using NRC.Const.CodesAPI.Domain.Entities.CodesPRCommentPCAs;
namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesPRCommentPCAProfile : Profile
    {
        public CodesPRCommentPCAProfile()
        {
            CreateMap<CodesPRCommentPCA, GetCodesPRCommentPCA_Result>();
        }
    }
}