using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Resources;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesResources;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Users;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesResources;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Users;
using NRC.Const.CodesAPI.Domain.Entities.Core;


namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface IResourcesRepository
    {
        Task<IEnumerable<GetCodesResources_Result>> GetCodesResourcesAsync();
        Task<GetCodesResources_Result?> GetCodesResourceByIdAsync(int id);
        Task<CreateCodesResource_Result> CreateCodesResourceAsync(CodesResourceCreateRequest resource);
        Task<CodesResource> UpdateCodesResourceAsync(CodesResourceUpdateRequest resource);
        Task DeleteCodesResourceAsync(int resourceId);
        Task<IEnumerable<GetUser_Result>> GetUsersAsync();
        Task<GetUser_Result?> GetUserByIdAsync(long id);
        Task<CreateUser_Result> CreateUserAsync(User usr);
        Task<User> UpdateUserAsync(UserUpdateRequest usr);
        Task DeleteUserAsync(long userId);
        Task<GetCodesResources_Result?> GetByUserIdAsync(long userId);
        Task<IEnumerable<CodesResource>> GetCodesResourcesByRoleAsync(IEnumerable<string> roles);
    }
}

