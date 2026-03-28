namespace SunamoPackageJson._sunamo.SunamoDictionary;

/// <summary>
/// Provides helper methods for dictionary operations with collection values.
/// </summary>
internal class DictionaryHelper
{
    /// <summary>
    /// Adds a value to the list associated with the given key, creating a new list if the key does not exist.
    /// Supports keys that are themselves lists (compared by sequence equality when <typeparamref name="TCollectionType"/> is not <see cref="object"/>).
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary lists.</typeparam>
    /// <typeparam name="TCollectionType">The element type when the key is a list; use <see cref="object"/> if the key is not a list.</typeparam>
    /// <param name="dict">The dictionary to add the value to.</param>
    /// <param name="key">The key under which to store the value.</param>
    /// <param name="value">The value to add.</param>
    /// <param name="isAvoidingDuplicates">If <c>true</c>, prevents adding duplicate values.</param>
    /// <param name="stringComparisonDict">Optional parallel dictionary for string-based duplicate comparison.</param>
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

    /// <summary>
    /// Adds a value to the list associated with the given key, creating a new list if the key does not exist.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the dictionary lists.</typeparam>
    /// <param name="dict">The dictionary to add the value to.</param>
    /// <param name="key">The key under which to store the value.</param>
    /// <param name="value">The value to add.</param>
    /// <param name="isAvoidingDuplicates">If <c>true</c>, prevents adding duplicate values.</param>
    /// <param name="stringComparisonDict">Optional parallel dictionary for string-based duplicate comparison.</param>
    internal static void AddOrCreate<TKey, TValue>(IDictionary<TKey, List<TValue>> dict, TKey key, TValue value,
        bool isAvoidingDuplicates = false, Dictionary<TKey, List<string>>? stringComparisonDict = null) where TKey : notnull
    {
        AddOrCreate<TKey, TValue, object>(dict, key, value, isAvoidingDuplicates, stringComparisonDict);
    }
}
