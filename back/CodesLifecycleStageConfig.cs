using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Infrastructure.Persistence.EntityConfigurations
{
    public class CodesLifecycleStageConfig : IEntityTypeConfiguration<CodesLifecycleStage>
    {
        public void Configure(EntityTypeBuilder<CodesLifecycleStage> builder)
        {
            builder.HasKey(x => x.LifecycleStageId); 
            builder.ToTable("CodesLifecycleStage"); 
        }
    }
}
