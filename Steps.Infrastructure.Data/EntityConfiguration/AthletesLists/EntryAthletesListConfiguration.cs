using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;

namespace Steps.Infrastructure.Data.EntityConfiguration.AthletesLists;

public class EntryAthletesListConfiguration : IEntityTypeConfiguration<EntryAthletesList>
{
    public void Configure(EntityTypeBuilder<EntryAthletesList> builder)
    {
        builder.HasMany(eal => eal.Athletes)
            .WithMany(a => a.EntryAthletesLists);
    }
}