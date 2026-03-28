namespace SunamoPackageJson._sunamo;

/// <summary>
/// Provides string splitting utilities.
/// </summary>
internal class SHSplit
{
    /// <summary>
    /// Splits the specified text by the given delimiters, removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The delimiters to split by.</param>
    /// <returns>A list of non-empty substrings.</returns>
    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
