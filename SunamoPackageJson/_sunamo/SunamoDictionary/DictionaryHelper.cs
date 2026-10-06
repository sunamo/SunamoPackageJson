namespace SunamoPackageJson._sunamo.SunamoDictionary;

internal class DictionaryHelper
{
    internal static void AddOrCreate<TKey, TValue, TCollectionType>(IDictionary<TKey, List<TValue>> dict, TKey key, TValue value,
        bool isAvoidingDuplicates = false, Dictionary<TKey, List<string>>? stringComparisonDict = null) where TKey : notnull
    {
        var isComparingWithString = stringComparisonDict != null;

        if (key is IList && typeof(TCollectionType) != typeof(object))
        {
            var keyAsList = key as IList<TCollectionType>;
            var isKeyFound = false;
            foreach (var item in dict)
            {
                var currentKeyAsList = item.Key as IList<TCollectionType>;
                if (currentKeyAsList!.SequenceEqual(keyAsList!)) isKeyFound = true;
            }

            if (isKeyFound)
            {
                foreach (var item in dict)
                {
                    var currentKeyAsList = item.Key as IList<TCollectionType>;
                    if (currentKeyAsList!.SequenceEqual(keyAsList!))
                    {
                        if (isAvoidingDuplicates)
                            if (item.Value.Contains(value))
                                return;
                        item.Value.Add(value);
                    }
                }
            }
            else
            {
                List<TValue> newList = new();
                newList.Add(value);
                dict.Add(key, newList);

                if (isComparingWithString)
                {
                    List<string> newStringList = new();
                    newStringList.Add(value!.ToString()!);
                    stringComparisonDict!.Add(key, newStringList);
                }
            }
        }
        else
        {
            var shouldAdd = true;
            lock (dict)
            {
                if (dict.ContainsKey(key))
                {
                    if (isAvoidingDuplicates)
                    {
                        if (dict[key].Contains(value))
                            shouldAdd = false;
                        else if (isComparingWithString)
                            if (stringComparisonDict![key].Contains(value!.ToString()!))
                                shouldAdd = false;
                    }

                    if (shouldAdd)
                    {
                        var existingList = dict[key];

                        if (existingList != null) existingList.Add(value);

                        if (isComparingWithString)
                        {
                            var existingStringList = stringComparisonDict![key];

                            if (existingList != null) existingStringList.Add(value!.ToString()!);
                        }
                    }
                }
                else
                {
                    if (!dict.ContainsKey(key))
                    {
                        List<TValue> newList = new();
                        newList.Add(value);
                        dict.Add(key, newList);
                    }
                    else
                    {
                        dict[key].Add(value);
                    }

                    if (isComparingWithString)
                    {
                        if (!stringComparisonDict!.ContainsKey(key))
                        {
                            List<string> newStringList = new();
                            newStringList.Add(value!.ToString()!);
                            stringComparisonDict.Add(key, newStringList);
                        }
                        else
                        {
                            stringComparisonDict[key].Add(value!.ToString()!);
                        }
                    }
                }
            }
        }
    }

    internal static void AddOrCreate<TKey, TValue>(IDictionary<TKey, List<TValue>> dict, TKey key, TValue value,
        bool isAvoidingDuplicates = false, Dictionary<TKey, List<string>>? stringComparisonDict = null) where TKey : notnull
    {
        AddOrCreate<TKey, TValue, object>(dict, key, value, isAvoidingDuplicates, stringComparisonDict);
    }
}
