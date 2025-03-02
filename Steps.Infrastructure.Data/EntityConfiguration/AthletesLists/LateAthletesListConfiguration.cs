using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steps.Domain.Entities.AthletesLists;

namespace Steps.Infrastructure.Data.EntityConfiguration.AthletesLists;

public class LateAthletesListConfiguration : IEntityTypeConfiguration<LateAthletesList>
{
    public void Configure(EntityTypeBuilder<LateAthletesList> builder)
    {
        builder.HasMany(eal => eal.Athletes)
            .WithMany(a => a.LateAthletesLists);
    }
}