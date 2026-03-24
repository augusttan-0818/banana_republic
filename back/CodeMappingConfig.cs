using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Variations;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodeMappingConfig : IEntityTypeConfiguration<CodeMapping>
    {
        public void Configure(EntityTypeBuilder<CodeMapping> builder)
        {
            builder.HasKey(e => e.MappingId).HasName("PK__CodeMapp__5AE90045F09FEBCC");

            builder.Property(e => e.MappingId).HasColumnName("mapping_id");
            builder.Property(e => e.DestinationCodeId).HasColumnName("destination_code_id");
            builder.Property(e => e.DestinationJurisdiction)
                .HasMaxLength(50)
                .HasColumnName("destination_jurisdiction");
            builder.Property(e => e.DifferenceType)
                .HasMaxLength(50)
                .HasColumnName("difference_type");
            builder.Property(e => e.SourceCodeId).HasColumnName("source_code_id");
            builder.Property(e => e.SourceJurisdiction)
                .HasMaxLength(50)
                .HasColumnName("source_jurisdiction");
            builder.Property(e => e.Variation).HasColumnName("variation");
            builder.Property(e => e.VariationLabel)
                .HasMaxLength(500)
                .HasColumnName("variation_label");

            builder.HasOne(d => d.DestinationCode).WithMany(p => p.CodeMappingDestinationCodes)
                .HasForeignKey(d => d.DestinationCodeId)
                .HasConstraintName("FK__CodeMappi__desti__68487DD7");

            builder.HasOne(d => d.SourceCode).WithMany(p => p.CodeMappingSourceCodes)
                .HasForeignKey(d => d.SourceCodeId)
                .HasConstraintName("FK__CodeMappi__sourc__693CA210");
        }
    }
}
