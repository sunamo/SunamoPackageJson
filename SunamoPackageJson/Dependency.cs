namespace SunamoPackageJson;

/// <summary>
/// Represents the dependency type.
/// </summary>
public class Dependency
{
    /// <summary>
    /// Initializes a new instance of the dependency class.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public Dependency(string key, string value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Gets or sets the key.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public string Value { get; set; }
}
