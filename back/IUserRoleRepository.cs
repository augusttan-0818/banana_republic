using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<List<string>> GetRoleNamesByUserIdAsync(long userId);
        Task<bool> UserHasRolesAsync(long userId, IEnumerable<string> roleNames, bool requireAll);
    }

}
