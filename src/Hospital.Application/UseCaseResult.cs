namespace Hospital.Application;

public class UseCaseResult<T>
{
    public T? Data { get; init; }
    public string? Error { get; init; }
    public Guid? CorrelationId { get; init; }
    public bool IsSuccess => Error == null;
    public static UseCaseResult<T> Success(T data) => new() { Data = data };
    public static UseCaseResult<T> Failure(string errorMessage, Guid correlationId) => new()
    {
        Error = errorMessage,
        CorrelationId = correlationId
    };
}