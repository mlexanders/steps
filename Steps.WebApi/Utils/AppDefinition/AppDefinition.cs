﻿namespace Steps.Services.WebApi.Utils.AppDefinition;

public abstract class AppDefinition
{
    public virtual void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
    }

    public virtual void Use(WebApplication app)
    {
    }
}