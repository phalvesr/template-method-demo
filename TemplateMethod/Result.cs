namespace TemplateMethodDemo.TemplateMethod;

public class Result<T> where T : class
{
    public T Data { get; }
    public bool IsSuccess { get; }

    private Result(bool success, T data)
    {
        IsSuccess = success;
        Data = data;
    }

    internal static Result<T> Failure()
    {
        return new Result<T>(false, null!);
    }
    
    internal static Result<T> Success(T data)
    {
        return new Result<T>(true, data);
    }

    internal static Result<T> OfNullable(T? data)
    {
        if (data is null)
        {
            return Failure();
        }

        return Success(data);
    }
    

    public static implicit operator Result<T>(T data) => new(true, data);
}

public class Result
{
    public bool IsSuccess { get; }

    public Result(bool success)
    {
        IsSuccess = success;
    }
    
    internal static Result Failure()
    {
        return new Result(false);
    }
    
    internal static Result Success()
    {
        return new Result(true);
    }
}