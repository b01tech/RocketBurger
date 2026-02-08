namespace Core.Shared.Result;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public Error? Error { get; }

    private Result(bool isSuccess, T data, IEnumerable<string> errors, int? statusCode = null)
    {
        this.IsSuccess = isSuccess;
        this.Data = data;
        this.Error = new Error(errors, statusCode);
    }

    public static Result<T> Success(T data) => new Result<T>(true, data, default);

    public static Result<T> Failure(IEnumerable<string> errors, int? statusCode = null) =>
        new(false, default, errors, statusCode);

    public static Result<T> Failure(string error, int? statusCode = null) =>
        new(false, default, [error], statusCode);

    public static implicit operator Result<T>(T data) => Success(data);
}
