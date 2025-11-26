namespace Garage.Extensions;

public static class StringExtensions
{
    extension(string source)
    {
        public string? ValueOrDefault() => string.IsNullOrWhiteSpace(source) ? default : source;
    }
}
