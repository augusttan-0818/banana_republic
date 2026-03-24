using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodesCCRFormatTypeRepository
    {
        Task<IEnumerable<CodesCCRFormatType>> GetCodesCCRFormatTypes();
        Task<CodesCCRFormatType?> GetCodesCCRFormatType(short id);
    }
}
