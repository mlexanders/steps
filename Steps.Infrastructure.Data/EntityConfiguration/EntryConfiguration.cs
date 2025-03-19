using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;

namespace Steps.Infrastructure.Data.EntityConfiguration;

public class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder.HasMany(e => e.Athletes)
            .WithMany();
        
        builder.HasOne(e => e.Contest)
            .WithMany(c => c.Entries)
            .HasForeignKey(e => e.ContestId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Creator)
            .WithMany()
            .HasForeignKey(e => e.CreatorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}