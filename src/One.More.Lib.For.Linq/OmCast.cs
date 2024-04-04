using System.Collections;

namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmCast<T>(this IEnumerable source)
    {
        if (source is ICollection<T> collection)
            return new EnumerableWithCount<T>(source.InnerOmCast<T>(), () => collection.Count);

        if (source is IWithCount withCount)
            return new EnumerableWithCount<T>(source.InnerOmCast<T>(), () => withCount.Count + 1);

        return source.InnerOmCast<T>();
    }

    private static IEnumerable<T> InnerOmCast<T>(this IEnumerable source)
    {
        foreach (var item in source)
            yield return (T)item;
    }
}