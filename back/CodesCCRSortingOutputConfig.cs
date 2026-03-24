using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCCRSortingOutputConfig : IEntityTypeConfiguration<CodesCCRSortingOutput>
    {
        public void Configure(EntityTypeBuilder<CodesCCRSortingOutput> builder)
        {
            builder.HasKey(x => x.SortingOutputId);
            builder.ToTable("CodesCCRSortingOutput");
        }
    }
}
