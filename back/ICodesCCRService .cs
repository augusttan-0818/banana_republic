using System.Security.Claims;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
   
    public interface ICodesCCRService
    {
        Task<PagedResult<GetCodesCCRs_Result>> GetPagedCodesCCRsAsync(int page, int size);
        Task<CcrDetailDto?> GetCodesCCRByIdAsync(int id);
        Task UpdateCCRAsync(CodesCCRUpdateRequest request, ClaimsPrincipal user);
        Task ChangeStatusAsync(int ccrId, ChangeCcrStatusRequest request);
        Task<IEnumerable<CcrStatusDto>> GetStatusByCCRIdAsync(int ccrId);   
        Task<SearchResult<GetCodesCCRs_Result>> AdvanceSearchAsync(SearchRequest request);
        Task<PagedResult<GetCodesCCRs_Result>> SearchAsync(CcrSearchQuery query);
        Task<CodesCCRCreateResponse> CreateCCRAsync(CodesCCRCreateRequest request);
        
    }
}
