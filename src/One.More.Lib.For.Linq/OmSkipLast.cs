namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmSkipLast<T>(this IEnumerable<T> source, int count)
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