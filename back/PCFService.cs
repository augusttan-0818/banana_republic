using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.PCF;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.PCF;
using NRC.Const.CodesAPI.Infrastructure.Repositories;

namespace NRC.Const.CodesAPI.Application.Services
{
    public class PCFService : IPCFService
    {
        private readonly IPCFRepository _repository;
        private readonly IMapper _mapper;

        public PCFService(IPCFRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetPCFTrackingRecordDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<GetPCFTrackingRecordDto>(entity);
        }

        public async Task<List<GetPCFTrackingRecordDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<GetPCFTrackingRecordDto>>(entities);
        }

        public async Task<int> CreateAsync(CreatePCFRequest request)
        {
            int nextNumber = await _repository.GetNextPCFNumberAsync();

            // map request to entity
            var entity = _mapper.Map<PCF>(request);
            entity.PcfNumber = nextNumber;
            int id = await _repository.CreateAsync(entity);
            return nextNumber;
        }

        public async Task UpdateAsync(int id, UpdatePCFRequest request)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"PCF with id {id} not found.");

            _mapper.Map(request, existing);
            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}