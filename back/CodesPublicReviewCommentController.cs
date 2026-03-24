using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NRC.Const.CodesAPI.API.Auth;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.PublicReviewComments;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.PublicReviewComments;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/publicreviewcomment/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesPublicReviewCommentController : ControllerBase
    {
        private readonly ICodesPublicCommentUnifiedService _publicReviewService;
        public CodesPublicReviewCommentController(
           ICodesPublicCommentUnifiedService ccrService)
        {
            _publicReviewService = ccrService ?? throw new ArgumentNullException(nameof(ccrService));
        }

        [HttpPost("search")]
        public async Task<ActionResult<PagedResult<GetPublicReviewComments_Result>>> Search([FromBody]PublicReviewCommentSearchRequest request, int pageNumber, int pageSize)
        {
            try {
                var result = await _publicReviewService.Search(request, pageNumber, pageSize);
                return Ok(result);
            }
            catch(Exception e) {
                return StatusCode(500, new {message = e.Message});
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetPublicReviewComments_Result>> GetPublicReviewComment(int id)
        {
            var publicreviewComment = await _publicReviewService.GetPublicReviewCommentByIdAsync(id);

            if (publicreviewComment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }

            return Ok(publicreviewComment);
        }

        [HttpPut]
        [Produces("application/json")]
        public async Task<ActionResult<UpdatePublicReviewComment_Result>> UpdateComment([FromBody]PublicReviewCommentUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Update Request data is required.");
            }
            var publicReviewComment = await _publicReviewService.UpdatePublicCommentAsync(request);
            return Ok(publicReviewComment);
        }

    }
}

