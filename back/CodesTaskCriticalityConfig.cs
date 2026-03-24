using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesTaskCriticalityConfig : IEntityTypeConfiguration<CodesTaskCriticality>
    {
        public void Configure(EntityTypeBuilder<CodesTaskCriticality> builder)
        {
            builder.HasKey(x => x.TaskCriticalityId); 
            builder.ToTable("CodesTaskCriticality"); 
        }
    }
}
