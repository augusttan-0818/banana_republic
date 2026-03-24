using Microsoft.EntityFrameworkCore;
// using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Domain.Entities.CodesSupportStatuses;


namespace NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts
{
    public class SupportStatusDbContext : DbContext
    {
    public SupportStatusDbContext(DbContextOptions<SupportStatusDbContext> options) : base(options) { }
        public DbSet<CodesPRCommentSupportStatus> CodesPRCommentSupportStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SupportStatusDbContext).Assembly);

        }
    }
}

