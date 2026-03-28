namespace SunamoPackageJson;

/// <summary>
/// Provides utility methods for parsing and processing package.json files.
/// </summary>
public class PackageJsonHelper
{
    /// <summary>
    /// Categorizes package.json files found in the specified folder by the major version number of the given package.
    /// Files where the package is not found are categorized under key -1. Files with "latest" version are categorized under <see cref="int.MaxValue"/>.
    /// </summary>
    /// <param name="folder">The root folder to search for package.json files recursively.</param>
    /// <param name="packageName">The name of the package whose version is used for categorization.</param>
    /// <returns>A dictionary mapping major version numbers to lists of package.json file paths.</returns>
    public static
#if ASYNC
        async Task<Dictionary<int, List<string>>>
#else
        Dictionary<int, List<string>>
#endif
        CategorizeByFirstNumberOfPackage(string folder, string packageName)
    {
        Dictionary<int, List<string>> result = new();
        var packageJsonFiles = Directory.GetFiles(folder, "package.json", SearchOption.AllDirectories);
        foreach (var item in packageJsonFiles)
        {
            var packageJson = Parse(
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(item));
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

    /// <summary>
    /// Parses a JSON string into a <see cref="PackageJson"/> object.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <returns>The deserialized <see cref="PackageJson"/> object.</returns>
    public static PackageJson Parse(string json)
    {
        var packageJson = JsonConvert.DeserializeObject<PackageJson>(json);
        return packageJson ?? new PackageJson();
    }

    /// <summary>
    /// Extracts all package names from a package.json file or JSON string and returns them as npm URLs.
    /// </summary>
    /// <param name="jsonOrPath">Either a JSON string or a file path to a package.json file.</param>
    /// <returns>A list of npm package URLs for all dependencies and devDependencies.</returns>
    public static
#if ASYNC
        async Task<List<string>>
#else
    List<string>
#endif
        PackageNamesFromPackageJson(string jsonOrPath)
    {
        if (File.Exists(jsonOrPath))
            jsonOrPath =
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(jsonOrPath);
        var npmUrlPrefix = @"https://www.npmjs.com/package/";
        var packageJson = Parse(jsonOrPath);
        var result = new List<string>();
        if (packageJson.dependencies != null)
            foreach (var item in packageJson.dependencies) result.Add(npmUrlPrefix + item.Key);
        if (packageJson.devDependencies != null)
            foreach (var item in packageJson.devDependencies) result.Add(npmUrlPrefix + item.Key);
        return result;
    }

    /// <summary>
    /// Opens all dependency packages from a package.json file in a browser using the specified CDN provider.
    /// </summary>
    /// <param name="jsonOrPath">Either a JSON string or a file path to a package.json file.</param>
    /// <param name="openInBrowser">An action that opens the given URL in a browser.</param>
    /// <param name="cdnProviderUrl">The base URL of the CDN provider (e.g. unpkg).</param>
    /// <param name="urlReplacementFunction">A function that constructs the final URL from the CDN provider URL and the package name.</param>
    public static
#if ASYNC
        async Task
#else
    void
#endif
        OpenPackagesFromPackageJsonFromNpm(string jsonOrPath, Action<string> openInBrowser,
            string cdnProviderUrl, Func<string, string, string> urlReplacementFunction)
    {
        if (File.Exists(jsonOrPath))
            jsonOrPath =
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(jsonOrPath);
        var packageJson = Parse(jsonOrPath);
        if (packageJson.dependencies != null)
            foreach (var item in packageJson.dependencies)
                openInBrowser(urlReplacementFunction.Invoke(cdnProviderUrl, item.Key));
        if (packageJson.devDependencies != null)
            foreach (var item in packageJson.devDependencies)
                openInBrowser(urlReplacementFunction.Invoke(cdnProviderUrl, item.Key));
    }
}
