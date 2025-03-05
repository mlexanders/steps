using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Domain.Entities.GroupBlocks.SubGroups;

namespace Steps.Infrastructure.Data.EntityConfiguration.BlocksConfiguration;

public class FinalAthleteBlockConfiguration : IEntityTypeConfiguration<FinalAthleteSubGroup>
{
    public void Configure(EntityTypeBuilder<FinalAthleteSubGroup> builder)
    {
        builder.HasOne(c => c.SubGroup)
            .WithMany()
            .HasForeignKey(c => c.SubGroupId);
        
        builder.HasOne(c => c.Athlete)
            .WithMany()
            .HasForeignKey(c => c.AthleteId);
    }
}
public class PreSubGroupConfiguration : IEntityTypeConfiguration<PreSubGroup>
{
    public void Configure(EntityTypeBuilder<PreSubGroup> builder)
    {
        builder.HasOne(c => c.FinalSubGroup)
            .WithOne()
            .HasForeignKey<PreSubGroup>(g => g.FinalSubGroupId);
    }
}