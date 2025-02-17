using FluentValidation;
using Steps.Shared;

namespace Steps.Application.ExceptionsHandling.Descriptors;

internal class ValidationExceptionDescriptor : ExceptionDescriptor<ValidationException>
{
    public override (Result Result, int StatusCode) GetDescriptionWithStatusCode(ValidationException exception)
    {
        var error = exception.Errors.First();
        var description =  GetDescriptionWithStatusCode(error.ErrorCode, error.ErrorMessage, StatusCodes.Status400BadRequest);
        
        return (Result.Fail(description.Error), description.StatusCode);
    }
}