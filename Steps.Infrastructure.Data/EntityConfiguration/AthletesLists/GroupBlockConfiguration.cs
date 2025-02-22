using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities.AthletesLists;

namespace Steps.Infrastructure.Data.EntityConfiguration.AthletesLists;

public class GroupBlockConfiguration : IEntityTypeConfiguration<GroupBlock>
{
    public void Configure(EntityTypeBuilder<GroupBlock> builder)
    {
        builder.HasMany(eal => eal.Athletes)
            .WithMany(a => a.GroupBlocks);

        builder.HasOne(x => x.LateAthletesList)
            .WithMany(a => a.GroupBlocks);
        
        builder.HasOne(x => x.GeneratedAthletesList)
            .WithMany(a => a.GroupBlocks);
    }
}