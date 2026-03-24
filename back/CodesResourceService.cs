using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodesResources;
using NRC.Const.CodesAPI.Application.Interfaces;

namespace NRC.Const.CodesAPI.Application.Services
{
    public class CodesResourceService : ICodesResourceService
    {
        private readonly IResourcesRepository _repo;
        private readonly IMapper _mapper;

        public CodesResourceService(IResourcesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CodesResourceDto>> GetCodesResourcesByRoleAsync(IEnumerable<string> roles)
        {
            var entities = await _repo.GetCodesResourcesByRoleAsync(roles);
            return _mapper.Map<IEnumerable<CodesResourceDto>>(entities);
        }
    }
}
