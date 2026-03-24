using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesTaskStatusConfig : IEntityTypeConfiguration<CodesTaskStatus>
    {
        public void Configure(EntityTypeBuilder<CodesTaskStatus> builder)
        {
            builder.HasKey(x => x.TaskStatusId); 
            builder.ToTable("CodesTaskStatus"); 
        }
    }
}
