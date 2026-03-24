using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;

namespace NRC.Const.CodesAPI.Application.Services
{
    public class CcrSearchService : ICcrSearchService
    {
        private readonly ICodesCCRsRepository _repository;

        public CcrSearchService(ICodesCCRsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<SearchResult<CodesCCR>> AdvanceSearchAsync(SearchRequest request)
        {
            return await _repository.AdvanceSearchAsync(request);
        }

        public async Task<PagedResult<CodesCCR>> SearchAsync(CcrSearchQuery query)
        {
            return await _repository.SearchAsync(query);
        }
    }
}

