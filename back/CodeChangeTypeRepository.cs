using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;

namespace NRC.Const.CodesAPI.Infrastructure.Services.Repositories
{
    public class CodeChangeTypeRepository(ReferenceDataDbContext context) : ICodeChangeTypeRepository
    {
        private readonly ReferenceDataDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        public async Task<IEnumerable<CodesCCRCodeChangeType>> GetCodeChangeTypes()
        {
            return await _context.CodesCCRCodeChangeTypes.ToListAsync();
        }

        public async Task<CodesCCRCodeChangeType?> GetCodeChangeType(byte id)
        {
            var codesChangeType = await _context.CodesCCRCodeChangeTypes.FindAsync(id);
            return codesChangeType;
        }

    }
}
