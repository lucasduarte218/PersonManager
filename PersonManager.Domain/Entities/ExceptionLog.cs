namespace PersonManager.Domain.Entities;

public class ExceptionLog
{
    public long Id { get; set; }
    public string Path { get; set; } = default!;
    public string Method { get; set; } = default!;
    public string? ExceptionMessage { get; set; }
    public string? StackTrace { get; set; }
    public DateTime Timestamp { get; set; }
}