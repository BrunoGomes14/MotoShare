using Microsoft.Extensions.Logging;

namespace MotoShare.Infrastructure;

public static class FileLoggerConfiguration
{
    public static void AddFileLogger(this ILoggingBuilder loggingBuilder)
    {
        var directory = $"{Directory.GetCurrentDirectory()}/Logs";
        loggingBuilder.AddProvider(new FileLoggerProvider(directory));
    }
}
