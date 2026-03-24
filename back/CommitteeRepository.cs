using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Committees;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Committees;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Committees;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Infrastructure.Services.Repositories
{
    public class CommitteeRepository(CommitteeDbContext context, IMapper mapper) : ICommitteeRepository
    {
        private readonly CommitteeDbContext _context =
       context ?? throw new ArgumentNullException(nameof(context));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        public async Task<IEnumerable<GetCommittees_Result>> GetCommitteesAsync()
        {
            var committees = await _context.CodesCommittees
                .Include(c => c.CodesCycle)
                .Include(c => c.CodesCommitteeType)
                .Include(c => c.ParentCommittee)
                .Select(static c => new GetCommittees_Result
                {
                    CommitteeId = c.CommitteeId,
                    CommitteeName = c.CommitteeName,
                    CommitteeShortName = c.CommitteeShortName,
                    CommitteeType = c.CommitteeType,
                    CodesCycleId = c.CodesCycleId,
                    CodesCycleName = c.CodesCycle != null ? c.CodesCycle.CodesCycleName : "N/A",
                    CodesCommitteeTypeName = c.CodesCommitteeType != null ? c.CodesCommitteeType.CommitteeTypeName : "N/A",
                    ParentCommitteeId = c.ParentCommitteeId,
                    ParentCommitteeName = c.ParentCommittee != null ? c.ParentCommittee.CommitteeName : "None",
                    CombinedName = c.CommitteeShortName + " - " + c.CommitteeName,
                })
                .OrderBy(c => c.CommitteeShortName)
                .ToListAsync();

            return committees;

        }

        public async Task<IEnumerable<GetCommittees_Result>> GetCommitteesByTypeAsync(byte committeeType)
        {
            var committees = await _context.CodesCommittees
                .Where(c => c.CommitteeType == committeeType)
                .Include(c => c.CodesCycle)
                .Include(c => c.CodesCommitteeType)
                 .Include(c => c.ParentCommittee)
                .Select(static c => new GetCommittees_Result
                {
                    CommitteeId = c.CommitteeId,
                    CommitteeName = c.CommitteeName,
                    CommitteeShortName = c.CommitteeShortName,
                    CommitteeType = c.CommitteeType,
                    CodesCycleId = c.CodesCycleId,
                    CodesCycleName = c.CodesCycle != null ? c.CodesCycle.CodesCycleName : "N/A",
                    CodesCommitteeTypeName = c.CodesCommitteeType != null ? c.CodesCommitteeType.CommitteeTypeName : "N/A",
                    ParentCommitteeId = c.ParentCommitteeId,
                    ParentCommitteeName = c.ParentCommittee != null ? c.ParentCommittee.CommitteeName : "None"
                })
                .ToListAsync();
            return committees;
        }
        public async Task<GetSingleCommittee_Result?> GetCommitteeByIdAsync(int id)
        {
            var committee = await _context.CodesCommittees
              .Include(c => c.CodesCycle)
              .Include(c => c.CodesCommitteeType)
              .Include(c => c.ParentCommittee) // Include parent committee details
              .Include(c=>c.SupportResources)
             
              .Select(static c => new GetSingleCommittee_Result
              {
                  CommitteeId = c.CommitteeId,
                  CommitteeName = c.CommitteeName,
                  CommitteeShortName = c.CommitteeShortName,
                  CommitteeType = c.CommitteeType,
                  CodesCycleId = c.CodesCycleId,
                  CodesCycleName = c.CodesCycle != null ? c.CodesCycle.CodesCycleName : "N/A",
                  CodesCommitteeTypeName = c.CodesCommitteeType != null ? c.CodesCommitteeType.CommitteeTypeName : "N/A",
                  ParentCommitteeId = c.ParentCommitteeId,
                  ParentCommitteeName = c.ParentCommittee != null ? c.ParentCommittee.CommitteeName : "None",
                  SupportResources = c.SupportResources,
                  
              })
              .FirstOrDefaultAsync(c => c.CommitteeId == id);
            return committee;
        }

        public async Task<IEnumerable<CodesCommitteeSupportResourceMinimal>> GetCommitteeDetailedResources(int committeeId)
        {
            var resources = await _context.CodesCommitteeSupportResources
                .Where(r => r.CommitteeId == committeeId)
                .Join(_context.CodesResources,
                    csr => csr.ResourceId,
                    cr => cr.ResourceId,
                    (csr,cr)=> new { csr, cr}
                    )
                .Join(_context.Users,
                    comb1 => comb1.cr.ResourceUserId,
                    cu => cu.UserId,
                    (comb1, cu)=> new { comb1,ResourceName = cu.FirstName + ' ' + cu.LastName}
                    )
                .Join(_context.CodesCommitteeSupportRoles,
                    combined => combined.comb1.csr.CommitteeSupportRoleId,
                    role => role.CommitteeSupportRoleId,
                    (combined, role) => new { combined.comb1.csr, combined.ResourceName, RoleName = role.CommitteeSupportRoleName })
                .Select(result => new CodesCommitteeSupportResourceMinimal
                {
                    CommitteeId= result.csr.CommitteeId,
                    ResourceId= result.csr.ResourceId,
                    CommitteeSupportResourceId = result.csr.CommitteeSupportResourceId,
                    CommitteeSupportRoleId= result.csr.CommitteeSupportRoleId,
                    ResourceName = result.ResourceName,
                    RoleName = result.RoleName,
                    Year = result.csr.Year,
                    EffortQuarter1 = result.csr.EffortQuarter1,
                    EffortQuarter2 = result.csr.EffortQuarter2,
                    EffortQuarter3 = result.csr.EffortQuarter3,
                    EffortQuarter4 = result.csr.EffortQuarter4
                })
                .ToListAsync();

            return resources;
        }

        public async Task<GetCommitteeMinimal_Result?> GetMinimalCommitteeByIdAsync(int id)
        {
            var committee = await _context.CodesCommittees
              .Include(c => c.CodesCycle)
              .Include(c => c.CodesCommitteeType)
              .Include(c => c.ParentCommittee) // Include parent committee details
              .Select(static c => new GetCommitteeMinimal_Result
              {
                  CommitteeId = c.CommitteeId,
                  CommitteeName = c.CommitteeName,
                  CommitteeShortName = c.CommitteeShortName,
                  CommitteeType = c.CommitteeType,
                  CodesCycleId = c.CodesCycleId,
                  ParentCommitteeId = c.ParentCommitteeId,
                 
              })
              .FirstOrDefaultAsync(c => c.CommitteeId == id);
            return committee;
        }
        public async Task<CreateCommittee_Result> CreateCommitteeAsync(CommitteeCreateRequest reqCommittee)
        {
            CodesCommittee target = _mapper.Map<CodesCommittee>(reqCommittee);
            _context.CodesCommittees.Add(target); // Add the entity to the DbContext
            await _context.SaveChangesAsync(); // Persist changes to the database
            return _mapper.Map<CreateCommittee_Result>(target);

        }

        public async Task<IEnumerable<GetParentCommittees_Result>> GetParentCommittees()
        {
            var committees = await _context.CodesCommittees
               .Where(c => c.CommitteeType == 2 || c.CommitteeType == 5) // Filter by committee types
                .Select(static c => new GetParentCommittees_Result
                {
                    CommitteeId= c.CommitteeId,
                    CommitteeName= c.CommitteeName
                })
                .Distinct()
                .ToListAsync();

            return committees;
        }

        public async Task<IEnumerable<GetParentCommittees_Result>> GetExistingParentCommittees()
        {
            var parentCommittees = await _context.CodesCommittees
                .Where(c => _context.CodesCommittees.Any(cc => cc.ParentCommitteeId == c.CommitteeId)) // Only referenced committees
                   .Select(static c => new GetParentCommittees_Result
                   {
                       CommitteeId = c.CommitteeId,
                       CommitteeName = c.CommitteeName
                   })
                .Distinct() // Remove duplicates if any
                .ToListAsync();

            return parentCommittees;
        }

        public async Task<UpdateCommittee_Result> UpdateCommitteeAsync(CommitteeUpdateRequest reqCommittee)
        {
            var existingCommittee = await _context.CodesCommittees
                                         .FirstOrDefaultAsync(r => r.CommitteeId == reqCommittee.CommitteeId);

            if (existingCommittee == null)
            {
                throw new KeyNotFoundException($"Committee with ID {reqCommittee.CommitteeId} not found.");
            }

            // Update properties
            existingCommittee.ParentCommitteeId = reqCommittee.ParentCommitteeId;
            existingCommittee.CodesCycleId = reqCommittee.CodesCycleId;
            existingCommittee.CommitteeType = reqCommittee.CommitteeType;
            if( !string.IsNullOrEmpty(reqCommittee.CommitteeName))
            {
                existingCommittee.CommitteeName = reqCommittee.CommitteeName;
            }
            if (!string.IsNullOrEmpty(reqCommittee.CommitteeShortName))
            {
                existingCommittee.CommitteeShortName = reqCommittee.CommitteeShortName;
            }
         
            await _context.SaveChangesAsync();

            return _mapper.Map<UpdateCommittee_Result>(existingCommittee);

        }
        public async Task DeleteCommitteeAsync(int id)
        {
            var comm = await GetMinimalCommitteeByIdAsync(id);
            if (comm != null)
            {
                var targetResource = _mapper.Map<CodesCommittee>(comm);
                _context.CodesCommittees.Remove(targetResource);
                await _context.SaveChangesAsync();
            }

        }

        #region committee types

        public async Task<IEnumerable<CodesCommitteeType>> GetCommitteeTypes()
        {
            return await _context.CodesCommitteeTypes.Include(ct => ct.CodesCycle).ToListAsync();

        }
        public async Task<CodesCommitteeType> GetCommitteeType(byte id) {
            var committeeType = await _context.CodesCommitteeTypes
                    .Include(ct => ct.CodesCycle)
                    .FirstOrDefaultAsync(ct => ct.CommitteeTypeId == id);

            return committeeType;
        }
        public async Task<CodesCommitteeType> CreateCommitteeType(CodesCommitteeType committeeType)
        {           
            _context.CodesCommitteeTypes.Add(committeeType);
            await _context.SaveChangesAsync();
            return committeeType;
         
        }

        #endregion

        #region Code cycles
       public async Task<IEnumerable<CodesCycle>> GetCodesCycles()
        {
            return await _context.CodesCycles.ToListAsync();
        }
        public async Task<CodesCycle> GetCodesCycle(short id)
        {
            var codesCycle = await _context.CodesCycles.FindAsync(id);
            return codesCycle;
        }

        public async Task<CodesCycle> CreateCodesCycle(CodesCycle codesCycle)
        {
            _context.CodesCycles.Add(codesCycle);
            await _context.SaveChangesAsync();
            return codesCycle;
        }

        #endregion
    }
}

