using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesSavedSearches;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;
using AutoMapper;
using NRC.Const.CodesAPI.Domain.Entities.CodesSavedSearches;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesSavedSearches;

namespace NRC.Const.CodesAPI.Infrastructure.Services.Repositories
{

    public class CodesSavedSearchRepository(CodesSavedSearchDbContext context, IMapper mapper) : ICodesSavedSearchRepository 
    {
        private readonly CodesSavedSearchDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<IEnumerable<GetCodesSavedSearch_Result>> GetCodesSavedSearchesAsync(string userName, string context)
        {
           var savedSearches = await _context.CodesSavedSearch.Where(s=> s.Context == "global" || s.CreatedBy == userName && s.Context == context).ToListAsync();
           return _mapper.Map<IEnumerable<GetCodesSavedSearch_Result>>(savedSearches);
        }

        public async Task<GetCodesSavedSearch_Result?> GetCodesSavedSearchByIdAsync(int id)
        {
            var savedSearch = await _context.CodesSavedSearch.FirstOrDefaultAsync(s=> s.SavedSearchId == id);
            if(savedSearch == null) return null;
            
            return _mapper.Map<GetCodesSavedSearch_Result>(savedSearch);
        }

        public async Task <GetCodesSavedSearch_Result?>UpdateCodesSavedSearchAsync(CodesSavedSearchUpdateRequest savedSearchUpdateRequest)
        {
            var existingSavedSearch = await  _context.CodesSavedSearch.FirstOrDefaultAsync(s=> s.SavedSearchId == savedSearchUpdateRequest.SavedSearchId);

            if(existingSavedSearch == null)
            {
                throw new KeyNotFoundException($"Saved Search with id {savedSearchUpdateRequest.SavedSearchId} not found.");
            }

            if (!string.IsNullOrWhiteSpace(savedSearchUpdateRequest.Name))
                existingSavedSearch.Name = savedSearchUpdateRequest.Name;
            if (!string.IsNullOrWhiteSpace(savedSearchUpdateRequest.Visibility))
                existingSavedSearch.Visibility = savedSearchUpdateRequest.Visibility;
                
            existingSavedSearch.UpdatedAt = DateTime.Now;
            
            await _context.SaveChangesAsync();
            return _mapper.Map<GetCodesSavedSearch_Result?>(existingSavedSearch);
        }

        public async Task<GetCodesSavedSearch_Result>CreateSavedSearchAsync(CodesSavedSearchCreateRequest savedSearchCreateRequest)
        {
            CodesSavedSearch savedSearch = _mapper.Map<CodesSavedSearch>(savedSearchCreateRequest);
            _context.CodesSavedSearch.Add(savedSearch);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetCodesSavedSearch_Result>(savedSearch);
        }

        Task ICodesSavedSearchRepository.DeleteSavedSearchAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}