using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesSavedSearches;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesSavedSearches;
namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodesSavedSearchRepository
    {
        Task<IEnumerable<GetCodesSavedSearch_Result>> GetCodesSavedSearchesAsync(string userName, string context);
        Task<GetCodesSavedSearch_Result?> GetCodesSavedSearchByIdAsync(int id);
        Task <GetCodesSavedSearch_Result?>UpdateCodesSavedSearchAsync(CodesSavedSearchUpdateRequest savedSearchUpdateRequest);
        Task <GetCodesSavedSearch_Result>CreateSavedSearchAsync(CodesSavedSearchCreateRequest savedSearchCreateRequest);
        Task DeleteSavedSearchAsync(int id);
    }
}

