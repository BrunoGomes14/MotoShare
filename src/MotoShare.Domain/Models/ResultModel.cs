namespace MotoShare.Domain;

public sealed class ResultModel
{
    public ResultModel(bool success, string message, object? data = null)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public bool Success { get; private set; }
    public string Message { get; private set; }
    public object? Data { get; private set; }
}
