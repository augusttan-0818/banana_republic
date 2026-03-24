using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;

namespace NRC.Const.CodesAPI.Infrastructure.Services.Repositories
{
    public class CodeSortingOutputRepository(ReferenceDataDbContext context, IMapper mapper) : ICodeSortingOutputRepository
    {
        private readonly ReferenceDataDbContext _context =
   context ?? throw new ArgumentNullException(nameof(context));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));



        public async Task<IEnumerable<CodesCCRSortingOutput>> GetCodeCCRSortingOutputs()
        {
            return await _context.CodesCCRSortingOutputs.ToListAsync();
        }

        public async Task<CodesCCRSortingOutput?> GetCodeCCRSortingOutput(short id)
        {
            var codesCCRSortingOutput = await _context.CodesCCRSortingOutputs.FindAsync(id);
            return codesCCRSortingOutput;
        }

    }
}
