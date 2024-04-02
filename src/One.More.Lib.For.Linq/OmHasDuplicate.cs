namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static bool OmHasDuplicate<T>(this IEnumerable<T> source)
    {
        var hash = new HashSet<T>();
        foreach (var item in source)
        {
            if (!hash.Add(item))
                return true;
        }

        return false;
    }

    public static bool OmHasDuplicate<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
    {
        var hash = new HashSet<TKey>();
        foreach (var item in source)
        {
            if (!hash.Add(selector(item)))
                return true;
        }

        return false;
    }
}