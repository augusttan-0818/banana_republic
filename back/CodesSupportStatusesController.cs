using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NRC.Const.CodesAPI.API.Auth;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.SupportStatuses;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/supportstatus/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesCommentSupportStatusesController(ICodesPRCommentSupportStatusRepository supportStatusRepository) : ControllerBase
    {
        private readonly ICodesPRCommentSupportStatusRepository _supportStatusRepository = supportStatusRepository ?? throw new ArgumentNullException(nameof(supportStatusRepository));

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetSupportStatuses_Result>>> GetSupportStatuses()
        {
            var supportStatuses = await _supportStatusRepository.GetSupportStatusesAsync();
            return Ok(supportStatuses);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetSupportStatuses_Result>> GetSupportStatus(int id)
        {
            var supportStatus = await _supportStatusRepository.GetSupportStatusesByIdAsync(id);

            if (supportStatus == null)
            {
                return NotFound($"Support Status with ID {id} not found.");
            }
            return Ok(supportStatus);
        }

    }
}