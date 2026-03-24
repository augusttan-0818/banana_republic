using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesPRCommentPCAs;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodesPRCommentPCARepository
    {
        Task<IEnumerable<GetCodesPRCommentPCA_Result>> GetCodesPRCommentPCAsAsync();

        Task<GetCodesPRCommentPCA_Result?> GetCodesPRCommentPCAByIdAsync(int id);
    }
}

