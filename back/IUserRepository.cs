using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Users;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Users;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> FindByOidAsync(string identityProvider, string oid);
        Task<long> CreateAsync(User user);
        Task<IEnumerable<GetUser_Result>> GetUsersAsync();
        Task<GetUser_Result?> GetUserByIdAsync(long id);
        Task<CreateUser_Result> CreateUserAsync(User usr);
        Task<User> UpdateUserAsync(UserUpdateRequest usr);
        Task DeleteUserAsync(long userId);

        Task<IEnumerable<GetUser_Result>> GetUsersByRolesAsync(IEnumerable<string> roleNames);

    }

}
