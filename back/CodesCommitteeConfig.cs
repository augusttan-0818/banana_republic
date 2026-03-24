using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Committees;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCommitteeConfig : IEntityTypeConfiguration<CodesCommittee>
    {
        public void Configure(EntityTypeBuilder<CodesCommittee> builder)
        {
            builder.HasKey(x => x.CommitteeId);
            builder.ToTable("CodesCommittee"); 

            builder.HasOne(e => e.CodesCycle)
                .WithMany()
                .HasForeignKey(e => e.CodesCycleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CodesCommitteeType)
                .WithMany()
                .HasForeignKey(e => e.CommitteeType)
                .OnDelete(DeleteBehavior.Restrict);

            // Self-referencing relationship for ParentCommitteeId
            builder.HasOne(e => e.ParentCommittee)
                .WithMany(c => c.SubCommittees)
                .HasForeignKey(e => e.ParentCommitteeId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            builder.HasMany(c => c.SupportResources)
                  .WithOne(sr => sr.Committee)
                  .HasForeignKey(sr => sr.CommitteeId);

        }
    }
}
