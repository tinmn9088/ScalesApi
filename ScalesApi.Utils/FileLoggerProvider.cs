using Microsoft.Extensions.Options;
using ScalesApi.Contracts;

namespace ScalesApi.Utils;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly FileLoggerConfiguration _configuration;

    public FileLoggerProvider(IOptions<FileLoggerConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public ILogger CreateLogger(string categoryName)
    {
        
        return new FileLogger(categoryName, _configuration);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}