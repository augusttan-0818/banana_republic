using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCCRStatusHistoryConfig : IEntityTypeConfiguration<CodesCCRStatusHistory>
    {
        public void Configure(EntityTypeBuilder<CodesCCRStatusHistory> builder)
        {
            builder.HasKey(x => x.StatusHistoryId);

            builder.Property(x => x.ChangedDate)
                .HasColumnType("datetime");

            builder.HasOne(x => x.CCR)
                .WithMany(x => x.StatusHistory)
                .HasForeignKey(x => x.CCRId);
            builder.HasOne(x => x.ChangedBy)
                .WithMany()
                .HasForeignKey(x => x.ChangedById);

            builder.ToTable("CodesCCRStatusHistory");
        }
    }

}
