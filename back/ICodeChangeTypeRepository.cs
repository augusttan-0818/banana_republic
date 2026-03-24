using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodeChangeTypeRepository
    {
            Task<IEnumerable<CodesCCRCodeChangeType>> GetCodeChangeTypes();
            Task<CodesCCRCodeChangeType?> GetCodeChangeType(byte id);
    }
}
