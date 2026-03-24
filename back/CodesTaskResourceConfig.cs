using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesTaskResourceConfig : IEntityTypeConfiguration<CodesTaskResource>
    {
        public void Configure(EntityTypeBuilder<CodesTaskResource> builder)
        {
            builder.HasKey(x => x.TaskResourceId); 
            builder.ToTable("CodesTaskResource"); 
        }
    }
}
