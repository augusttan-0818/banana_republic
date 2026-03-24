
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesAuditLogs;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface IAuditLogService
    {
        Task<AuditLogPagedResult> GetAuditHistoryAsync(
        string tableName,
        int recordId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
    }
}
