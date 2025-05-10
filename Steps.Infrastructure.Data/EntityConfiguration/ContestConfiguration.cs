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

        builder.HasOne(c => c.ScheduleFile)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(c => c.Entries)
            .WithOne(e => e.Contest)
            .HasForeignKey(e => e.ContestId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(c => c.Judges)
            .WithMany()
            .UsingEntity<JudgeContest>(
                l => l.HasOne<User>().WithMany().HasForeignKey(e => e.JudgeId),
                r => r.HasOne<Contest>().WithMany().HasForeignKey(e => e.ContestId));

        builder.HasMany(c => c.Counters)
            .WithMany()
            .UsingEntity<CounterContest>(
                l => l.HasOne<User>().WithMany().HasForeignKey(e => e.CounterId),
                r => r.HasOne<Contest>().WithMany().HasForeignKey(e => e.ContestId));
    }

    public class CounterContest
    {
        public Guid CounterId { get; set; }
        public Guid ContestId { get; set; }
    }

    public class JudgeContest
    {
        public Guid JudgeId { get; set; }
        public Guid ContestId { get; set; }
    }
}