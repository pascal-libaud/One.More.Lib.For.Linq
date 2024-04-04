namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmSkipLast<T>(this IEnumerable<T> source, int count)
    {
        if (source is ICollection<T> collection)
            return new EnumerableWithCount<T>(source.InnerOmSkipLast(count), () => Math.Max(collection.Count - count, 0));

        if (source is IWithCount withCount)
            return new EnumerableWithCount<T>(source.InnerOmSkipLast(count), () => Math.Max(withCount.Count - count, 0));

        return source.InnerOmSkipLast(count);
    }

    private static IEnumerable<T> InnerOmSkipLast<T>(this IEnumerable<T> source, int count)
    {
        Queue<T> queue = new Queue<T>();

        foreach (var item in source)
        {
            queue.Enqueue(item);
            if (queue.Count > count)
                yield return queue.Dequeue();
        }
    }
}