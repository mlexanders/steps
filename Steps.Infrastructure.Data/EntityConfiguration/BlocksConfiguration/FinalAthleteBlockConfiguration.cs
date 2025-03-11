// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Steps.Domain.Entities.GroupBlocks;
// using Steps.Domain.Entities.GroupBlocks.AthleteSubGroups;
//
// namespace Steps.Infrastructure.Data.EntityConfiguration.BlocksConfiguration;
//
// public class FinalAthleteBlockConfiguration : IEntityTypeConfiguration<FinalAthleteSubGroup>
// {
//     public void Configure(EntityTypeBuilder<FinalAthleteSubGroup> builder)
//     {
//         builder.HasOne(c => c.SubGroup)
//             .WithMany()
//             .HasForeignKey(c => c.SubGroupId);
//         
//         builder.HasOne(c => c.Athlete)
//             .WithMany()
//             .HasForeignKey(c => c.AthleteId);
//     }
// }
// public class PreSubGroupConfiguration : IEntityTypeConfiguration<AthleteSubGroup>
// {
//     public void Configure(EntityTypeBuilder<AthleteSubGroup> builder)
//     {
//         builder.HasOne(c => c.GroupBlock)
//             .WithOne()
//             .HasForeignKey<AthleteSubGroup>(g => g.GroupBlockId);
//         
//         builder.HasOne(c => c.FinalSubGroup)
//             .WithOne()
//             .HasForeignKey<AthleteSubGroup>(g => g.FinalSubGroupId);
//         
//         builder.HasOne(c => c.Athlete)
//             .WithOne()
//             .HasForeignKey<AthleteSubGroup>(g => g.AthleteId);
//     }
// }