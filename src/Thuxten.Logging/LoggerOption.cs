using Microsoft.Extensions.Logging;

namespace Thuxten.Logging;

public class LoggerOption
{
    internal bool StructuredLogging { get; private set; } = false;
    internal LogLevel MinimumLogLevel { get; private set; } = LogLevel.Information;
    internal List<MaskingRule> MaskingRules { get; } = new();  

    public LoggerOption MaskData(params string[] fields) 
    {
        MaskingRules.AddRange(fields.Select(f => new MaskingRule(f)));
        return this;
    }
    
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
    
    public LoggerOption SetMinimumLogLevel(
        LogLevel logLevel)
    {
        MinimumLogLevel = logLevel;
        return this;
    }
}