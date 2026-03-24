using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Committees;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Resources;


namespace NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts
{
    public class CommitteeDbContext : DbContext
    {
        public CommitteeDbContext(DbContextOptions<CommitteeDbContext> options) : base(options) { }

        public DbSet<CodesCommittee> CodesCommittees { get; set; }
        public DbSet<CodesCycle> CodesCycles { get; set; }
        public DbSet<CodesCommitteeType> CodesCommitteeTypes { get; set; }

        public DbSet<CodesCommitteeSupportRole> CodesCommitteeSupportRoles { get; set; }

        public DbSet<CodesCommitteeSupportResource> CodesCommitteeSupportResources { get; set; }

        public DbSet<CodesResource> CodesResources { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommitteeDbContext).Assembly);

        }
    }

}

