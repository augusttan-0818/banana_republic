using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodesResources;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodesResourceService
    {
        Task<IEnumerable<CodesResourceDto>> GetCodesResourcesByRoleAsync(IEnumerable<string> roles);
    }
}
