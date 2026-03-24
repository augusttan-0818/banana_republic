using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesTaskPriorityConfig : IEntityTypeConfiguration<CodesTaskPriority>
    {
        public void Configure(EntityTypeBuilder<CodesTaskPriority> builder)
        {
            builder.HasKey(x => x.TaskPriorityId); 
            builder.ToTable("CodesTaskPriority"); 
        }
    }
}
