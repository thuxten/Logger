namespace Thuxten.Logging;

public class LoggerOption
{
    internal bool StructuredLogging { get; private set; } = false;
    
    public LoggerOption UseStructuredLogging()
    {
        StructuredLogging = true;
        return this;
    }

    public LoggerOption UseNormalLogging()
    {
        StructuredLogging = false;
        return this;
    }
}