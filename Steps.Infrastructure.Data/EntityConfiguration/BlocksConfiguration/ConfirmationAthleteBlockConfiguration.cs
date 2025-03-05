using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Domain.Entities.GroupBlocks.SubGroups;

namespace Steps.Infrastructure.Data.EntityConfiguration.BlocksConfiguration;

public class ConfirmationAthleteBlockConfiguration : IEntityTypeConfiguration<ConfirmationAthleteSubGroup>
{
    public void Configure(EntityTypeBuilder<ConfirmationAthleteSubGroup> builder)
    {
        builder.HasOne(c => c.SubGroup)
            .WithMany()
            .HasForeignKey(c => c.SubGroupId);

        builder.HasOne(c => c.Athlete)
            .WithMany()
            .HasForeignKey(c => c.AthleteId);
    }
}