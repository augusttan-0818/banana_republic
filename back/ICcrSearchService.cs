using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICcrSearchService
    {
        Task<SearchResult<CodesCCR>> AdvanceSearchAsync(SearchRequest request);
        Task<PagedResult<CodesCCR>> SearchAsync(CcrSearchQuery query);
    }
}
