using Microsoft.Extensions.Logging;
using MsLogger = Microsoft.Extensions.Logging;

namespace Thuxten.Logging;

public sealed class MaskingLogger<T> : MsLogger.ILogger<T> where T : class
{
    private readonly MsLogger.ILogger<T> _inner;
    private readonly MaskingProcessor _processor;

    internal MaskingLogger(MsLogger.ILogger<T> inner, MaskingProcessor processor)
    {
        _inner = inner;
        _processor = processor;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!_inner.IsEnabled(logLevel)) return;

        _inner.Log(logLevel, eventId, state, exception,
            (s, ex) => _processor.Mask(formatter(s, ex)));
    }

    public bool IsEnabled(LogLevel logLevel) => _inner.IsEnabled(logLevel);
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        => _inner.BeginScope(state);
}