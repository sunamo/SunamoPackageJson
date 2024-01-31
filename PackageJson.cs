namespace SunamoPackageJson;

public class PackageJson
{
    /// <summary>
    ///     deps se špatně parsují protože to není pole ale objekt, nejde List<Dependency>
    /// </summary>
    public Dictionary<string, string>? dependencies { get; set; }

    public Dictionary<string, string>? devDependencies { get; set; }

    public string name { get; set; }
    public string version { get; set; }
    public string description { get; set; }
    public string packageManager { get; set; }
    public Dictionary<string, string> scripts { get; set; }
    public List<object> keywords { get; set; }
    public string author { get; set; }
    public string license { get; set; }
    public string main { get; set; }
    public string type { get; set; }

    /// <summary>
    ///     musí být null aby se nedávalo false i když předtím nebylo
    /// </summary>
    public bool? @private { get; set; } = null;

    public string repository { get; set; }
    public Dictionary<string, string> directories { get; set; }
    public Dictionary<string, string> engines { get; set; }
    public Dictionary<string, string> ember { get; set; }
    public Dictionary<string, string> bundledDependencies { get; set; }

    /// <summary>
    ///     never return null
    /// </summary>
    /// <param name="package"></param>
    /// <returns></returns>
    public string GetVersionFromDepsOrDevDeps(string package)
    {
        if (dependencies != null && dependencies.ContainsKey(package)) return dependencies[package];
        if (devDependencies != null && devDependencies.ContainsKey(package)) return devDependencies[package];

        return Consts.se;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}
