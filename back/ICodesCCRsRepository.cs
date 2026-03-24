using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodesCCRsRepository
    {
        Task<IEnumerable<CodesCCR>> GetCodesCCRsAsync();
        Task<PagedResult<CodesCCR>> GetPagedCodesCCRsAsync(int pageNumber, int pageSize);
        Task<CodesCCR?> GetCodesCCRByIdAsync(int id);
        Task<CodesCCR> CreateCodeCCRsAsync(CodesCCR reqCodeCCRs);
        Task UpdateCodeCCRsAsync(CodesCCR reqCodeCCRs);
        Task DeleteCodesCCRAsync(int id);
        Task<SearchResult<CodesCCR>> AdvanceSearchAsync(SearchRequest request);
        Task<PagedResult<CodesCCR>> SearchAsync(CcrSearchQuery query);
        Task<CodesCCR?> GetWithHistoryAsync(int id);
        Task SaveAsync();
        Task<CodesCCR?> GetForStatusUpdateAsync(int ccrId);

        Task AddStatusHistoryAsync(CodesCCRStatusHistory history);
        Task<CodesCCRStatus?> GetStatusByIdAsync(short statusId);
        Task<IEnumerable<CodesCCRStatusHistory>> GetStatusByCCRIdAsync(int ccrId);
        Task<CodesCCR> CreateCCRAsync(CodesCCR ccr);

        #region Code cycles
        Task<IEnumerable<CodesCycle>> GetCodesCycles();
        Task<CodesCycle?> GetCodesCycle(short id);
        #endregion


    }
}
