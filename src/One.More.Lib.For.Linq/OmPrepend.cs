namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmPrepend<T>(this IEnumerable<T> source, T item)
    {
        if (source is ICollection<T> collection)
            return new EnumerableWithCount<T>(source.InnerOmPrepend(item), () => collection.Count + 1);

        if (source is IWithCount withCount)
            return new EnumerableWithCount<T>(source.InnerOmPrepend(item), () => withCount.Count + 1);

        return source.InnerOmPrepend(item);
    }

    private static IEnumerable<T> InnerOmPrepend<T>(this IEnumerable<T> source, T item)
    {
        yield return item;

        foreach (var t in source)
            yield return t;
    }
}