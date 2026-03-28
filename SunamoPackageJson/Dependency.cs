namespace SunamoPackageJson;

/// <summary>
/// Represents a single package dependency with a key (package name) and value (version).
/// </summary>
public class Dependency
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Dependency"/> class.
    /// </summary>
    /// <param name="key">The package name.</param>
    /// <param name="value">The package version.</param>
    public Dependency(string key, string value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Gets or sets the package name.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the package version.
    /// </summary>
    public string Value { get; set; }
}
