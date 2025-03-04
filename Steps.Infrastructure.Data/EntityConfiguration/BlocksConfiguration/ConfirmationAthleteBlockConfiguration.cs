using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;

namespace Steps.Infrastructure.Data.EntityConfiguration.BlocksConfiguration;

public class ConfirmationAthleteBlockConfiguration : IEntityTypeConfiguration<ConfirmationAthleteBlock>
{
    public void Configure(EntityTypeBuilder<ConfirmationAthleteBlock> builder)
    {
        builder.HasOne(c => c.Block)
            .WithMany()
            .HasForeignKey(c => c.BlockId);
        
        builder.HasOne(c => c.Athlete)
            .WithMany()
            .HasForeignKey(c => c.AthleteId);
    }
}