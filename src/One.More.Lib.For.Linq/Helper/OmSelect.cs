namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<U> OmSelect<T, U>(this IEnumerable<T> source, Func<T, U> selector)
    {
        foreach (var item in source)
            yield return selector(item);
    }

    public static IEnumerable<U> OmSelect<T, U>(this IEnumerable<T> source, Func<T, int, U> selector)
    {
        int index = 0;
        foreach (var item in source)
            yield return selector(item, index++);
    }
}