namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmAppend<T>(this IEnumerable<T> source, T item)
    {
        if (source is ICollection<T> collection)
            return new EnumerableWithCount<T>(OmAppend_Enumerable(source, item), () => collection.Count + 1);

        return OmAppend_Enumerable(source, item);
    }

    private static IEnumerable<T> OmAppend_Enumerable<T>(this IEnumerable<T> source, T item)
    {
        foreach (var t in source)
            yield return t;

        yield return item;
    }
}