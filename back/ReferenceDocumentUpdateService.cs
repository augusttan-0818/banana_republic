using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.ReferenceDocumentUpdate;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.Interfaces;

namespace NRC.Const.CodesAPI.Application.Services
{
    public class ReferenceDocumentUpdateService : IReferenceDocumentUpdateService
    {
        private readonly IReferenceDocumentUpdateRepository _repository;
        private readonly IMapper _mapper;

        public ReferenceDocumentUpdateService(
            IReferenceDocumentUpdateRepository repository,
            IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // Standard Updates
        public async Task<IEnumerable<StandardUpdateDto>> GetAllStandardUpdatesAsync()
        {
            var standardUpdates = await _repository.GetAllStandardUpdatesAsync();
            return _mapper.Map<IEnumerable<StandardUpdateDto>>(standardUpdates);
        }

        public async Task<StandardUpdateDto?> GetStandardUpdateByIdAsync(int id)
        {
            var standardUpdate = await _repository.GetStandardUpdateByIdAsync(id);
            return standardUpdate == null ? null : _mapper.Map<StandardUpdateDto>(standardUpdate);
        }

        public async Task<IEnumerable<ReferenceDocumentUpdateListDto>> GetAllStandardUpdatesListAsync()
        {
            var standardUpdates = await _repository.GetAllStandardUpdatesAsync();
            return _mapper.Map<IEnumerable<ReferenceDocumentUpdateListDto>>(standardUpdates);
        }

        public async Task<PagedResult<ReferenceDocumentUpdateListDto>> GetPagedStandardUpdatesListAsync(int page, int pageSize)
        {
            var pagedResult = await _repository.GetPagedStandardUpdatesAsync(page, pageSize);
            return new PagedResult<ReferenceDocumentUpdateListDto>
            {
                Items = _mapper.Map<IEnumerable<ReferenceDocumentUpdateListDto>>(pagedResult.Items),
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize,
            };
        }

        // Standards
        public async Task<IEnumerable<StandardDto>> GetAllStandardsAsync()
        {
            var standards = await _repository.GetAllStandardsAsync();
            return _mapper.Map<IEnumerable<StandardDto>>(standards);
        }

        public async Task<StandardDto?> GetStandardByIdAsync(string id)
        {
            var standard = await _repository.GetStandardByIdAsync(id);
            return standard == null ? null : _mapper.Map<StandardDto>(standard);
        }

        // Agencies
        public async Task<IEnumerable<AgencyDto>> GetAllAgenciesAsync()
        {
            var agencies = await _repository.GetAllAgenciesAsync();
            return _mapper.Map<IEnumerable<AgencyDto>>(agencies);
        }

        public async Task<AgencyDto?> GetAgencyByIdAsync(string id)
        {
            var agency = await _repository.GetAgencyByIdAsync(id);
            return agency == null ? null : _mapper.Map<AgencyDto>(agency);
        }

        // Statuses
        public async Task<IEnumerable<StandardUpdateStatusDto>> GetAllStatusesAsync()
        {
            var statuses = await _repository.GetAllStatusesAsync();
            return _mapper.Map<IEnumerable<StandardUpdateStatusDto>>(statuses);
        }

        public async Task<IEnumerable<StandardUpdateSubStatusDto>> GetSubStatusesByStatusIdAsync(int statusId)
        {
            var subStatuses = await _repository.GetSubStatusesByStatusIdAsync(statusId);
            return _mapper.Map<IEnumerable<StandardUpdateSubStatusDto>>(subStatuses);
        }
    }
}

