using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;

namespace Steps.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Athlete> Athletes { get; set; }
    public DbSet<User> User { get; set; }
}