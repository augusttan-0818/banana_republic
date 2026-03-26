using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.ReferenceDocumentUpdate;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search;
using NRC.Const.CodesAPI.Application.Interfaces;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/referencedocumentupdates")]
    [ApiVersion(1)]
    public class ReferenceDocumentUpdateController : ControllerBase
    {
        private readonly IReferenceDocumentUpdateService _service;

        public ReferenceDocumentUpdateController(IReferenceDocumentUpdateService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/v1/referencedocumentupdates/agencies
        [HttpGet("agencies")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<AgencyDto>>> GetAllAgencies()
        {
            var result = await _service.GetAllAgenciesAsync();
            return Ok(result);
        }

        // GET: api/v1/referencedocumentupdates/agencies/{id}
        [HttpGet("agencies/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<AgencyDto>> GetAgencyById(string id)
        {
            var result = await _service.GetAgencyByIdAsync(id);
            if (result == null)
                return NotFound($"Agency with ID {id} not found.");
            return Ok(result);
        }

        // GET: api/v1/referencedocumentupdates/standards
        [HttpGet("standards")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<StandardDto>>> GetAllStandards()
        {
            var result = await _service.GetAllStandardsAsync();
            return Ok(result);
        }

        // GET: api/v1/referencedocumentupdates/standards/{id}
        [HttpGet("standards/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<StandardDto>> GetStandardById(string id)
        {
            var result = await _service.GetStandardByIdAsync(id);
            if (result == null)
                return NotFound($"Standard with ID {id} not found.");
            return Ok(result);
        }

        // GET: api/v1/referencedocumentupdates/standardupdates
        [HttpGet("standardupdates")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<StandardUpdateDto>>> GetAllStandardUpdates()
        {
            var result = await _service.GetAllStandardUpdatesAsync();
            return Ok(result);
        }

        // GET: api/v1/referencedocumentupdates/standardupdates/list
        [HttpGet("standardupdates/list")]
        [Produces("application/json")]
        public async Task<ActionResult<PagedResult<ReferenceDocumentUpdateListDto>>> GetAllStandardUpdatesList(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetPagedStandardUpdatesListAsync(page, pageSize);
            return Ok(result);
        }

        // GET: api/v1/referencedocumentupdates/standardupdates/search
        [HttpGet("standardupdates/search")]
        [Produces("application/json")]
        public async Task<ActionResult<PagedResult<ReferenceDocumentUpdateListDto>>> SearchStandardUpdates(
            [FromQuery] RdUpdateSearchQuery query)
        {
            var result = await _service.SearchStandardUpdatesAsync(query);
            return Ok(result);
        }

        // GET: api/v1/referencedocumentupdates/standardupdates/{id}
        [HttpGet("standardupdates/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<StandardUpdateDto>> GetStandardUpdateById(int id)
        {
            var result = await _service.GetStandardUpdateByIdAsync(id);
            if (result == null)
                return NotFound($"StandardUpdate with ID {id} not found.");
            return Ok(result);
        }

        // GET: api/v1/referencedocumentupdates/statuses
        [HttpGet("statuses")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<StandardUpdateStatusDto>>> GetAllStatuses()
        {
            var result = await _service.GetAllStatusesAsync();
            return Ok(result);
        }

        // GET: api/v1/referencedocumentupdates/statuses/{statusId}/substatuses
        [HttpGet("statuses/{statusId}/substatuses")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<StandardUpdateSubStatusDto>>> GetSubStatusesByStatusId(int statusId)
        {
            var result = await _service.GetSubStatusesByStatusIdAsync(statusId);
            return Ok(result);
        }
    }
}

