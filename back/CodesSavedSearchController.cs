using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NRC.Const.CodesAPI.API.Auth;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesSavedSearches;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesSavedSearches;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/savedsearch/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesSavedSearchController : ControllerBase
    {
        private readonly ICodesSavedSearchRepository _savedSearchRepository;
        public CodesSavedSearchController(ICodesSavedSearchRepository savedSearchRepository)
        {
            _savedSearchRepository = savedSearchRepository ?? throw new ArgumentNullException(nameof(savedSearchRepository));
        }

        [HttpGet]
        public async Task<ActionResult<GetCodesSavedSearch_Result>> GetSavedSearches([FromQuery]string userName, [FromQuery]string context)
        {
            try {
                var result = await _savedSearchRepository.GetCodesSavedSearchesAsync(userName, context);
                return Ok(result);
            }
            catch(Exception e) {
                return StatusCode(500, new {message = e.Message});
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetCodesSavedSearch_Result>> GetSavedSearch(int id)
        {
            var savedSearch = await _savedSearchRepository.GetCodesSavedSearchByIdAsync(id);

            if (savedSearch == null)
            {
                return NotFound($"Saved search with ID {id} not found.");
            }

            return Ok(savedSearch);
        }

        [HttpPut("update")]
        [Produces("application/json")]
        public async Task<ActionResult<GetCodesSavedSearch_Result>> UpdateSavedSearch([FromBody]CodesSavedSearchUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Update Request data is required.");
            }
            var savedSearch = await _savedSearchRepository.UpdateCodesSavedSearchAsync(request);
            return Ok(savedSearch);
        }

        [HttpPost("create")]
        [Produces("application/json")]
        public async Task<ActionResult<GetCodesSavedSearch_Result>> CreateSavedSearch([FromBody]CodesSavedSearchCreateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Update Request data is required.");
            }
            var savedSearch = await _savedSearchRepository.CreateSavedSearchAsync(request);
            return Ok(savedSearch);
        }
    }
}