using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.SupportStatuses;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodesPRCommentSupportStatusRepository
    {
        Task<IEnumerable<GetSupportStatuses_Result>> GetSupportStatusesAsync();

        Task<GetSupportStatuses_Result?> GetSupportStatusesByIdAsync(int id);
    }
}