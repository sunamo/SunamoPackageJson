namespace SunamoPackageJson;

public class PackageJsonHelper
{
    public static
#if ASYNC
    async Task<Dictionary<int, List<string>>>
#else
        Dictionary<int, List<string>>
#endif
        CategorizeByFirstNumberOfPackage(string folder, string package)
    {
        Dictionary<int, List<string>> result = new();

        var pjs = Directory.GetFiles(folder, "package.json", SearchOption.AllDirectories);

        foreach (var item in pjs)
        {
            var d = Parse(
#if ASYNC
    await
#endif
                TFSE.ReadAllText(item));

            var p = d.GetVersionFromDepsOrDevDeps(package).TrimStart('^');

            if (p != Consts.se)
            {
                if (p == "latest")
                {
                    DictionaryHelper.AddOrCreate(result, int.MaxValue, item);
                }
                else
                {
                    var parts = SHSunamoExceptions.Split(p, ".");


                    if (int.TryParse(parts[0], out var i)) DictionaryHelper.AddOrCreate(result, i, item);
                }

                DictionaryHelper.AddOrCreate(result, -1, item);
            }
            else
            {
                DictionaryHelper.AddOrCreate(result, -1, item);
            }
        }

        return result;
    }

    public static PackageJson Parse(string json)
    {
        var ds = JsonConvert.DeserializeObject<PackageJson>(json);
        return ds;
    }

    public static
#if ASYNC
    async Task<List<string>>
#else
    List<string>  
#endif
 PackageNamesFromPackageJson(string jsonOrPath)
    {
        if (File.Exists(jsonOrPath)) jsonOrPath =
#if ASYNC
    await
#endif
 TFSE.ReadAllText(jsonOrPath);

        var prefix = @"https://www.npmjs.com/package/";
        var v = Parse(jsonOrPath);
        var result = new List<string>();

        foreach (var item in v.dependencies) result.Add(prefix + item.Key);

        foreach (var item in v.devDependencies) result.Add(prefix + item.Key);

        return result;
    }


    public static
#if ASYNC
    async Task
#else
    void  
#endif
 OpenPackagesFromPackageJsonFromNpm(string jsonOrPath, Action<string> openInBrowser,
        string cdnProvidersUnpkgd, Func<string, string, string> uriWebServicesFromChromeReplacement)
    {
        if (File.Exists(jsonOrPath)) jsonOrPath =
#if ASYNC
    await
#endif
 TFSE.ReadAllText(jsonOrPath);

        var v = Parse(jsonOrPath);
        foreach (var item in v.dependencies)
            openInBrowser(uriWebServicesFromChromeReplacement.Invoke(cdnProvidersUnpkgd, item.Key));

        foreach (var item in v.devDependencies)
            openInBrowser(uriWebServicesFromChromeReplacement.Invoke(cdnProvidersUnpkgd, item.Key));
    }
}
