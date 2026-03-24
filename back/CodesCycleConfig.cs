using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesCycleConfig : IEntityTypeConfiguration<CodesCycle>
    {
        public void Configure(EntityTypeBuilder<CodesCycle> builder)
        {
            builder.HasKey(x => x.CodesCycleId); 
            builder.ToTable("CodesCycle"); 
        }
    }
}
