using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;

namespace NRC.Const.CodesAPI.Infrastructure.Services.Repositories
{
    public class CodesCCRFormatTypeRepository(ReferenceDataDbContext context, IMapper mapper) : ICodesCCRFormatTypeRepository
    {
        private readonly ReferenceDataDbContext _context =
 context ?? throw new ArgumentNullException(nameof(context));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));


        public async Task<CodesCCRFormatType?> GetCodesCCRFormatType(short id)
        {
            var formatType = await _context.CodesCCRFormatTypes.FindAsync(id);
            return formatType;
        }

        public async Task<IEnumerable<CodesCCRFormatType>> GetCodesCCRFormatTypes()
        {
            return await _context.CodesCCRFormatTypes.ToListAsync();

        }
    }
}
