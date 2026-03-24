using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts
{
    public class ReferenceDataDbContext : DbContext
    {
        public ReferenceDataDbContext(DbContextOptions<ReferenceDataDbContext> options) : base(options) { }
        public DbSet<CodesCycle> CodesCycles { get; set; }
        public DbSet<CodesCCRCodeChangeType> CodesCCRCodeChangeTypes { get; set; }
        public DbSet<CodesCCRFormatType> CodesCCRFormatTypes { get; set; }

        public DbSet<CodesCCRSortingOutput> CodesCCRSortingOutputs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReferenceDataDbContext).Assembly);

        }
    }
}
