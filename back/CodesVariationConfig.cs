using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Variations;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesVariationConfig : IEntityTypeConfiguration<CodesVariation>
    {
        public void Configure(EntityTypeBuilder<CodesVariation> builder)
        {
            builder.HasKey(e => e.CodeId).HasName("PK__tmp_ms_x__9A4BCC0CA4035F97");

            builder.Property(e => e.CodeId)
                .ValueGeneratedNever()
                .HasColumnName("code_id");
            builder.Property(e => e.ArticleNumber)
                .HasMaxLength(50)
                .HasColumnName("article_number");
            builder.Property(e => e.ArticleTitle).HasColumnName("article_title");
            builder.Property(e => e.CodeBook)
                .HasMaxLength(100)
                .HasColumnName("code_book");
            builder.Property(e => e.CodeDivision)
                .HasMaxLength(50)
                .HasColumnName("code_division");
            builder.Property(e => e.CodeYear).HasColumnName("code_year");
            builder.Property(e => e.Jurisdiction)
                .HasMaxLength(50)
                .HasColumnName("jurisdiction");
            builder.Property(e => e.PartNumber)
                .HasMaxLength(50)
                .HasColumnName("part_number");
            builder.Property(e => e.SectionNumber)
                .HasMaxLength(50)
                .HasColumnName("section_number");
            builder.Property(e => e.SentenceNumber)
                .HasMaxLength(50)
                .HasColumnName("sentence_number");
            builder.Property(e => e.SentenceText).HasColumnName("sentence_text");
            builder.Property(e => e.SubsectionNumber)
                .HasMaxLength(50)
                .HasColumnName("subsection_number");
        }
    }
}
