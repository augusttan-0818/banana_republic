using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCCRCodeChangeTypeConfig : IEntityTypeConfiguration<CodesCCRCodeChangeType>
    {
        public void Configure(EntityTypeBuilder<CodesCCRCodeChangeType> builder)
        {
            builder.HasKey(x => x.CCRCodeChangeTypeId);
            builder.ToTable("CodesCCRCodeChangeType");
        }
    }
}
