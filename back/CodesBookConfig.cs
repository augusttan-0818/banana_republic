using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesBookConfig : IEntityTypeConfiguration<CodesBook>
    {
        public void Configure(EntityTypeBuilder<CodesBook> builder)
        {
            builder.HasKey(x => x.CodesBookId); 
            builder.ToTable("CodesBook"); 
        }
    }
}
