using Microsoft.EntityFrameworkCore;
using Npgsql;
using Steps.Application.ExceptionsHandling.Descriptors;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.ExceptionsHandling;

public class CommonExceptionHandler
{
    private readonly SqlExceptionDescriptor _sqlExceptionDescriptor;
    private readonly BusinesExceptionDescriptor _businessExceptionDescriptor;
    private readonly ValidationExceptionDescriptor _validationExceptionDescriptor;

    public CommonExceptionHandler()
    {
        _validationExceptionDescriptor = new ValidationExceptionDescriptor();
        _sqlExceptionDescriptor = new SqlExceptionDescriptor();
        _businessExceptionDescriptor = new BusinesExceptionDescriptor();
    }

    public async Task<(Result content, int statusCode)> Handle(Exception exception)
    {
        var result = exception switch
        {
            PostgresException pgEx when string.IsNullOrEmpty(pgEx.SqlState) => _sqlExceptionDescriptor
                .GetDescriptionWithStatusCode(pgEx),
            DbUpdateException { InnerException: PostgresException pgEx } => _sqlExceptionDescriptor
                .GetDescriptionWithStatusCode(pgEx),
            StepsBusinessException businessException => _businessExceptionDescriptor.GetDescriptionWithStatusCode(
                businessException),
            FluentValidation.ValidationException validationException => _validationExceptionDescriptor
                .GetDescriptionWithStatusCode(validationException),
            _ => throw new ArgumentOutOfRangeException(nameof(exception), exception, null)
        };

        return (Result.Fail(result.Item1), result.Item2);
    }
}