namespace SunamoPackageJson;

public class PackageJsonHelper
{
    public static
        async Task<Dictionary<int, List<string>>>
        CategorizeByFirstNumberOfPackage(string folder, string packageName)
    {
        Dictionary<int, List<string>> result = new();
        var packageJsonFiles = Directory.GetFiles(folder, "package.json", SearchOption.AllDirectories);
        foreach (var item in packageJsonFiles)
        {
            var packageJson = Parse(
                await
                    FileAsync.ReadAllTextAsync(item));
            var version = packageJson.GetVersionFromDepsOrDevDeps(packageName).TrimStart('^');
            if (version != "")
            {
                if (version == "latest")
                {
                    DictionaryHelper.AddOrCreate(result, int.MaxValue, item);
                }
                else
                {
                    var versionParts = SHSplit.Split(version, ".");
                    if (int.TryParse(versionParts[0], out var majorVersion)) DictionaryHelper.AddOrCreate(result, majorVersion, item);
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
        var packageJson = JsonConvert.DeserializeObject<PackageJson>(json);
        return packageJson ?? new PackageJson();
    }

    public static
        async Task<List<string>>
        PackageNamesFromPackageJson(string jsonOrPath)
    {
        if (File.Exists(jsonOrPath))
            jsonOrPath =
                await
                    FileAsync.ReadAllTextAsync(jsonOrPath);
        var npmUrlPrefix = @"https://www.npmjs.com/package/";
        var packageJson = Parse(jsonOrPath);
        var result = new List<string>();
        if (packageJson.dependencies != null)
            foreach (var item in packageJson.dependencies) result.Add(npmUrlPrefix + item.Key);
        if (packageJson.devDependencies != null)
            foreach (var item in packageJson.devDependencies) result.Add(npmUrlPrefix + item.Key);
        return result;
    }

    public static
        async Task
        OpenPackagesFromPackageJsonFromNpm(string jsonOrPath, Action<string> openInBrowser,
            string cdnProviderUrl, Func<string, string, string> urlReplacementFunction)
    {
        if (File.Exists(jsonOrPath))
            jsonOrPath =
                await
                    FileAsync.ReadAllTextAsync(jsonOrPath);
        var packageJson = Parse(jsonOrPath);
        if (packageJson.dependencies != null)
            foreach (var item in packageJson.dependencies)
                openInBrowser(urlReplacementFunction.Invoke(cdnProviderUrl, item.Key));
        if (packageJson.devDependencies != null)
            foreach (var item in packageJson.devDependencies)
                openInBrowser(urlReplacementFunction.Invoke(cdnProviderUrl, item.Key));
    }
}
