using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Resources;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{ 
    public class CodesResourceConfig : IEntityTypeConfiguration<CodesResource>
    {
        public void Configure(EntityTypeBuilder<CodesResource> builder)
        {
            builder.HasKey(x => x.ResourceId); 
            builder.ToTable("CodesResource");

            builder.HasOne(r => r.User)
                .WithMany(u => u.CodesResources)
                .HasForeignKey(r => r.ResourceUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
