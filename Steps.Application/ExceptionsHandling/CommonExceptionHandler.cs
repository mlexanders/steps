using Microsoft.EntityFrameworkCore;
using Npgsql;
using Steps.Application.ExceptionsHandling.Descriptors;
using Steps.Application.Interfaces;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.ExceptionsHandling;

public class CommonExceptionHandler : IExceptionHandler
{
    private readonly SqlExceptionDescriptor _sqlDescriptor;
    private readonly BusinessExceptionDescriptor _businessDescriptor;
    private readonly ValidationExceptionDescriptor _validationDescriptor;

    public CommonExceptionHandler()
    {
        _validationDescriptor = new ValidationExceptionDescriptor();
        _sqlDescriptor = new SqlExceptionDescriptor();
        _businessDescriptor = new BusinessExceptionDescriptor();
    }

    public async Task<(Result content, int statusCode)> GetDescription(Exception exception)
    {
        return exception switch
        {
            PostgresException pgEx when string.IsNullOrEmpty(pgEx.SqlState) => _sqlDescriptor
                .GetDescriptionWithStatusCode(pgEx),
            DbUpdateException { InnerException: PostgresException pgEx } => _sqlDescriptor
                .GetDescriptionWithStatusCode(pgEx),
            StepsBusinessException businessException => _businessDescriptor.GetDescriptionWithStatusCode(
                businessException),
            FluentValidation.ValidationException validationException => _validationDescriptor
                .GetDescriptionWithStatusCode(validationException),
            _ => throw new ArgumentOutOfRangeException(nameof(exception), exception, null)
        };
    }
}