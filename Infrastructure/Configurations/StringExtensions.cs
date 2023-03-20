using System.Text.RegularExpressions;

namespace MyApp.Infrastructure;

public static class StringExtensions
{
    private const string pascalPattern = "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])";

    public static string PascalToKebabCase(this string value)
    {
        return transformWithDotNotation(value, pascalToKebabCase);
    }

    public static string PascalToSnakeCase(this string value)
    {
        return transformWithDotNotation(value, pascalToSnakeCase);
    }

    private static string transformWithDotNotation(string value, Func<string, string> transformer)
    {
        if (string.IsNullOrEmpty(value)) return value;
        
        IEnumerable<string> substrings = value.Split(".").Select(transformer);
        return String.Join(".", substrings);
    }

    private static string pascalToKebabCase(string value)
    {
        if (string.IsNullOrEmpty(value)) return value;

        return Regex.Replace(value, pascalPattern, "-$1", RegexOptions.Compiled)
            .Trim().ToLower();
    }

    private static string pascalToSnakeCase(string value)
    {
        if (string.IsNullOrEmpty(value)) return value;

        return Regex.Replace(value, pascalPattern, "_$1", RegexOptions.Compiled)
            .Trim().ToLower();
    }

    public static string PascalToScreamingCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return value;

        return Regex.Replace(value, pascalPattern, "_$1", RegexOptions.Compiled)
            .Trim().ToUpper();
    }

    public static string ScreamingToPascalCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return value;

        var substrings = value.ToLower()
            .Split('_')
            .Select(s => s[0].ToString().ToUpper() + s.Substring(1));

        return string.Join(null, substrings);
    }
}