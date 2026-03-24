using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;
using NRC.Const.CodesAPI.Application.Interfaces;

namespace NRC.Const.CodesAPI.Infrastructure.Services.Repositories
{
    public class CodesCycleRepository : ICodesCycleRepository
    {
        private readonly TasksDbContext _context;

        public CodesCycleRepository(TasksDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CodesCycle>> GetCodesCycles()
        {
            return await _context.Set<CodesCycle>().ToListAsync();
        }

        public async Task<CodesCycle?> GetCodesCycle(short id)
        {
            return await _context.Set<CodesCycle>()
                                 .FirstOrDefaultAsync(c => c.CodesCycleId == id);
        }

        public async Task<CodesCycle> CreateCodesCycle(CodesCycle codesCycle)
        {
            _context.Set<CodesCycle>().Add(codesCycle);
            await _context.SaveChangesAsync();
            return codesCycle;
        }
    }
}
