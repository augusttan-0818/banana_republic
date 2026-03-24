using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface IUnifiedContactRepository
    {
        Task<IEnumerable<UnifiedContact>> GetUnifiedContactsAsync();
        Task<UnifiedContact?> GetUnifiedContactByIdAsync(int id);
    }
}
