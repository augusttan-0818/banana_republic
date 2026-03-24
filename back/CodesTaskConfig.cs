using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesTaskConfig : IEntityTypeConfiguration<CodesTask>
    {
        public void Configure(EntityTypeBuilder<CodesTask> builder)
        {
            builder.HasKey(x => x.TaskId); 
            builder.ToTable("CodesTask");

            builder.HasOne(e => e.CodesCycle)
                .WithMany()
                .HasForeignKey(e => e.CodesCycleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CodesTaskPriority)
                .WithMany()
                .HasForeignKey(e => e.TaskPriorityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CodesTaskType)
                .WithMany()
                .HasForeignKey(e => e.TaskType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CodesTaskStatus)
                .WithMany()
                .HasForeignKey(e => e.TaskStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.NMCCCodesCommittee)
                .WithMany()
                .HasForeignKey(e => e.NMCCId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.TaskGroupCodesCommittee)
                .WithMany()
                .HasForeignKey(e => e.TaskGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.WorkGroupCodesCommittee)
                .WithMany()
                .HasForeignKey(e => e.WorkGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CodesStrategicPriority)
                .WithMany()
                .HasForeignKey(e => e.StrategicPriorityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CodesLifecycleStage)
                .WithMany()
                .HasForeignKey(e => e.LifeCycleStateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CodesTaskCriticality)
                .WithMany()
                .HasForeignKey(e => e.TaskCriticalityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
