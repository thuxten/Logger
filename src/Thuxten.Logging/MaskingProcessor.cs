using System.Text.RegularExpressions;

namespace Thuxten.Logging;

internal record MaskingRule(string FieldName);

public class MaskingProcessor
{
    private readonly List<(string Field, Regex Regex)> _rules;

    internal MaskingProcessor(IEnumerable<MaskingRule> rules)
    {
        _rules = rules.Select(r => (
            r.FieldName,
            new Regex(
                @$"(?i)({Regex.Escape(r.FieldName)}[""']?\s*[=:]\s*[""']?)(\S+)",
                RegexOptions.Compiled)
        )).ToList();
    }

    internal string Mask(string message)
    {
        foreach (var (_, regex) in _rules)
            message = regex.Replace(message, m => m.Groups[1].Value + "****");

        return message;
    }
}