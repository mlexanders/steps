using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities.AthletesLists;

namespace Steps.Infrastructure.Data.EntityConfiguration.AthletesLists;

public class PreAthletesListConfiguration : IEntityTypeConfiguration<PreAthletesList>
{
    public void Configure(EntityTypeBuilder<PreAthletesList> builder)
    {
        builder.HasMany(eal => eal.Athletes)
            .WithMany(a => a.PreAthletesLists);
    }
}