using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Meetings;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Meetings;
using Microsoft.AspNetCore.Authorization;
using NRC.Const.CodesAPI.API.Auth;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/meetings/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesMeetingsController(IMeetingsRepository meetingsRepository, IMapper mapper) : ControllerBase
    {
        private readonly IMeetingsRepository _meetingsRepository = meetingsRepository ?? throw new ArgumentNullException(nameof(meetingsRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetMeetings_Response>>> GetMeetings()
        {
            var meetingsEntities = await _meetingsRepository.GetMeetingsAsync();
            return Ok(_mapper.Map<IEnumerable<GetMeetings_Response>>(meetingsEntities));
        }

        [HttpGet("committee/{committee}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetMeetings_Response>>> GetMeetingsByCommittee(int committee)
        {
            var meetingsEntities = await _meetingsRepository.GetMeetingsByCommitteeAsync(committee);
            return Ok(_mapper.Map<IEnumerable<GetMeetings_Response>>(meetingsEntities));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetSingleMeeting_Response>> GetMeeting(int id)
        {
            var meeting = await _meetingsRepository.GetMeetingByIdAsync(id);

            if (meeting == null)
            {
                return NotFound($"Meeting with ID {id} not found.");
            }

            return Ok(_mapper.Map<GetSingleMeeting_Response>(meeting));
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CreateMeetings_Response>> CreateMeeting([FromBody] CreateMeetings_Request newMeeting)
        {
            if (newMeeting == null)
            {
                return BadRequest("Meeting data is required.");
            }


            var meeting = await _meetingsRepository.CreateMeetingAsync(_mapper.Map<MeetingsCreateRequest>(newMeeting));

            return CreatedAtAction(nameof(CreateMeeting), new { id = meeting.CodesMeetingId }, _mapper.Map<CreateMeetings_Response>(meeting));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<UpdateMeetings_Response>> PutMeeting(int id, UpdateMeetings_Request meeting)
        {
            if (id != meeting.CodesMeetingId)
            {
                return BadRequest();
            }

            try
            {
                var getmeetingupdateresult = await _meetingsRepository.UpdateMeetingsAsync(_mapper.Map<MeetingsUpdateRequest>(meeting));
                return Ok(_mapper.Map<UpdateMeetings_Response>(getmeetingupdateresult));
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
            var committee = await _meetingsRepository.GetMeetingByIdAsync(id);
            if (committee == null)
            {
                return NotFound();
            }

            await _meetingsRepository.DeleteMeetingAsync(id);

            return NoContent();
        }
    }
}

