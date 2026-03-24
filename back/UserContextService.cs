using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Auth;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using System.Security.Claims;

namespace NRC.Const.CodesAPI.Application.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IUserRepository _userRepository;
        private readonly IResourcesRepository _resourceRepository;
        public UserContextService(
       IUserRepository userRepository,
       IResourcesRepository resourceRepository)
        {
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task<int?> GetCurrentResourceIdAsync(ClaimsPrincipal user)
        {
            var userId = await GetOrCreateCurrentUserIdAsync(user);

            var resource = await _resourceRepository
                .GetByUserIdAsync(userId);

            return resource?.ResourceId;
        }

         public async Task<string?> GetCurrentResourceEmailAsync(ClaimsPrincipal user)
        {
            var userId = await GetOrCreateCurrentUserIdAsync(user);

            var resource = await _resourceRepository
                .GetByUserIdAsync(userId);

            return resource?.User.Email;
        }

        public async Task<long> GetOrCreateCurrentUserIdAsync(ClaimsPrincipal user)
        {
           var upn = user.FindFirst(ClaimTypes.Upn)?.Value ?? throw new UnauthorizedAccessException();
           var identityProvider = "AAD";

            var existing = await _userRepository.FindByOidAsync(identityProvider, upn);
            if (existing != null) return existing.UserId;

            var newUser = new User
            {
                IdentityProviderUserId = upn,
                IdentityProviderName = identityProvider,
                Email = user.FindFirst(ClaimTypes.Upn)?.Value ?? "",
                FirstName = user.FindFirst(ClaimTypes.GivenName)?.Value ?? "",
                LastName = user.FindFirst(ClaimTypes.Surname)?.Value ?? "",
                IsExternal = false
            };

            return await _userRepository.CreateAsync(newUser);
        }
    }

}

