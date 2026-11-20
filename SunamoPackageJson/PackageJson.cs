namespace SunamoPackageJson;

/// <summary>
/// Represents the package json type.
/// </summary>
public class PackageJson
{
    /// <summary>
    /// Gets or sets the dependencies.
    /// </summary>
    public Dictionary<string, string>? dependencies { get; set; }

    /// <summary>
    /// Gets or sets the dev dependencies.
    /// </summary>
    public Dictionary<string, string>? devDependencies { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string? name { get; set; }

    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    public string? version { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? description { get; set; }

    /// <summary>
    /// Gets or sets the package manager.
    /// </summary>
    public string? packageManager { get; set; }

    /// <summary>
    /// Gets or sets the scripts.
    /// </summary>
    public Dictionary<string, string>? scripts { get; set; }

    /// <summary>
    /// Gets or sets the keywords.
    /// </summary>
    public List<object>? keywords { get; set; }

    /// <summary>
    /// Gets or sets the author.
    /// </summary>
    public string? author { get; set; }

    /// <summary>
    /// Gets or sets the license.
    /// </summary>
    public string? license { get; set; }

    /// <summary>
    /// Gets or sets the main.
    /// </summary>
    public string? main { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public string? type { get; set; }

    /// <summary>
    /// Gets or sets the private.
    /// </summary>
    public bool? @private { get; set; }

    /// <summary>
    /// Gets or sets the repository.
    /// </summary>
    public string? repository { get; set; }

    /// <summary>
    /// Gets or sets the directories.
    /// </summary>
    public Dictionary<string, string>? directories { get; set; }

    /// <summary>
    /// Gets or sets the engines.
    /// </summary>
    public Dictionary<string, string>? engines { get; set; }

    /// <summary>
    /// Gets or sets the ember.
    /// </summary>
    public Dictionary<string, string>? ember { get; set; }

    /// <summary>
    /// Gets or sets the bundled dependencies.
    /// </summary>
    public Dictionary<string, string>? bundledDependencies { get; set; }

    /// <summary>
    /// Get version from deps or dev deps.
    /// </summary>
    /// <param name="packageName">The package name.</param>
    /// <returns>The get version from deps or dev deps result.</returns>
    public string GetVersionFromDepsOrDevDeps(string packageName)
    {
        if (dependencies != null && dependencies.ContainsKey(packageName)) return dependencies[packageName];
        if (devDependencies != null && devDependencies.ContainsKey(packageName)) return devDependencies[packageName];

        return "";
    }

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns>The to string result.</returns>
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}
