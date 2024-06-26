﻿namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmTake<T>(this IEnumerable<T> source, int count)
    {
        if (source is ICollection<T> collection)
            return new EnumerableWithCount<T>(source.InnerOmTake(count), () => Math.Min(collection.Count, count));

        if (source is IWithCount withCount)
            return new EnumerableWithCount<T>(source.InnerOmTake(count), () => Math.Min(withCount.Count, count));

        return source.InnerOmTake(count);
    }

    private static IEnumerable<T> InnerOmTake<T>(this IEnumerable<T> source, int count)
    {
        return InternalStrategy.Selected switch
        {
            EnumerationWay.Foreach => OmTake_Foreach(source, count),
            EnumerationWay.Enumerator => OmTake_Enumerator(source, count),
            _ => OmTake_Foreach(source, count)
        };
    }

    private static IEnumerable<T> OmTake_Foreach<T>(this IEnumerable<T> source, int count)
    {
        if (count > 0)
            foreach (var item in source)
            {
                yield return item;
                if (--count <= 0)
                    break;
            }
    }

    private static IEnumerable<T> OmTake_Enumerator<T>(this IEnumerable<T> source, int count)
    {
        if (count > 0)
        {
            using var enumerator = source.GetEnumerator();

            while (count-- > 0 && enumerator.MoveNext())
                yield return enumerator.Current;
        }
    }
}