using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;

namespace Steps.Infrastructure.Data.EntityConfiguration;

public class ClubConfiguration : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(c => c.Name)
            .IsUnique();
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(EntityConfiguration.MaxNameLength);

        builder.HasOne(c => c.Owner)
            .WithMany()
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}