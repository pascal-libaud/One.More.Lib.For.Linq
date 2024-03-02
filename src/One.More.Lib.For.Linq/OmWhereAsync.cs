namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static async IAsyncEnumerable<T> OmWhereAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        await foreach (var item in source)
            if (await predicate(item))
                yield return item;
    }

    public static async IAsyncEnumerable<T> OmWhereAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate)
    {
        await foreach (var item in source)
            if (predicate(item))
                yield return item;

    }

    public static async IAsyncEnumerable<T> OmWhereAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        foreach (var item in source)
            if (await predicate(item))
                yield return item;
    }
}