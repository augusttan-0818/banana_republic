using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodesCycleRepository
    {
        Task<IEnumerable<CodesCycle>> GetCodesCycles();
        Task<CodesCycle> GetCodesCycle(short id);
        Task<CodesCycle> CreateCodesCycle(CodesCycle codesCycle);
    }
}
