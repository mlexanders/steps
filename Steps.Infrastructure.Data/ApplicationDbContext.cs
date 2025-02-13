using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data.EntityConfiguration;

namespace Steps.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Athlete> Athletes { get; set; }
    public DbSet<User> User { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasCollation("case_insensitive", locale: "und-u-ks-primary", provider: "icu", deterministic: false);
        
        modelBuilder.ApplyConfiguration(new AthleteConfiguration());
        modelBuilder.ApplyConfiguration(new ClubConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}