namespace SunamoPackageJson;

public class Dependency
{
    public Dependency(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public string Key { get; set; }
    public string Value { get; set; }
}
