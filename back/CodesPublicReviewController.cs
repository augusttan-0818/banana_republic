using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.PublicReviews;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.PublicReviews;
using Microsoft.AspNetCore.Authorization;
using NRC.Const.CodesAPI.API.Auth;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/publicreview/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesPublicReviewController(IPublicReviewRepository publicReviewRepository, IMapper mapper, IPublicReviewService publicReviewService) : ControllerBase
    {
        private readonly IPublicReviewRepository _publicReviewRepository = publicReviewRepository ?? throw new ArgumentNullException(nameof(publicReviewRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly IPublicReviewService _publicReviewService= publicReviewService ?? throw new ArgumentNullException(nameof(_publicReviewService));

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetPublicReviews_Response>>> GetPublicReviews()
        {
            var publicReviewsEntities = await _publicReviewRepository.GetPublicReviewAsync();
            return Ok(_mapper.Map<IEnumerable<GetPublicReviews_Response>>(publicReviewsEntities));

        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetSinglePublicReview_Response>> GetPublicReview(int id)
        {
            var publicreview = await _publicReviewRepository.GetPublicReviewByIdAsync(id);

            if (publicreview == null)
            {
                return NotFound($"Public Review with ID {id} not found.");
            }

            return Ok(_mapper.Map<GetSinglePublicReview_Response>(publicreview));
        }

        [HttpGet("phase")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetPublicReviewsPhase_Response>>> GetPublicReviewPhase()
        {
            var publicReviewsEntities = await _publicReviewRepository.GetPublicReviewPhaseAsync();
            return Ok(_mapper.Map<IEnumerable<GetPublicReviewsPhase_Response>>(publicReviewsEntities));
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CreatePublicReview_Response>> CreatePublicReview([FromBody] CreatePublicReview_Request newPublicReview)
        {
            if (newPublicReview == null)
            {
                return BadRequest("Public Review data is required.");
            }
            try
            {
                var publicreview = await _publicReviewRepository.CreatePublicReviewAsync(_mapper.Map<PublicReviewCreateRequest>(newPublicReview));
                return CreatedAtAction(nameof(GetPublicReview), new { id = publicreview.PublicReviewId }, _mapper.Map<CreatePublicReview_Response>(publicreview));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<UpdatePublicReview_Response>> PutPublicReview(int id, UpdatePublicReview_Request publicreview)
        {
            if (id != publicreview.PublicReviewId)
            {
                return BadRequest();
            }

            try
            {

                var getpublicreviewupdateresult = await _publicReviewService.UpdatePublicReviewAsync(_mapper.Map<PublicReviewUpdateRequest>(publicreview));
                return Ok(_mapper.Map<UpdatePublicReview_Response>(getpublicreviewupdateresult));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", detail = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            var committee = await _publicReviewRepository.GetPublicReviewByIdAsync(id);
            if (committee == null)
            {
                return NotFound();
            }

            await _publicReviewRepository.DeletePublicReviewAsync(id);

            return NoContent();
        }
    }
}

