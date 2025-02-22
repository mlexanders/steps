using MediatR.Pipeline;
using Steps.Application.Exceptions;
using Steps.Application.ExceptionsHandling;
using Steps.Shared;

namespace Steps.Application.Behaviors;

public class RequestExceptionHandler<TRequest, TResponse, TException>
    : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TException : Exception
    where TResponse : Result
    where TRequest : notnull
{
    private readonly CommonExceptionHandler _exceptionHandler;

    public RequestExceptionHandler(CommonExceptionHandler exceptionHandler)
    {
        _exceptionHandler = exceptionHandler;
    }

    public async Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state,
        CancellationToken cancellationToken)
    {
        var description = await _exceptionHandler.GetDescription(exception);
        throw new AppHandledException(description.content, description.statusCode, exception);
    }
}