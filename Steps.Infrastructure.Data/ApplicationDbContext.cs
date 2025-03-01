using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;
using Steps.Infrastructure.Data.EntityConfiguration;
using Steps.Infrastructure.Data.EntityConfiguration.AthletesLists;

namespace Steps.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Athlete> Athletes { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Entry> Entries { get; set; }
    
    public DbSet<GeneratedAthletesList> GeneratedAthletesLists { get; set; }
    public DbSet<GroupBlock> GroupBlocks { get; set; }
    public DbSet<LateAthletesList> LateAtheletesLists { get; set; }
    public DbSet<PreAthletesList> PreAthletesLists { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasCollation("case_insensitive", locale: "und-u-ks-primary", provider: "icu", deterministic: false);
        
        modelBuilder.ApplyConfiguration(new AthleteConfiguration());
        modelBuilder.ApplyConfiguration(new ClubConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ContestConfiguration());
        modelBuilder.ApplyConfiguration(new EntryConfiguration());
        
        modelBuilder.ApplyConfiguration(new GeneratedAthletesListConfiguration());
        modelBuilder.ApplyConfiguration(new GroupBlockConfiguration());
        modelBuilder.ApplyConfiguration(new LateAthletesListConfiguration());
        modelBuilder.ApplyConfiguration(new PreAthletesListConfiguration());
    }
}