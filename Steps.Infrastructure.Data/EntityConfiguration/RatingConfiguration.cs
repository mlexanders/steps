using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;
using Steps.Domain.Entities.GroupBlocks;

namespace Steps.Infrastructure.Data.EntityConfiguration;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasOne<TestResult>()
            .WithOne(p => p.Rating)
            .HasForeignKey<Rating>(p => p.TestResultId);
        
        builder.HasOne<Contest>()
            .WithMany()
            .HasForeignKey(p => p.ContestId);
        
        builder.HasOne<GroupBlock>()
            .WithMany()
            .HasForeignKey(p => p.GroupBlockId);
    }
}