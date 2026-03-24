using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Domain.Entities.ReferenceDocumentUpdate;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts
{
    public class ReferenceDocumentUpdateDbContext : DbContext
    {
        public ReferenceDocumentUpdateDbContext(DbContextOptions<ReferenceDocumentUpdateDbContext> options) : base(options) { }

        // Ref Doc Update Tables
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Standard> Standards { get; set; }
        public DbSet<StandardUpdate> StandardUpdates { get; set; }
        public DbSet<StandardUpdateStatus> StandardUpdateStatuses { get; set; }
        public DbSet<StandardUpdateSubStatus> StandardUpdateSubStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReferenceDocumentUpdateDbContext).Assembly);
        }
    }
}
