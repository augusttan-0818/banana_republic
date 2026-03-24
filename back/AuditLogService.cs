using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesAuditLogs;
using NRC.Const.CodesAPI.Application.Interfaces;

namespace NRC.Const.CodesAPI.Application.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _repository;
        private readonly IMapper _mapper;

        public AuditLogService(IAuditLogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AuditLogPagedResult> GetAuditHistoryAsync(
            string tableName,
            int recordId,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var (items, total) = await _repository.GetPagedAsync(
                tableName, recordId, page, pageSize, cancellationToken);

            return _mapper.Map<AuditLogPagedResult>(
                new AuditLogPagedSource(items, total, page, pageSize));
        }
    }
}
