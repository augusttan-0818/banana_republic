using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Core;


namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCCRInternalStatusConfig : IEntityTypeConfiguration<CodesCCRInternalStatus>
    {
        public void Configure(EntityTypeBuilder<CodesCCRInternalStatus> builder)
        {
            builder.HasKey(x => x.CCRInternalStatusId);
            builder.ToTable("CodesCCRInternalStatus");
        }
    }
}
