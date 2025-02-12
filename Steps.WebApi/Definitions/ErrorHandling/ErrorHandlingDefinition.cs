using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Steps.Services.WebApi.Utils.AppDefinition;
using Steps.Shared;

namespace Steps.Services.WebApi.Definitions.ErrorHandling;


/// <summary>
/// Custom Error handling 
/// </summary>
public class ErrorHandlingDefinition : AppDefinition
{
    public override void Use(WebApplication app)
    {
        app.UseExceptionHandler(error => error.Run(async context =>
        {
            context.Response.ContentType = "application/json";
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature is not null)
            {
                // handling all another errors 
                Log.Logger.Information("TraceIdentifier: {TraceIdentifier}", context.TraceIdentifier);
                Log.Error($"Something went wrong in the {contextFeature.Error}");
                context.Response.StatusCode = GetErrorCode(contextFeature.Error);

                if (app.Environment.IsDevelopment())
                {
                    await context.Response.WriteAsJsonAsync<Result>(Result.Failure(contextFeature.Error.Message,
                        context.TraceIdentifier));
                }
                else
                {
                    //TODO:
                    await context.Response.WriteAsJsonAsync<Result>(Result.Failure(contextFeature.Error.Message,
                        context.TraceIdentifier));
                }
            }
        }));
    }


    private static int GetErrorCode(Exception exception)
        => exception switch
        {
            ValidationException _ => (int)HttpStatusCode.BadRequest,
            AuthenticationException _ => (int)HttpStatusCode.Forbidden,
            NotImplementedException _ => (int)HttpStatusCode.NotImplemented,
            _ => (int)HttpStatusCode.InternalServerError
        };
}