namespace Steps.Shared;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public string? Message { get; protected set; }
    public string? TraceId { get; protected set; }

    protected Result(bool isSuccess, string? message, string? traceId)
    {
        IsSuccess = isSuccess;
        Message = message;
        TraceId = traceId;
    }
    public static Result Failure(string message, string traceId)
        => new(false, message, traceId);
    
    public static Result Success(string message, string? traceId = null)
        => new(true, message, traceId);
}

public class Result<T> : Result
{
    public T? Value { get; }

    public Result(): base (true, default, default)
    {
    }

    private Result(bool isSuccess, T? value, string? message, string? traceId)
    : base(isSuccess, message, traceId){
        Value = value;
    }

    public static Result<T> Success(T value, string? message = "Success.", string? traceId = null)
        => new(true, value, message, traceId);

    public static Result<T> Failure(string? message = "Failure.", string? traceId = null)
        => new(false, default, message, traceId);

    public static Result<T> NotFound(string message = "Not found.", string? traceId = null)
        => Failure(message, traceId: traceId);

    public static Result<T> ValidationError(string message = "Validation error.", string? traceId = null)
        => Failure(message, traceId: traceId);

    public static Result<T> Unauthorized(string? traceId = null)
        => Failure("Unauthorized access.", traceId: traceId);

    public static Result<T> Forbidden(string? traceId = null)
        => Failure("Forbidden access.", traceId: traceId);

    public Result<T> SetTraceId(string traceId)
    {
        TraceId = traceId;
        return this;
    }
    
    public Result<T> SetMessage(string message)
    {
        Message = message;
        return this;
    }
}