using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.API.Auth;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodeResources;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodesResources;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesResources;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Resources;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/codesresources/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesResourcesController(ICodesResourceService service,IResourcesRepository codesResourcesRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICodesResourceService _service = service ?? throw new ArgumentNullException(nameof(service));
        private readonly IResourcesRepository _codesResourcesRepository = codesResourcesRepository ?? throw new ArgumentNullException(nameof(codesResourcesRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
              

        // POST: api/codesresources
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<GetCodesResourcesResponse>> CreateCodesResource(CreateCodesResourceRequest resource)
        {
           
           var targetResource= await _codesResourcesRepository.CreateCodesResourceAsync(_mapper.Map<CodesResourceCreateRequest>(resource));
            return CreatedAtAction(nameof(GetCodesResource), new { id = targetResource.ResourceId }, targetResource);
        }

        // GET: api/codesresources/{id}
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetCodesResourcesResponse>> GetCodesResource(int id)
        {
           var codesResourceEntity= await _codesResourcesRepository.GetCodesResourceByIdAsync(id);
           
            if (codesResourceEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<GetCodesResourcesResponse>(codesResourceEntity);
        }

        // PUT: api/codesresources/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<GetCodesResourcesResponse>> UpdateCodesResource(int id, UpdateCodesResourceRequest resource)
        {
            if (id != resource.ResourceId)
            {
                return BadRequest("Resource ID mismatch.");
            }
           
                     
            try
            {
                var targetEntity = _mapper.Map<CodesResource>(resource);
                var getcodesresourceresult = await _codesResourcesRepository.UpdateCodesResourceAsync(_mapper.Map<CodesResourceUpdateRequest>(targetEntity));
                return Ok(_mapper.Map<GetCodesResourcesResponse>(getcodesresourceresult));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        // -------------------------------------------------
        // GET: api/v1/codesresources?role=TA
        // -------------------------------------------------
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CodesResourceDto>>> GetCodesResourcesByRoleAsync([FromQuery] List<string> roles)
        {

            if (roles == null || !roles.Any())
            {
                return BadRequest("At least one role name must be provided.");
            }

            var result = await _service.GetCodesResourcesByRoleAsync(roles);
            return Ok(result);
        }
    }

}

