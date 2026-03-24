using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCCRProponentConfig : IEntityTypeConfiguration<CodesCCRProponent>
    {
        public void Configure(EntityTypeBuilder<CodesCCRProponent> builder)
        {
            builder.HasKey(x => x.CCRProponentId);
            builder.ToTable("CodesCCRProponent");
        }
    }
}


