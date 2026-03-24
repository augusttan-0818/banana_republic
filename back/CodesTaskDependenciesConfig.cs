using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesTaskDependenciesConfig : IEntityTypeConfiguration<CodesTaskDependencies>
    {
        public void Configure(EntityTypeBuilder<CodesTaskDependencies> builder)
        {
            builder.ToTable("CodesTaskDependencies");
            builder.HasKey(e => e.DependencyId);

            builder.HasOne(e => e.SourceTaskRef)
                .WithMany()
                .HasForeignKey(e => e.SourceTask)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.DestinationTaskRef)
                .WithMany()
                .HasForeignKey(e => e.DestinationTask)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.SourceActivityRef)
                .WithMany()
                .HasForeignKey(e => e.SourceActivity)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.DestinationActivityRef)
                .WithMany()
                .HasForeignKey(e => e.DestinationActivity)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
