using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search;
using NRC.Const.CodesAPI.Application.Interfaces;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/codeccrs/")]
    [ApiController]
    //[Authorize]
    [ApiVersion(1)]
    public class CodeCCRsController : ControllerBase
    {
        private readonly ICodesCCRService _ccrService;
        public CodeCCRsController(
           ICodesCCRService ccrService)
        {
            _ccrService = ccrService ?? throw new ArgumentNullException(nameof(ccrService));
        }

        // -------------------------------------------------
        // GET: api/v1/codeccrs?page=1&size=10
        // -------------------------------------------------
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<PagedResult<GetCodesCCRs_Result>>> GetPagedCodesCCRs(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await _ccrService.GetPagedCodesCCRsAsync(page, size);
            return Ok(result);
        }

        // -------------------------------------------------
        // GET: api/v1/codeccrs/{id}
        // -------------------------------------------------
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        public async Task<ActionResult<CcrDetailDto>> GetCodesCCR(int id)
        {
            var result = await _ccrService.GetCodesCCRByIdAsync(id);

            if (result == null)
                return NotFound($"Codes CCR with ID {id} not found.");

            return Ok(result);
        }

        // -------------------------------------------------
        // PUT: api/v1/codeccrs
        // -------------------------------------------------
        [HttpPut(Name = "UpdateCodesCCR")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCodesCCR([FromBody] CodesCCRUpdateRequest request)
        {
            try
            {
                await _ccrService.UpdateCCRAsync(request, User);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        // -------------------------------------------------
        // POST: api/v1/codeccrs/advancesearch
        // -------------------------------------------------
        [HttpPost("advancesearch")]
        public async Task<ActionResult<SearchResult<GetCodesCCRs_Result>>> AdvanceSearch(
            [FromBody] SearchRequest request)
        {
            var result = await _ccrService.AdvanceSearchAsync(request);
            return Ok(result);
        }

        // -------------------------------------------------
        // GET: api/v1/codeccrs/search
        // -------------------------------------------------
        [HttpGet("search")]
        public async Task<ActionResult<PagedResult<GetCodesCCRs_Result>>> Search(
            [FromQuery] CcrSearchQuery query)
        {
            var result = await _ccrService.SearchAsync(query);
            return Ok(result);
        }

        // -------------------------------------------------
        // POST: api/v1/codeccrs/{ccrId}/status
        // -------------------------------------------------
        [HttpPost("{ccrId:int}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStatus(
            int ccrId,
            [FromBody] ChangeCcrStatusRequest request)
        {
            try
            {
                await _ccrService.ChangeStatusAsync(ccrId, request);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // -------------------------------------------------
        // POST: api/v1/codeccrs
        // -------------------------------------------------
        [HttpPost]
        [ProducesResponseType(typeof(CodesCCRCreateResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CodesCCRCreateResponse>> CreateCodesCCR(
            [FromBody] CodesCCRCreateRequest request)
        {
            var result = await _ccrService.CreateCCRAsync(request);

            return CreatedAtAction(
                nameof(GetCodesCCR),
                new { id = result.CCRId, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1" },
                result
            );
        }

        // -------------------------------------------------
        // GET: api/v1/codeccrs/{ccrId}/status
        // -------------------------------------------------
        [HttpGet("{ccrId:int}/status")]
        [ProducesResponseType(typeof(IEnumerable<CcrStatusDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CcrStatusDto>>> GetStatuses(int ccrId)
        {
            try
            {
                var statuses = await _ccrService.GetStatusByCCRIdAsync(ccrId);
                return Ok(statuses);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}

