using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Querying;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;

namespace NRC.Const.CodesAPI.Infrastructure.Services.Repositories
{
    public class CodesCCRsRepository(CodesCCRDbContext context) : ICodesCCRsRepository
    {
        private readonly CodesCCRDbContext _context = context ?? throw new ArgumentNullException(nameof(context));



        public async Task<IEnumerable<CodesCCR>> GetCodesCCRsAsync()
        {
            var codesCCRs = await _context.CodesCCRs
                   .Include(c => c.CodesCycle)
                   .Include(c => c.CCRInternalStatus)
                   .Include(c => c.CodesCCRProponent)
                   .ToListAsync();

            return codesCCRs;
        }

        public async Task<PagedResult<CodesCCR>> GetPagedCodesCCRsAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var query = _context.CodesCCRs.OrderByDescending(ccr => ccr.CCRId).AsNoTracking();
            var totalItemCount = await query.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var codesCCRs = await query
                .Include(c => c.CodesCycle)
                .Include(c => c.CCRInternalStatus)
                .Include(c => c.CodesCCRProponent)
                .Include(c => c.LeadTAResource).ThenInclude(r => r.User)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<CodesCCR>
            {
                Items = codesCCRs,
                TotalCount = totalItemCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }

        public Task DeleteCodesCCRAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CodesCCR> CreateCodeCCRsAsync(CodesCCR reqCodeCCRs)
        {
            throw new NotImplementedException();
        }


        public async Task<CodesCCR?> GetCodesCCRByIdAsync(int id)
        {
            var codesCCR = await _context.CodesCCRs
                               .Include(c => c.CodesCycle)
                               .Include(c => c.CCRInternalStatus)
                               .Include(c => c.CodesCCRProponent)
                               .Include(c => c.ChangeType)
                               .Include(c => c.FormatType)
                              .FirstOrDefaultAsync(c => c.CCRId == id);
            return codesCCR;
        }


