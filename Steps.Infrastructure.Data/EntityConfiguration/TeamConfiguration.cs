using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;

namespace Steps.Infrastructure.Data.EntityConfiguration;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => t.Name)
            .IsUnique();
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(EntityConfiguration.MaxNameLength);
        
        builder.Property(t => t.Address)
            .HasMaxLength(EntityConfiguration.MaxAdressLength);

        builder.HasOne(t => t.Owner)
            .WithMany()
            .IsRequired()
            .HasForeignKey(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Athletes)
            .WithOne()
            .HasForeignKey(t => t.TeamId);

        builder.HasOne<Club>(t => t.Club)
            .WithMany(c => c.Teams)
            .IsRequired()
            .HasForeignKey(t => t.ClubId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}