using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesTaskActivitiesConfig : IEntityTypeConfiguration<CodesTaskActivities>
    {
        public void Configure(EntityTypeBuilder<CodesTaskActivities> builder)
        {
            builder.HasKey(x => x.TaskActivityId); 
            builder.ToTable("CodesTaskActivities");

            builder.HasOne(e => e.CodesTasks)
                .WithMany()
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CodesTaskResource)
                .WithMany()
                .HasForeignKey(e => e.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
