namespace ScalesApi.Contracts;

public record FileLoggerConfiguration
{
    public string? Path { get; set; }
    public Dictionary<string, LogLevel>? LogLevel { get; set; }
}
