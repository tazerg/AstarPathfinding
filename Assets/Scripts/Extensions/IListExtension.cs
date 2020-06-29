using System.Collections.Generic;

public static class IListExtension
{
    public static void AddIfNotNull<T>(this IList<T> collection, T value)
    {
        if (value != null)
            collection.Add(value);
    }
}
