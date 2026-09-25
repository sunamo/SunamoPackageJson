namespace SunamoPackageJson;

/// <summary>
/// Represents the structure of a package.json file.
/// Property names are lowercase to match JSON serialization format.
/// </summary>
public class PackageJson
{
    /// <summary>
    /// Gets or sets the production dependencies. Cannot be a List because it is a JSON object, not an array.
    /// </summary>
    public Dictionary<string, string>? dependencies { get; set; }

    /// <summary>
    /// Gets or sets the development dependencies.
    /// </summary>
    public Dictionary<string, string>? devDependencies { get; set; }

    /// <summary>
    /// Gets or sets the package name.
    /// </summary>
    public string? name { get; set; }

    /// <summary>
    /// Gets or sets the package version.
    /// </summary>
    public string? version { get; set; }

    /// <summary>
    /// Gets or sets the package description.
    /// </summary>
    public string? description { get; set; }

    /// <summary>
    /// Gets or sets the package manager specification.
    /// </summary>
    public string? packageManager { get; set; }

    /// <summary>
    /// Gets or sets the npm scripts.
    /// </summary>
    public Dictionary<string, string>? scripts { get; set; }

    /// <summary>
    /// Gets or sets the package keywords.
    /// </summary>
    public List<object>? keywords { get; set; }

    /// <summary>
    /// Gets or sets the package author.
    /// </summary>
    public string? author { get; set; }

    /// <summary>
    /// Gets or sets the license type.
    /// </summary>
    public string? license { get; set; }

    /// <summary>
    /// Gets or sets the main entry point.
    /// </summary>
    public string? main { get; set; }

    /// <summary>
    /// Gets or sets the module type (e.g. "module" or "commonjs").
    /// </summary>
    public string? type { get; set; }

    /// <summary>
    /// Gets or sets whether the package is private. Must be nullable so that false is not serialized when it was not originally present.
    /// </summary>
    public bool? @private { get; set; }

    /// <summary>
    /// Gets or sets the repository URL.
    /// </summary>
    public string? repository { get; set; }

    /// <summary>
    /// Gets or sets the directory mappings.
    /// </summary>
    public Dictionary<string, string>? directories { get; set; }

    /// <summary>
    /// Gets or sets the engine version requirements.
    /// </summary>
    public Dictionary<string, string>? engines { get; set; }

    /// <summary>
    /// Gets or sets the Ember.js configuration.
    /// </summary>
    public Dictionary<string, string>? ember { get; set; }

    /// <summary>
    /// Gets or sets the bundled dependencies.
    /// </summary>
    public Dictionary<string, string>? bundledDependencies { get; set; }

    /// <summary>
    /// Returns the version of the specified package from dependencies or devDependencies. Never returns null.
    /// </summary>
    /// <param name="packageName">The name of the package to look up.</param>
    /// <returns>The version string, or an empty string if the package is not found.</returns>
    public string GetVersionFromDepsOrDevDeps(string packageName)
    {
        if (dependencies != null && dependencies.ContainsKey(packageName)) return dependencies[packageName];
        if (devDependencies != null && devDependencies.ContainsKey(packageName)) return devDependencies[packageName];

        return "";
    }

    /// <summary>
    /// Serializes the package.json object to a JSON string, ignoring null properties.
    /// </summary>
    /// <returns>A formatted JSON string representation.</returns>
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}