        public async Task UpdateCodeCCRsAsync(CodesCCR reqCodeCCRs)
        {
            var ccr = await _context.CodesCCRs.FindAsync(reqCodeCCRs.CCRId);

            if (ccr == null)
                throw new KeyNotFoundException($"Code Change Request with Id {reqCodeCCRs.CCRId} not found.");

            // Update properties
            ccr.CreatedOn = reqCodeCCRs.CreatedOn;
            ccr.CodeReference = reqCodeCCRs.CodeReference;
            ccr.CodeReferenceFR = reqCodeCCRs.CodeReferenceFR;
            ccr.CCRNumber = reqCodeCCRs.CCRNumber;
            ccr.ChangeTypeId = reqCodeCCRs.ChangeTypeId;
            ccr.FormatTypeId = reqCodeCCRs.FormatTypeId;
            ccr.Subject = reqCodeCCRs.Subject;
            ccr.SubjectFR = reqCodeCCRs.SubjectFR;
            ccr.Problem = reqCodeCCRs.Problem;
            ccr.ProblemFR = reqCodeCCRs.ProblemFR;
            ccr.Language = reqCodeCCRs.Language;
            ccr.ChangeSolution = reqCodeCCRs.ChangeSolution;
            ccr.ChangeSolutionFR = reqCodeCCRs.ChangeSolutionFR;
            ccr.EnforcementImplications = reqCodeCCRs.EnforcementImplications;
            ccr.EnforcementImplicationsFR = reqCodeCCRs.EnforcementImplicationsFR;
            ccr.ImpactCosts = reqCodeCCRs.ImpactCosts;
            ccr.ImpactCostsFR = reqCodeCCRs.ImpactCostsFR;
            ccr.ImpactBenefits = reqCodeCCRs.ImpactBenefits;
            ccr.ImpactBenefitsFR = reqCodeCCRs.ImpactBenefitsFR;
            ccr.Justification = reqCodeCCRs.Justification;
            ccr.JustificationFR = reqCodeCCRs.JustificationFR;
            ccr.Comments = reqCodeCCRs.Comments;
            ccr.CommentsFR = reqCodeCCRs.CommentsFR;
            ccr.SubmittedByFPT = reqCodeCCRs.SubmittedByFPT;
            ccr.CodesCCRProponentId = reqCodeCCRs.CodesCCRProponentId;
            ccr.SortingOutputId = reqCodeCCRs.SortingOutputId;
            ccr.TeamLead = reqCodeCCRs.TeamLead;
            ccr.LeadTA = reqCodeCCRs.LeadTA;
            ccr.SortingCommitteesConcernedId = reqCodeCCRs.SortingCommitteesConcernedId;
            ccr.CCRDescription = reqCodeCCRs.CCRDescription;
            ccr.CCRDescriptionFR = reqCodeCCRs.CCRDescriptionFR;
            ccr.CCRTitle = reqCodeCCRs.CCRTitle;
            ccr.CCRTitleFR = reqCodeCCRs.CCRTitleFR;
            ccr.InternalOverallComment = reqCodeCCRs.InternalOverallComment;
            await context.SaveChangesAsync();
        }
        public async Task<SearchResult<CodesCCR>> AdvanceSearchAsync(SearchRequest request)
        {
            var query = _context.CodesCCRs.AsNoTracking();
            query = query.ApplyFilters(request.Filters);
            query = query.ApplySorting(request.Sort);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new SearchResult<CodesCCR>
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public async Task<PagedResult<CodesCCR>> SearchAsync(CcrSearchQuery query)
        {
            var pageNumber = query.PageNumber < 1 ? 1 : query.PageNumber;
            var pageSize = query.PageSize < 10 ? 10 : query.PageSize;


            IQueryable<CodesCCR> q = _context.CodesCCRs.OrderByDescending(ccr => ccr.CCRId).AsNoTracking().AsQueryable();

            if (query.FromCcrId.HasValue)
                q = q.Where(x => x.CCRId >= query.FromCcrId);

            if (query.ToCcrId.HasValue)
                q = q.Where(x => x.CCRId <= query.ToCcrId);

            if (query.TeamLeadId.HasValue)
                q = q.Where(x => x.TeamLead == query.TeamLeadId);

            if (query.LeadTAId.HasValue)
                q = q.Where(x => x.LeadTA == query.LeadTAId);

            if (query.SortingOutputId.HasValue)
                q = q.Where(x => x.SortingOutputId == query.SortingOutputId);

            if (query.TriageOutputId.HasValue)
                q = q.Where(x => x.TriageOutputId == query.TriageOutputId);

            if (query.SubmittedByFPT.HasValue)
                q = q.Where(x => x.SubmittedByFPT == query.SubmittedByFPT);

            if (query.TriageDateSet.HasValue)
                q = q.Where(x => (x.TriageDate != null) == query.TriageDateSet);

            if (query.TriageDate.HasValue)
                q = q.Where(x => x.TriageDate == query.TriageDate);

            if (query.SortingCommitteeId.HasValue)
                q = q.Where(x => x.SortingCommitteesConcernedId == query.SortingCommitteeId);

            if (query.TriageCommitteeId.HasValue)
                q = q.Where(x => x.TriageCommitteesConcernedId == query.TriageCommitteeId);

            if (query.FromDateSubmitted.HasValue)
                q = q.Where(x => x.SubmissionDate >= query.FromDateSubmitted);

            if (query.ToDateSubmitted.HasValue)
                q = q.Where(x => x.SubmissionDate <= query.ToDateSubmitted);


            var totalItemCount = await q.CountAsync();

            var codesCCRs = await q.Include(c => c.LeadTAResource).ThenInclude(r => r.User)
                .Skip((pageNumber - 1) * pageSize)
                .Take(query.PageSize)
                .ToListAsync();


            return new PagedResult<CodesCCR>
            {
                Items = codesCCRs,
                TotalCount = totalItemCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }

        public async Task<CodesCCR?> GetWithHistoryAsync(int id)
        {
            return await _context.CodesCCRs
                .Include(c => c.CodesCycle)
                .Include(c => c.CCRInternalStatus)
                .Include(c => c.CodesCCRProponent)
                .Include(c => c.ChangeType)
                .Include(c => c.FormatType)
                .Include(x => x.StatusHistory)
                .FirstOrDefaultAsync(x => x.CCRId == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<CodesCCR?> GetForStatusUpdateAsync(int ccrId)
        {
            return await _context.CodesCCRs
                .FirstOrDefaultAsync(x => x.CCRId == ccrId);
        }

        public async Task AddStatusHistoryAsync(CodesCCRStatusHistory history)
        {
            await _context.CodesCCRStatusHistories.AddAsync(history);
        }

        public async Task<CodesCCRStatus?> GetStatusByIdAsync(short statusId)
        {
            return await _context.CodesCCRStatuses
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StatusId == statusId);
        }


        #region Code cycles
        public async Task<IEnumerable<CodesCycle>> GetCodesCycles()
        {
            return await _context.CodesCycles.ToListAsync();
        }
        public async Task<CodesCycle?> GetCodesCycle(short id)
        {
            var codesCycle = await _context.CodesCycles.FindAsync(id);
            return codesCycle;
        }

        public async Task<IEnumerable<CodesCCRStatusHistory>> GetStatusByCCRIdAsync(int ccrId)
        {
            var statuses = await _context.CodesCCRStatusHistories.Where(h => h.CCRId == ccrId)
                .Include(c => c.Status)
                .Include(c => c.ChangedBy).ThenInclude(s => s.User)
                .ToListAsync();
            return statuses;
        }

        public async Task<CodesCCR> CreateCCRAsync(CodesCCR ccr)
        {
            await _context.AddAsync(ccr);
            return ccr;
        }
        #endregion

    }
}
