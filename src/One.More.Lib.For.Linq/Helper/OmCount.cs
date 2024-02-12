namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static int OmCount<T>(this IEnumerable<T> source)
    {
        if (source is ICollection<T> collection)
            return collection.Count;

        if(source is T[] array)
            return array.Length;

        int result = 0;
        foreach (var _ in source)
            result++;

        return result;
    }

    public static int OmCount<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        int result = 0;
        foreach (var t in source)
            if (predicate(t))
                result++;

        return result;
    }
}