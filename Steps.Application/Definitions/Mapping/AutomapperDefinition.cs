using Steps.Utils.AppDefinition;

namespace Steps.Application.Definitions.Mapping;

public class AutomapperDefinition : AppDefinition
{

    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
        => services.AddAutoMapper(typeof(AutomapperDefinition));

    public override void Use(WebApplication app)
    {
        var mapper = app.Services.GetRequiredService<AutoMapper.IConfigurationProvider>();
        if (app.Environment.IsDevelopment())
        {
            // validate Mapper Configuration
            mapper.AssertConfigurationIsValid();
        }
        else
        {
            mapper.CompileMappings();
        }
    }
}