using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NRC.Const.CodesAPI.API.Auth;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesPRCommentPCAs;
namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/codesprcommentpca/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesPRCommentPCAsController(ICodesPRCommentPCARepository codesPRCommentPCARepository) : ControllerBase
    {
        private readonly ICodesPRCommentPCARepository _codesPRCommentPCARepository = codesPRCommentPCARepository ?? throw new ArgumentNullException(nameof(codesPRCommentPCARepository));

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetCodesPRCommentPCA_Result>>> GetCodesPRCommentPCAs()
        {
            var codesPRCommentPCAs = await _codesPRCommentPCARepository.GetCodesPRCommentPCAsAsync();
            return Ok(codesPRCommentPCAs);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetCodesPRCommentPCA_Result>> GetCodesPRCommentPCA(int id)
        {
            var codesPRCommentPCA = await _codesPRCommentPCARepository.GetCodesPRCommentPCAByIdAsync(id);

            if (codesPRCommentPCA == null)
            {
                return NotFound($"PCA with ID {id} not found.");
            }

            return Ok(codesPRCommentPCA);
        }
    }
}