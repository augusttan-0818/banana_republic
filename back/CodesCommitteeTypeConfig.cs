using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Committees;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCommitteeTypeConfig : IEntityTypeConfiguration<CodesCommitteeType>
    {
        public void Configure(EntityTypeBuilder<CodesCommitteeType> builder)
        {
            builder.HasKey(x => x.CommitteeTypeId); 
            builder.ToTable("CodesCommitteeType");

            builder.HasOne(e => e.CodesCycle)
                .WithMany()
                .HasForeignKey(e => e.CodesCycleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
