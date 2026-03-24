
using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Domain.Entities.CodesPRCommentPCAs;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts
{
    public class CodesPRCommentPCADbContext : DbContext
    {
    public CodesPRCommentPCADbContext(DbContextOptions<CodesPRCommentPCADbContext> options) : base(options) { }
        public DbSet<CodesPRCommentPCA> CodesPRCommentPCA { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodesPRCommentPCADbContext).Assembly);

        }
    }
}

