using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;

namespace Steps.Infrastructure.Data.EntityConfiguration;

public class EntryConfiguration : IEntityTypeConfiguration<Entry>
{
    public void Configure(EntityTypeBuilder<Entry> builder)
    {
        builder.HasOne(e => e.EntryAthletesList)
            .WithOne(eal => eal.Entry)
            .HasForeignKey<EntryAthletesList>(eal => eal.EntryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.Contest)
            .WithMany(c => c.Entries)
            .HasForeignKey(e => e.ContestId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.User)
            .WithMany(u => u.Entries)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}