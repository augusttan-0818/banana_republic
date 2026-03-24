using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesAuditLogs;
using NRC.Const.CodesAPI.Application.Interfaces;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/audits/")]
public class AuditController : ControllerBase
{
    private readonly IAuditLogService _service;

    public AuditController(IAuditLogService service)
    {
        _service = service;
    }

    /// <summary>
    /// GET /api/audit/{tableName}/{recordId}?page=0&amp;pageSize=10
    /// Returns paginated audit history for a specific record.
    /// </summary>
    [HttpGet("{tableName}/{recordId}")]
    public async Task<ActionResult<PagedResult<AuditLogDto>>> GetHistory(
        string tableName,
        int recordId,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        if (pageSize is < 1 or > 100)
            return BadRequest("pageSize must be between 1 and 100.");

        var result = await _service.GetAuditHistoryAsync(
            tableName, recordId, page, pageSize, cancellationToken);

        return Ok(result);
    }
}