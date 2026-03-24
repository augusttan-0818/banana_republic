using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    internal class CodesCCRStatusConfig:IEntityTypeConfiguration<CodesCCRStatus>
    {
        public void Configure(EntityTypeBuilder<CodesCCRStatus> builder)
        {
            builder.HasKey(x => x.StatusId);
            builder.ToTable("CodesCCRStatus");

        }
    }

}
