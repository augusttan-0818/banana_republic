using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesTaskCodeBookConfig : IEntityTypeConfiguration<CodesTaskCodeBook>
    {
        public void Configure(EntityTypeBuilder<CodesTaskCodeBook> builder)
        {
            builder.HasNoKey(); 
            builder.ToTable("CodesTaskCodeBook"); 
        }
    }
}
