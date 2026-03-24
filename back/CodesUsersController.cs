using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Users;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Users;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using Microsoft.AspNetCore.Authorization;
using NRC.Const.CodesAPI.API.Auth;
using NRC.Const.CodesAPI.Domain.Auth;
namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/codesusers/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesUsersController(IUserRepository userRepository, IUserContextService userContextService, IUserRoleRepository userRoleRepository, IMapper mapper) : ControllerBase
    {
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly IUserContextService _userContextService = userContextService;
        private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;


        // GET: api/codesusers
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetUserResponse>>> GetUsers()
        {
            var usersEntities = await _userRepository.GetUsersAsync();
            return Ok(_mapper.Map<IEnumerable<GetUserResponse>>(usersEntities));
        }

        // POST: api/codesusers
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<GetUserResponse>> CreateUser(CreateUserRequest usr)
        {

            var targetUser = await _userRepository.CreateUserAsync(_mapper.Map<User>(usr));
            return CreatedAtAction(nameof(GetUser), new { id = targetUser.UserId }, targetUser);
        }

        // GET: api/codesusers/{id}
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetUserResponse>> GetUser(int id)
        {
            var userEntity = await _userRepository.GetUserByIdAsync(id);

            if (userEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<GetUserResponse>(userEntity);
        }

        // PUT: api/codesresources/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<GetUserResponse>> UpdateUser(int id, UpdateUserRequest usr)
        {
            if (id != usr.UserId)
            {
                return BadRequest("User ID mismatch.");
            }


            try
            {
                var targetEntity = _mapper.Map<User>(usr);
                var getuserresult = await _userRepository.UpdateUserAsync(_mapper.Map<UserUpdateRequest>(targetEntity));
                return Ok(_mapper.Map<GetUserResponse>(getuserresult));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetCurrentUserRoles()
        {
            var userId = await _userContextService.GetOrCreateCurrentUserIdAsync(User);
            var roles = await _userRoleRepository.GetRoleNamesByUserIdAsync(userId);
            return Ok(new { roles });
        }


        [HttpGet("by-roles")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetUserResponse>>> GetUsersByRoles([FromQuery] List<string> roleNames)
        {
            if (roleNames == null || !roleNames.Any())
            {
                return BadRequest("At least one role name must be provided.");
            }

            var users = await _userRepository.GetUsersByRolesAsync(roleNames);
            return Ok(_mapper.Map<IEnumerable<GetUserResponse>>(users));
        }
    }


}

