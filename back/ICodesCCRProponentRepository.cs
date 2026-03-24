using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public  interface ICodesCCRProponentRepository
    {
        Task<CodesCCRProponent?> GetByIdAsync(int id);
        Task<IEnumerable<CodesCCRProponent>> GetAllAsync();
        Task AddAsync(CodesCCRProponent entity);
    }
}
