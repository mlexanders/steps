using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;

namespace Steps.Infrastructure.Data.EntityConfiguration;

public class SoloResultConfiguration : IEntityTypeConfiguration<SoloResult>
{
    public void Configure(EntityTypeBuilder<SoloResult> builder)
    {
        builder.HasIndex(p => new { p.AthleteId, p.ContestId })
            .IsUnique();

        builder.HasOne(p => p.Rating)
            .WithOne()
            .HasForeignKey<SoloResult>(p => p.RatingId);
    }
}
