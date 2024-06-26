﻿using Microsoft.Extensions.Logging;
using MotoShare.Infrastructure.Logger;

namespace MotoShare.Infrastructure;

public class FileLoggerProvider : ILoggerProvider
{
    private string _path;
    public FileLoggerProvider(string path)
    {
        _path = path;
    }

    public ILogger CreateLogger(string categoryName) =>
        new FileLogger(_path);

    public void Dispose()
    {
    }
}
