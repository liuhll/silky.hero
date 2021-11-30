namespace System.Collections.Generic;

public static class EnumerableExtensions
{
    public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
    {
        var result = source.SelectMany(selector);
        if (result != null && !result.Any())
        {
            return result;
        }
        return result.Concat(result.SelectManyRecursive(selector));
    }
}