using AutoMapper;
using NRC.Const.CodesAPI.Application.Abstractions;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using System.Security.Claims;

namespace NRC.Const.CodesAPI.Application.Services
{

    public class CodesCCRService : ICodesCCRService
    {
        private readonly ICodesCCRsRepository _repo;
        private readonly IUnifiedContactRepository _unifiedContactRepository;
        private readonly ICodesCCRProponentRepository _proponentRepository;

        private readonly IMapper _mapper;
        private readonly ICurrentRequestResource _currentResource;

        public CodesCCRService(

            ICodesCCRsRepository repo,
            IUnifiedContactRepository unifiedContactRepository,
            ICodesCCRProponentRepository proponentRepository,
            IMapper mapper,
            ICurrentRequestResource currentResource)
        {
            _repo = repo;
            _unifiedContactRepository = unifiedContactRepository;
            _proponentRepository = proponentRepository;
            _mapper = mapper;
            _currentResource = currentResource;
        }

        // Get CCRs paged
        public async Task<PagedResult<GetCodesCCRs_Result>> GetPagedCodesCCRsAsync(int page, int size)
        {
            var entities = await _repo.GetPagedCodesCCRsAsync(page, size);
            return _mapper.Map<PagedResult<GetCodesCCRs_Result>>(entities);
        }

        // Get single CCR by ID
        public async Task<CcrDetailDto?> GetCodesCCRByIdAsync(int id)
        {
            var entity = await _repo.GetWithHistoryAsync(id);
            return entity == null ? null : _mapper.Map<CcrDetailDto>(entity);
        }

        // Update CCR
        public async Task UpdateCCRAsync(CodesCCRUpdateRequest request, ClaimsPrincipal user)
        {
            var resourceId = _currentResource.ResourceId;
            if (!resourceId.HasValue)
                throw new UnauthorizedAccessException("ResourceId not set for current user.");

            var ccr = await _repo.GetCodesCCRByIdAsync(request.CCRId);
            if (ccr == null)
                throw new KeyNotFoundException($"CCR {request.CCRId} not found.");

            _mapper.Map(request, ccr);

            await _repo.SaveAsync();
        }

        // Change CCR status with history
        public async Task ChangeStatusAsync(int ccrId, ChangeCcrStatusRequest request)
        {
            var resourceId = _currentResource.ResourceId;
            if (!resourceId.HasValue)
                throw new UnauthorizedAccessException("ResourceId not set for current user.");

            var ccr = await _repo.GetCodesCCRByIdAsync(ccrId);
            if (ccr == null)
                throw new KeyNotFoundException($"CCR {ccrId} not found.");

            var status = await _repo.GetStatusByIdAsync(request.StatusId);
            if (status == null)
                throw new ArgumentException($"Status {request.StatusId} is invalid.");

            // Update CCR current status
            ccr.CurrentStatusId = status.StatusId;

            // Add status history
            var history = new CodesCCRStatusHistory
            {
                CCRId = ccrId,
                StatusId = status.StatusId,
                ChangedById = resourceId.Value,
                ChangedDate = DateTime.Now,
                Comments = request.Comments
            };

            await _repo.AddStatusHistoryAsync(history);

            await _repo.SaveAsync();
        }

        // Advance search
        public async Task<SearchResult<GetCodesCCRs_Result>> AdvanceSearchAsync(SearchRequest request)
        {
            var ccrs = await _repo.AdvanceSearchAsync(request);
            return _mapper.Map<SearchResult<GetCodesCCRs_Result>>(ccrs);
        }

        // Normal search
        public async Task<PagedResult<GetCodesCCRs_Result>> SearchAsync(CcrSearchQuery query)
        {
            var ccrs = await _repo.SearchAsync(query);
            return _mapper.Map<PagedResult<GetCodesCCRs_Result>>(ccrs);
        }

        public async Task<IEnumerable<CcrStatusDto>> GetStatusByCCRIdAsync(int ccrId)
        {
            var statuses = await _repo.GetStatusByCCRIdAsync(ccrId);
            return _mapper.Map<IEnumerable<CcrStatusDto>>(statuses);
        }

        public async Task<CodesCCRCreateResponse> CreateCCRAsync(CodesCCRCreateRequest request)
        {
            // 1️ Get UnifiedContact
            var unifiedContact = await _unifiedContactRepository.GetUnifiedContactByIdAsync(request.ProponentId);
            if (unifiedContact == null)
                throw new Exception("Proponent not found");

            // 2️ Create Proponent snapshot
            var proponent = _mapper.Map<CodesCCRProponent>(unifiedContact);
            
            await _proponentRepository.AddAsync(proponent);
            
            // 3 Create CCR
            var ccr = _mapper.Map<CodesCCR>(request);
            ccr.CodesCCRProponent = proponent;
            ccr.UnifiedContactId = unifiedContact.UnifiedContactId;
            await _repo.CreateCCRAsync(ccr);
            
            await _repo.SaveAsync();

            return _mapper.Map<CodesCCRCreateResponse>(ccr);
        }
    }
}
