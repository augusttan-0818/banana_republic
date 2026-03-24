using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesTaskPCFsConfig : IEntityTypeConfiguration<CodesTaskPCFs>
    {
        public void Configure(EntityTypeBuilder<CodesTaskPCFs> builder)
        {
            builder.HasNoKey(); 
            builder.ToTable("CodesTaskPCFs"); 

        }
    }
}
