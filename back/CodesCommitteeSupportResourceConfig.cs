using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Committees;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCommitteeSupportResourceConfig : IEntityTypeConfiguration<CodesCommitteeSupportResource>
    {
        public void Configure(EntityTypeBuilder<CodesCommitteeSupportResource> builder)
        {
            builder.HasKey(x => x.CommitteeSupportResourceId); 
            builder.ToTable("CodesCommitteeSupportResources"); 
            builder.HasOne(sr => sr.SupportRole)
            .WithMany()
            .HasForeignKey(sr => sr.CommitteeSupportRoleId);
        }
    }
}
