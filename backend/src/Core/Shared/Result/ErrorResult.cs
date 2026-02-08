namespace Core.Shared.Result;

public class ErrorResult
{
    public IEnumerable<string> Errors { get; set; } = [];
    public int? StatusCode { get; private set; } = 500;

    public ErrorResult() { }
    public ErrorResult(string message, int? statusCode)
    {
        this.Errors = [message];
        this.StatusCode = statusCode;
    }

    public ErrorResult(IEnumerable<string> errorMessages, int? statusCode)
    {
        this.Errors =  errorMessages;
        StatusCode = statusCode;
    }
}
