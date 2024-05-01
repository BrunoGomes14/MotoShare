using Microsoft.Extensions.Logging;

namespace MotoShare.Infrastructure.Logger;

public class FileLogger : ILogger
{
    private string filePath;
    private static object _lock = new object();
    public FileLogger(string path)
    {
        filePath = path;
    }

    public IDisposable BeginScope<TState>(TState state) => default!;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (logLevel == LogLevel.Information) 
            return;

        if (formatter != null)
        {
            lock (_lock)
            {
                string fullFilePath = Path.Combine(filePath, DateTime.Now.ToString("yyyy-MM-dd") + "_log.txt");
                string text = "";

                if (exception != null) 
                    text = $"{Environment.NewLine}{exception.GetType()} : \"{exception.Message}{Environment.NewLine}{exception.StackTrace} {Environment.NewLine}";    

                var logText = $"{logLevel}: {DateTime.Now.ToString()} {formatter(state, exception!)}{Environment.NewLine} {text}";
                File.AppendAllText(fullFilePath, logText);
            }
        }
    }
}
