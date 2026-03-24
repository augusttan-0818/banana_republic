using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCCRsConfig : IEntityTypeConfiguration<CodesCCR>
    {
        public void Configure(EntityTypeBuilder<CodesCCR> builder)
        {
            builder.HasKey(x => x.CCRId);
            builder.ToTable("CodesCCR");

            builder.HasOne(e => e.CodesCycle)
               .WithMany()
               .HasForeignKey(e => e.CodesCycleId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CCRInternalStatus)
                       .WithMany()
                       .HasForeignKey(e => e.CCRInternalStatusId)
                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CodesCCRProponent)
                         .WithMany()
                         .HasForeignKey(e => e.CodesCCRProponentId)
                         .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.UnifiedContact)
                         .WithMany()
                         .HasForeignKey(e => e.UnifiedContactId)
                         .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.LeadTAResource)
                                    .WithMany()
                                    .HasForeignKey(e => e.LeadTA)
                                    .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
