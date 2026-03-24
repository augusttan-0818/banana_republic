using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.SupportStatuses;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;
namespace NRC.Const.CodesAPI.Infrastructure.Services.Repositories
{
    public class SupportStatusesRepository(SupportStatusDbContext context, IMapper mapper) : ICodesPRCommentSupportStatusRepository
    {
        private readonly SupportStatusDbContext _context =
       context ?? throw new ArgumentNullException(nameof(context));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<IEnumerable<GetSupportStatuses_Result>> GetSupportStatusesAsync()
        {
            var supportStatuses = await _context.CodesPRCommentSupportStatus.ToListAsync();
            return _mapper.Map<IEnumerable<GetSupportStatuses_Result>>(supportStatuses);
        }
        public async Task<GetSupportStatuses_Result?> GetSupportStatusesByIdAsync(int id)
        {
            var supportStatus = await _context.CodesPRCommentSupportStatus.FirstOrDefaultAsync(c => c.PRCommentSupportStatusId == id);
            return _mapper.Map<GetSupportStatuses_Result>(supportStatus);
        }
    }
}