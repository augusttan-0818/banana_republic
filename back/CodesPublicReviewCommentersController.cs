using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NRC.Const.CodesAPI.API.Auth;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesPublicReviewCommenters;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesPublicReviewCommenters;
namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/codespublicreviewcommenter/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesPublicReviewCommentersController(ICodesPublicReviewCommenterRepository codesPublicReviewCommenterRepository) : ControllerBase
    {
        private readonly ICodesPublicReviewCommenterRepository _codesPublicReviewCommenterRepository = codesPublicReviewCommenterRepository ?? throw new ArgumentNullException(nameof(codesPublicReviewCommenterRepository));

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetCodesPublicReviewCommenter_Result>>> GetCodesPublicReviewCommenters()
        {
            var codesPublicReviewCommenters = await _codesPublicReviewCommenterRepository.GetCodesPublicReviewCommentersAsync();
            return Ok(codesPublicReviewCommenters);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetCodesPublicReviewCommenter_Result>> GetExternalPortalUnifiedContact(int id)
        {
            var codesPublicReviewCommenter = await _codesPublicReviewCommenterRepository.GetCodesPublicReviewCommenterByIdAsync(id);

            if (codesPublicReviewCommenter == null)
            {
                return NotFound($"Public Review with ID {id} not found.");
            }
            return Ok(codesPublicReviewCommenter);
        }
    }
}