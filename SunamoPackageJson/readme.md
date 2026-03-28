### SunamoPackageJson

A .NET library for reading and generating package.json files.

Part of PlatformIndependentNuGetPackages:

- [nuget.org](https://www.nuget.org/profiles/sunamo)
- [github.org](https://github.com/sunamo/PlatformIndependentNuGetPackages)

Another links:

- [Developer site](https://sunamo.cz)

Request for new features / bug report / etc: [Mail](mailto:radek.jancik@sunamo.cz) or on GitHub

## Key Classes

- **Dependency** - Represents a single package dependency with a key (package name) and value (version).
- **PackageJson** - Data model for the package.json structure (dependencies, devDependencies, scripts, version, etc.).
- **PackageJsonHelper** - Utility methods for parsing package.json files and categorizing packages by version.

## Key Methods

- `PackageJson.GetVersionFromDepsOrDevDeps(packageName)` - Looks up a package version in dependencies or devDependencies.
- `PackageJsonHelper.Parse(json)` - Parses a JSON string into a PackageJson object.
- `PackageJsonHelper.CategorizeByFirstNumberOfPackage(folder, packageName)` - Categorizes package.json files by major version of a given package.
- `PackageJsonHelper.PackageNamesFromPackageJson(jsonOrPath)` - Extracts all package names as npm URLs.

## Target Frameworks

**TargetFrameworks:** `net10.0;net9.0;net8.0`

## Dependencies

- **Newtonsoft.Json** (v13.0.4)
- **Microsoft.Extensions.Logging.Abstractions** (v10.0.2)
