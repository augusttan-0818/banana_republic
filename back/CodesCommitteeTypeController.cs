using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.API.Auth;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Committees;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/committeetype/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesCommitteeTypeController(ICommitteeRepository committeeRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICommitteeRepository _committeeRepository = committeeRepository ?? throw new ArgumentNullException(nameof(committeeRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));


        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CodesCommitteeType>>> GetCommitteeTypes()
        {
            var output = await _committeeRepository.GetCommitteeTypes();
            return Ok(output);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<CodesCommitteeType>> GetCommitteeType(byte id)
        {
            var committeeType = await _committeeRepository.GetCommitteesByTypeAsync(id);

            if (committeeType == null)
            {
                return NotFound();
            }

            return Ok(committeeType);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CodesCommitteeType>> CreateCommitteeType(CodesCommitteeType committeeType)
        {
          await _committeeRepository.CreateCommitteeType(committeeType);

            return CreatedAtAction(nameof(GetCommitteeType), new { id = committeeType.CommitteeTypeId }, committeeType);
        }

        
    }
}

