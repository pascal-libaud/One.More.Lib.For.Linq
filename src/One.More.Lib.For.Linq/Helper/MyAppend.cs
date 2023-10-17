namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T> MyAppend<T>(this IEnumerable<T> source, T item)
    {
        foreach(var t in source)
            yield return t;

        yield return item;
    }
}