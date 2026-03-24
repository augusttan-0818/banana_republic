using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Committees;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Committees;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Committees;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICommitteeRepository
    {
        Task<IEnumerable<GetCommittees_Result>> GetCommitteesAsync();
        Task<GetSingleCommittee_Result?> GetCommitteeByIdAsync(int id);
        Task<CreateCommittee_Result> CreateCommitteeAsync(CommitteeCreateRequest reqCommittee);
        Task<UpdateCommittee_Result> UpdateCommitteeAsync(CommitteeUpdateRequest reqCommittee);
        Task DeleteCommitteeAsync(int id);
        Task<IEnumerable<GetCommittees_Result>> GetCommitteesByTypeAsync(byte committeeType);
        Task<GetCommitteeMinimal_Result?> GetMinimalCommitteeByIdAsync(int id);
        Task<IEnumerable<GetParentCommittees_Result>> GetParentCommittees();
        Task<IEnumerable<GetParentCommittees_Result>> GetExistingParentCommittees();

        #region Committee Types
        Task<IEnumerable<CodesCommitteeType>> GetCommitteeTypes();
        Task<CodesCommitteeType> GetCommitteeType(byte id);
        Task<CodesCommitteeType> CreateCommitteeType(CodesCommitteeType committeeType);

        #endregion

        #region Committee Support Resources and Roles

        Task<IEnumerable<CodesCommitteeSupportResourceMinimal>> GetCommitteeDetailedResources(int committeeId);
        #endregion



    }
}

