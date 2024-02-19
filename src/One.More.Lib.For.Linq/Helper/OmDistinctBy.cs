namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmDistinctBy<T, U>(this IEnumerable<T> source, Func<T, U> selector)
    {
        var hash = new HashSet<U>();
        foreach (var item in source)
        {
            if (hash.Add(selector(item)))
                yield return item;
        }
    }
}