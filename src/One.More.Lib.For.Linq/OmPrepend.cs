namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmPrepend<T>(this IEnumerable<T> source, T item)
    {
        yield return item;

        foreach (var t in source)
            yield return t;
    }
}