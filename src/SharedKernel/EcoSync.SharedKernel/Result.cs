namespace EcoSync.SharedKernel;

public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string Error { get; }

    private Result(bool isSuccess, T? value, string error)
    {
        if (isSuccess && !string.IsNullOrEmpty(error))
            throw new InvalidOperationException("A successful result cannot have an error.");

        if (!isSuccess && string.IsNullOrEmpty(error))
            throw new InvalidOperationException("A failed result must have an error.");

        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, string.Empty);
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T>(false, default, error);
    }

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }
}

public static class Result
{
    public static Result<T> Success<T>(T value)
    {
        return Result<T>.Success(value);
    }

    public static Result<T> Failure<T>(string error)
    {
        return Result<T>.Failure(error);
    }
}
