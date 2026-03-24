using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesTaskTypeConfig : IEntityTypeConfiguration<CodesTaskType>
    {
        public void Configure(EntityTypeBuilder<CodesTaskType> builder)
        {
            builder.HasKey(x => x.CodesTaskTypeId); 
            builder.ToTable("CodesTaskType"); 
        }
    }
}
