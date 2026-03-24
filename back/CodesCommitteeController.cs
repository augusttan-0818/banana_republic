using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Committees;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Committees;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Committees;
using Microsoft.AspNetCore.Authorization;
using NRC.Const.CodesAPI.API.Auth;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/committee/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesCommitteeController(ICommitteeRepository committeeRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICommitteeRepository _committeeRepository = committeeRepository ?? throw new ArgumentNullException(nameof(committeeRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetCommittees_Response>>> GetCommittees()
        {
            var committeesEntities = await _committeeRepository.GetCommitteesAsync();
            return Ok(_mapper.Map<IEnumerable<GetCommittees_Response>>(committeesEntities));
       
        }

        [HttpGet("admin")]
        [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Admin + "," + AuthorizationPolicyRoles.SystemAdmin)]
        public ActionResult<string> GetAdminMessage()
        {
            return Ok("Here goes the secret admin message");

        }


        [HttpGet("type/{committeeType}")]
        [Produces("application/json")]

        public async Task<ActionResult<IEnumerable<GetCommittees_Response>>> GetCommitteesByType(byte committeeType)
        {
           var committees = await _committeeRepository.GetCommitteesByTypeAsync(committeeType);

            if (committees == null )
            {
                return NotFound($"No committees found for CommitteeType {committeeType}.");
            }

            return Ok(_mapper.Map <IEnumerable<GetCommittees_Response>>(committees));
        }


        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetSingleCommittee_Response>> GetCommittee(int id)
        {
            var committee = await _committeeRepository.GetCommitteeByIdAsync(id);

            if (committee == null)
            {
                return NotFound($"Committee with ID {id} not found.");
            }

            return Ok(_mapper.Map<GetSingleCommittee_Response> (committee));
        }


        [HttpGet("committeesupport/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CodesCommitteeSupportResourceMinimal>>> GetCommitteeSupportResources(int id)
        {
            var committees = await _committeeRepository.GetCommitteeDetailedResources(id);

            if (committees == null)
            {
                return NotFound($"Committee support resources for committee with ID {id} not found.");
            }

            return Ok(committees);
        }

        [HttpGet("all-parent-committees")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetParentCommittees_Response>>> GetParentCommittees()
        {
            var committees = await _committeeRepository.GetParentCommittees();
            if (committees == null)
            {
                return NotFound($"Parent committees not found.");
            }

            return Ok(_mapper.Map < IEnumerable<GetParentCommittees_Response>> (committees));
        }

        [HttpGet("parent-committees")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetParentCommittees_Response>>> GetExistingParentCommittees()
        {
            var committees = await _committeeRepository.GetExistingParentCommittees();
            if (committees == null)
            {
                return NotFound($"Parent committees not found.");
            }

            return Ok(_mapper.Map<IEnumerable< GetParentCommittees_Response>>(committees));
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CreateCommittee_Response>> CreateCommittee([FromBody] CreateCommittee_Request newCommittee)
        {
            if (newCommittee == null)
            {
                return BadRequest("Committee data is required.");
            }

       
            var committee = await _committeeRepository.CreateCommitteeAsync(_mapper.Map<CommitteeCreateRequest>(newCommittee));

            return CreatedAtAction(nameof(GetCommittee), new { id = committee.CommitteeId }, _mapper.Map<CreateCommittee_Response>(committee));
        }





        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateCommittee_Response>> PutCommittee(int id, UpdateCommittee_Request committee)
        {
            if (id != committee.CommitteeId)
            {
                return BadRequest();
            }

            try
            {
               
                var getcommitteeupdateresult = await _committeeRepository.UpdateCommitteeAsync(_mapper.Map<CommitteeUpdateRequest>(committee));
                return Ok(_mapper.Map<UpdateCommittee_Response>(getcommitteeupdateresult));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommittee(int id)
        {
            var committee = await _committeeRepository.GetMinimalCommitteeByIdAsync(id);
            if (committee == null)
            {
                return NotFound();
            }

            await _committeeRepository.DeleteCommitteeAsync(id);

            return NoContent();
        }
    }
}

