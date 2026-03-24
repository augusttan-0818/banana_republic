using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesStrategicPriorityConfig : IEntityTypeConfiguration<CodesStrategicPriority>
    {
        public void Configure(EntityTypeBuilder<CodesStrategicPriority> builder)
        {
            builder.HasKey(x => x.StrategicPriorityId); 
            builder.ToTable("CodesStrategicPriority");

            builder.HasOne(e => e.CodesCycle)
                .WithMany()
                .HasForeignKey(e => e.CodesCycleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
