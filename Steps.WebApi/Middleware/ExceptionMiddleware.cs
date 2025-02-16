using Steps.Application.ExceptionsHandling;
using Steps.Application.Interfaces;

namespace Steps.Services.WebApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IExceptionDescriptor _exceptionDescriptor;

    public ExceptionMiddleware(RequestDelegate next, IExceptionDescriptor exceptionDescriptor)
    {
        _next = next;
        _exceptionDescriptor = exceptionDescriptor;
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
        var result = await _exceptionDescriptor.GetResult(exception);
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = result.statusCode;
        await context.Response.WriteAsJsonAsync(result.content);
    }
}