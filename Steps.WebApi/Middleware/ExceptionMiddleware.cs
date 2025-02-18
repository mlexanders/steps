using Steps.Application.ExceptionsHandling;
using Steps.Application.Interfaces;

namespace Steps.Services.WebApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IExceptionHandler _exceptionHandler;

    public ExceptionMiddleware(RequestDelegate next, IExceptionHandler exceptionHandler)
    {
        _next = next;
        _exceptionHandler = exceptionHandler;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var result = await _exceptionHandler.GetDescription(exception);
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = result.statusCode;
        await context.Response.WriteAsJsonAsync(result.content);
    }
}