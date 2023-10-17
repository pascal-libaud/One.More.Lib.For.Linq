namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T> MySkip<T>(this IEnumerable<T> source, int count)
    {
        // Version Enumerator
        //using var enumerator = source.GetEnumerator();
        //while (count-- > 0)
        //    if(!enumerator.MoveNext())
        //        yield break;

        //while (enumerator.MoveNext())
        //    yield return enumerator.Current;

        // Version foreach
        foreach (var item in source)
        {
            if (count > 0)
                count--;
            else
                yield return item;
        }
    }

    public static IEnumerable<T> MySkipLast<T>(this IEnumerable<T> source, int count)
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