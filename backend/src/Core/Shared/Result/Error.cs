namespace Core.Shared.Result;

public class Error
{
    public IEnumerable<string> Messages { get; set; } = [];
    public int? Code { get; private set; } = 500;

    public Error() { }
    public Error(string message, int? code)
    {
        this.Messages = [message];
        this.Code = code;
    }

    public Error(IEnumerable<string>errorMessages, int? code)
    {
        this.Messages = errorMessages;
        Code = code;
    }
}
