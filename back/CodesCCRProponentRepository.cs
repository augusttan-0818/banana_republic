using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;

namespace NRC.Const.CodesAPI.Infrastructure.Services.Repositories
{
    public class CodesCCRProponentRepository(CodesCCRDbContext context) : ICodesCCRProponentRepository
    {
        private readonly CodesCCRDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<CodesCCRProponent?> GetByIdAsync(int id)
        {
            return await _context.CodesCCRProponents.FirstOrDefaultAsync(x => x.CCRProponentId == id);
        }

        public async Task<IEnumerable<CodesCCRProponent>> GetAllAsync()
        {
            return await _context.CodesCCRProponents
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(CodesCCRProponent entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.CodesCCRProponents.AddAsync(entity);
        }
    }
}
