using System.Diagnostics.CodeAnalysis;
using Steps.Shared.Contracts;

namespace Steps.Shared;

public class Result : IResult
{
    public bool Success { get; }
    public string? Message { get; protected set; }
    public List<Error> Errors { get; }

    protected Result(bool success, List<Error>? errors = null)
    {
        Success = success;
        Errors = errors ?? [];
    }

    public static Result Ok() => new(true);

    public static Result Fail(List<Error> errors) => new(false, errors);

    public static Result Fail(Error error) => new(false, new List<Error> { error });

    public static Result Fail(string errorMessage) =>
        new(false, new List<Error> { new Error("GENERAL_ERROR", errorMessage) });

    public static Result Fail(string errorCode, string errorMessage) =>
        new(false, new List<Error> { new Error(errorCode, errorMessage) });

    public Result SetMessage(string message)
    {
        Message = message;
        return this;
    }
}

public interface IResult
{
}

public class Result<T> : Result // where T : IDTO
{
    public T? Value { get; }

    private Result(T value) : base(true)
    {
        Value = value;
    }

    private Result(List<Error> errors) : base(false, errors)
    {
        Value = default;
    }

    public static Result<T> Ok(T value) => new(value);

    public static new Result<T> Fail(List<Error> errors) => new(errors);

    public static new Result<T> Fail(Error error) => new(new List<Error> { error });

    public static new Result<T> Fail(string errorMessage) =>
        new(new List<Error> { new Error("GENERAL_ERROR", errorMessage) });

    public static new Result<T> Fail(string errorCode, string errorMessage) =>
        new(new List<Error> { new Error(errorCode, errorMessage) });

    public new Result<T> SetMessage(string message)
    {
        Message = message;
        return this;
    }
}