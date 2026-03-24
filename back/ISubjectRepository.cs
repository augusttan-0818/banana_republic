using NRC.Const.CodesAPI.Domain.Entities.Subjects;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllAsync();
    }
}
