using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Common.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Resources;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts
{
    public class CodesCCRDbContext : DbContext
    {
        private readonly ICurrentRequestResource _requestResource;

        public CodesCCRDbContext(DbContextOptions<CodesCCRDbContext> options,
            ICurrentRequestResource requestResource) : base(options)
        {
            _requestResource = requestResource;
        }

        public DbSet<CodesCCR> CodesCCRs { get; set; }
        public DbSet<CodesCycle> CodesCycles { get; set; }
        public DbSet<CodesCCRInternalStatus> CCRInternalStatuses { get; set; }
        public DbSet<CodesCCRCodeChangeType> CodesCCRCodeChangeTypes { get; set; }
        public DbSet<CodesCCRStatusHistory> CodesCCRStatusHistories { get; set; }
        public DbSet<CodesCCRStatus> CodesCCRStatuses { get; set; }
        public DbSet<CodesResource> CodesResources { get; set; }
        public DbSet<CodesCCRProponent> CodesCCRProponents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodesCCRDbContext).Assembly);

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var resourceId = _requestResource.ResourceId;
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = resourceId;
                    entry.Entity.CreatedDate = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedBy = resourceId;
                    entry.Entity.ModifiedDate = now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
