using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Resources;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesResources;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Users;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesResources;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Users;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Infrastructure.Services.Repositories
{
    public class ResourcesRepository(UserResourceDbContext context, IMapper mapper) : IResourcesRepository
    {
        private readonly UserResourceDbContext _context =
        context ?? throw new ArgumentNullException(nameof(context));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        public async Task<CreateCodesResource_Result> CreateCodesResourceAsync(CodesResourceCreateRequest resource)
        {
            _context.CodesResources.Add(_mapper.Map<CodesResource>(resource)); // Add the entity to the DbContext
            await _context.SaveChangesAsync(); // Persist changes to the database
            return _mapper.Map<CreateCodesResource_Result>(resource);

        }

        public async Task<GetCodesResources_Result?> GetCodesResourceByIdAsync(int resourceId)
        {
            var codesResourcesEntity = await _context.CodesResources.Include(r => r.User).FirstOrDefaultAsync(r => r.ResourceId == resourceId);
            return _mapper.Map<GetCodesResources_Result>(codesResourcesEntity);
        }

        public async Task<IEnumerable<GetCodesResources_Result>> GetCodesResourcesAsync()
        {
            var codesResourcesEntities = await _context.CodesResources.Include(r => r.User).ToListAsync();
            return _mapper.Map<IEnumerable<GetCodesResources_Result>>(codesResourcesEntities);
        }

        public async Task DeleteCodesResourceAsync(int resourceId)
        {
            var resource = await GetCodesResourceByIdAsync(resourceId);
            if (resource != null)
            {
                var targetResource = _mapper.Map<CodesResource>(resource);
                _context.CodesResources.Remove(targetResource);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CodesResource> UpdateCodesResourceAsync(CodesResourceUpdateRequest resource)
        {
            var existingResource = await _context.CodesResources
                                         .FirstOrDefaultAsync(r => r.ResourceId == resource.ResourceId);
            if (existingResource == null)
            {
                throw new KeyNotFoundException($"Resource with ID {resource.ResourceId} not found.");
            }
            existingResource.ResourceUserId = resource.ResourceUserId;
            existingResource.CodesCycleId = resource.CodesCycleId;
            await _context.SaveChangesAsync();

            return existingResource;
        }

        public async Task<IEnumerable<GetUser_Result>> GetUsersAsync()
        {
            var usersEntities = await _context.Users.Include(r => r.CodesResources).ToListAsync();
            return _mapper.Map<IEnumerable<GetUser_Result>>(usersEntities);
        }
        public async Task<GetUser_Result?> GetUserByIdAsync(long id)
        {
            var userEntity = await _context.Users.Include(r => r.CodesResources).FirstOrDefaultAsync(r => r.UserId == id);
            return _mapper.Map<GetUser_Result>(userEntity);
        }
        public async Task<CreateUser_Result> CreateUserAsync(User usr)
        {
            _context.Users.Add(usr);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateUser_Result>(usr);

        }
        public async Task<User> UpdateUserAsync(UserUpdateRequest usr)
        {
            var existingUser = await _context.Users
                                        .FirstOrDefaultAsync(u => u.UserId == usr.UserId);

            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {usr.UserId} not found.");
            }

            if (usr.FirstName != null)
            {
                existingUser.FirstName = usr.FirstName;
            }
            if (usr.LastName != null)
            {
                existingUser.LastName = usr.LastName;
            }
            if (usr.Email != null)
            {
                existingUser.Email = usr.Email;
            }
            if (usr.IsExternal != null)
            {
                existingUser.IsExternal = (bool)usr.IsExternal;
            }
            await _context.SaveChangesAsync();

            return existingUser;

        }
        public async Task DeleteUserAsync(long userId)
        {
            var usr = await GetUserByIdAsync(userId);
            if (usr != null)
            {
                var targetUser = _mapper.Map<User>(usr);
                _context.Users.Remove(targetUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GetCodesResources_Result?> GetByUserIdAsync(long userId)
        {
            var codesResourcesEntity = await _context.CodesResources.FirstOrDefaultAsync(r => r.ResourceUserId == userId);
            return _mapper.Map<GetCodesResources_Result>(codesResourcesEntity);
        }

        public async Task<IEnumerable<CodesResource>> GetCodesResourcesByRoleAsync(IEnumerable<string> roles)
        {

            if (roles == null || !roles.Any())
                return Enumerable.Empty<CodesResource>();

            return await _context.CodesResources.Where(r => r.IsActive)
                .Where(r => r.WPRole != null && roles.Contains(r.WPRole)).Include(r => r.User)
                .AsNoTracking().ToListAsync();

        }
    }
}

