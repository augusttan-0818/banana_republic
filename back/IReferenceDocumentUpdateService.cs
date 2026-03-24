using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.ReferenceDocumentUpdate;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface IReferenceDocumentUpdateService
    {
        // Standard Updates
        Task<IEnumerable<StandardUpdateDto>> GetAllStandardUpdatesAsync();
        Task<StandardUpdateDto?> GetStandardUpdateByIdAsync(int id);
        Task<IEnumerable<ReferenceDocumentUpdateListDto>> GetAllStandardUpdatesListAsync();
        Task<PagedResult<ReferenceDocumentUpdateListDto>> GetPagedStandardUpdatesListAsync(int page, int pageSize);

        // Standards
        Task<IEnumerable<StandardDto>> GetAllStandardsAsync();
        Task<StandardDto?> GetStandardByIdAsync(string id);

        // Agencies
        Task<IEnumerable<AgencyDto>> GetAllAgenciesAsync();
        Task<AgencyDto?> GetAgencyByIdAsync(string id);
    }
}

