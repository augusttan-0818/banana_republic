using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Domain.Entities.CodesSavedSearches;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts
{
    public class CodesSavedSearchDbContext : DbContext
    {
        public CodesSavedSearchDbContext(DbContextOptions<CodesSavedSearchDbContext> options) : base(options) { }
        public DbSet<CodesSavedSearch> CodesSavedSearch { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodesSavedSearchDbContext).Assembly);

        }
    }
}

