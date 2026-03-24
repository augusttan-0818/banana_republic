using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCCRFormatTypeConfig : IEntityTypeConfiguration<CodesCCRFormatType>
    {
        public void Configure(EntityTypeBuilder<CodesCCRFormatType> builder)
        {
            builder.HasKey(x => x.FormatTypeId);
            builder.ToTable("CodesCCRFormatType");
        }
    }
}
