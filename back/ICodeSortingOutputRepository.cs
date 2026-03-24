using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodeSortingOutputRepository
    {
        Task<IEnumerable<CodesCCRSortingOutput>> GetCodeCCRSortingOutputs();
        Task<CodesCCRSortingOutput?> GetCodeCCRSortingOutput(short id);
    }
}
