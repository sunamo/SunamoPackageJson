namespace SunamoPackageJson;

public class PackageJson
{
    public Dictionary<string, string>? dependencies { get; set; }

    public Dictionary<string, string>? devDependencies { get; set; }

    public string? name { get; set; }

    public string? version { get; set; }

    public string? description { get; set; }

    public string? packageManager { get; set; }

    public Dictionary<string, string>? scripts { get; set; }

    public List<object>? keywords { get; set; }

    public string? author { get; set; }

    public string? license { get; set; }

    public string? main { get; set; }

    public string? type { get; set; }

    public bool? @private { get; set; }

    public string? repository { get; set; }

    public Dictionary<string, string>? directories { get; set; }

    public Dictionary<string, string>? engines { get; set; }

    public Dictionary<string, string>? ember { get; set; }

    public Dictionary<string, string>? bundledDependencies { get; set; }

    public string GetVersionFromDepsOrDevDeps(string packageName)
    {
        if (dependencies != null && dependencies.ContainsKey(packageName)) return dependencies[packageName];
        if (devDependencies != null && devDependencies.ContainsKey(packageName)) return devDependencies[packageName];

        return "";
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}
