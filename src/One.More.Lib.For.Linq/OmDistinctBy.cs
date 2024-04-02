namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<TSource> OmDistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
    {
        var hash = new HashSet<TKey>();
        foreach (var item in source)
        {
            if (hash.Add(selector(item)))
                yield return item;
        }
    }
}