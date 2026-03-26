using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search;
using NRC.Const.CodesAPI.Domain.Entities.ReferenceDocumentUpdate;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface IReferenceDocumentUpdateRepository
    {
        // Standard Updates
        Task<IEnumerable<StandardUpdate>> GetAllStandardUpdatesAsync();
        Task<PagedResult<StandardUpdate>> GetPagedStandardUpdatesAsync(int pageNumber, int pageSize);
        Task<PagedResult<StandardUpdate>> SearchStandardUpdatesAsync(RdUpdateSearchQuery query);
        Task<StandardUpdate?> GetStandardUpdateByIdAsync(int id);

        // Standards
        Task<IEnumerable<Standard>> GetAllStandardsAsync();
        Task<Standard?> GetStandardByIdAsync(string id);

        // Agencies
        Task<IEnumerable<Agency>> GetAllAgenciesAsync();
        Task<Agency?> GetAgencyByIdAsync(string id);

        // Statuses
        Task<IEnumerable<StandardUpdateStatus>> GetAllStatusesAsync();
        Task<IEnumerable<StandardUpdateSubStatus>> GetSubStatusesByStatusIdAsync(int statusId);
    }
}
