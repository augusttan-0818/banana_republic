using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Auth;


namespace NRC.Const.CodesAPI.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public AuthorizationService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<bool> UserHasRolesAsync(long userId, IEnumerable<string> requiredRoles, bool requireAll)
        {
            return await _userRoleRepository.UserHasRolesAsync(userId, requiredRoles, requireAll);
        }
    }

}
