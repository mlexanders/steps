using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;

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

        builder.HasMany(c => c.GroupBlocks)
            .WithOne(gb => gb.Contest)
            .HasForeignKey(gb => gb.ContestId);
        
        builder.HasOne(c => c.LateAthletesList)
            .WithOne(lal => lal.Contest)
            .HasForeignKey<LateAthletesList>(lal => lal.ContestId);
        
        builder.HasOne(c => c.PreAthletesList)
            .WithOne(pal => pal.Contest)
            .HasForeignKey<PreAthletesList>(pal => pal.ContestId);
        
        builder.HasOne(c => c.GeneratedAthletesList)
            .WithOne(gal => gal.Contest)
            .HasForeignKey<GeneratedAthletesList>(gal => gal.ContestId);
        
        builder
            .HasMany(c => c.Judjes)
            .WithMany(u => u.JudgingContests);

        builder
            .HasMany(c => c.Counters)
            .WithMany(u => u.CountingContests);
    }
}