using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Committees;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCommitteeSupportRoleConfig : IEntityTypeConfiguration<CodesCommitteeSupportRole>
    {
        public void Configure(EntityTypeBuilder<CodesCommitteeSupportRole> builder)
        {
            builder.HasKey(x => x.CommitteeSupportRoleId); 
            builder.ToTable("CodesCommitteeSupportRoles"); 
        }
    }
}
