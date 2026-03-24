using NRC.Const.CodesAPI.Domain.Entities.CodesAuditLogs;
namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<(List<CodesAuditLog> Items, int Total)> GetPagedAsync(
        string tableName,
        int recordId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
    }
}