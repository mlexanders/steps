using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Steps.Infrastructure.Data;
using Steps.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.Swagger;

/// <summary>
///     Swagger definition for application
/// </summary>
public class SwaggerDefinition : AppDefinition
{
    private const string SwaggerConfig = "/swagger/v1/swagger.json";

    public override void Use(WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;


        app.UseSwagger();
        app.UseSwaggerUI(settings =>
        {
            settings.SwaggerEndpoint(SwaggerConfig, $"{AppData.ServiceName} v.{AppData.AppVersion}");
        });
    }

    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("cookieAuth", new OpenApiSecurityScheme
            {
                Name = "Cookie",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Cookie,
                Description = "Cookie: "
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "cookieAuth"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        builder.Services.AddSwaggerGen();
    }
}