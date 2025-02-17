using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Steps.Infrastructure.Data;
using Steps.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.DbContext;

public class DbContextDefinition : AppDefinition
{
    /// <summary>
    ///     Configure services for current application
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builder"></param>
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddUnitOfWork<ApplicationDbContext>();
    }

    public override void Use(WebApplication app)
    {
        app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
    }
}