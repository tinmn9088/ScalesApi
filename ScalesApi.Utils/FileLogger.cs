using ScalesApi.Contracts;

namespace ScalesApi.Utils;

/// <summary>
/// Пишет логи в файл. Путь к файлу задается в конфигурации, иначе текущая дата по умолчанию.
/// </summary>
/// <example>
/// 19.04.2023 12:43:04 [Microsoft.Hosting.Lifetime] [Information] Application started. Press Ctrl+C to shut down.
/// </example>
public class FileLogger : ILogger
{
    private const string _DefaultLogLevelKey = "Default";
    private readonly string _categoryName;
    private readonly string _path;
    private readonly Dictionary<string, LogLevel> _logLevelByCategory;

    public FileLogger(string categoryName, FileLoggerConfiguration configuration)
    {
        _categoryName = categoryName;
        _path = configuration.Path ?? $"{DateTime.Now:yyyyMMdd}.log";
        _logLevelByCategory = configuration.LogLevel ?? new Dictionary<string, LogLevel>();
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return new Scope();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return _logLevelByCategory.TryGetValue(_categoryName, out LogLevel categoryLogLevel)
            ? (int)logLevel >= (int)categoryLogLevel
            : _logLevelByCategory.TryGetValue(_DefaultLogLevelKey, out LogLevel defaultLogLevel) && (int)logLevel >= (int)defaultLogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }
        var logMessage = formatter(state, exception);
        var logEntry = $"[{DateTime.Now:G}] [{_categoryName}] [{logLevel}] {logMessage}{Environment.NewLine}";
        File.AppendAllText(_path, logEntry);
    }

    private class Scope : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}