using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;

namespace Steps.Infrastructure.Data.EntityConfiguration;

public class ContestConfiguration : IEntityTypeConfiguration<Contest>
{
    public void Configure(EntityTypeBuilder<Contest> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.HasIndex(t => t.Name)
            .IsUnique()
            .UseCollation("case_insensitive");
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(EntityConfiguration.MaxNameLength);
        
        builder.HasMany(c => c.Entries)
            .WithOne(e => e.Contest)
            .HasForeignKey(e => e.ContestId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}