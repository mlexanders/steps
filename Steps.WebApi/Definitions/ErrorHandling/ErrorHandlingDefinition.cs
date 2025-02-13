using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Security.Authentication;
using Steps.Services.WebApi.Middleware;
using Steps.Services.WebApi.Utils.AppDefinition;

namespace Steps.Services.WebApi.Definitions.ErrorHandling;

/// <summary>
/// Custom Error handling 
/// </summary>
public class ErrorHandlingDefinition : AppDefinition
{
    public override void Use(WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}