using Steps.Domain.Entities;

namespace Steps.Infrastructure.Data.EntityConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AthleteConfiguration : IEntityTypeConfiguration<Athlete>
{
    public void Configure(EntityTypeBuilder<Athlete> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FullName)
            .IsRequired()
            .HasMaxLength(EntityConfiguration.MaxFullNameLength);
        builder.Property(a => a.BirthDate).IsRequired();

        builder.HasOne<Team>(a => a.Team)
            .WithMany(t => t.Athletes)
            .HasForeignKey(a => a.TeamId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}