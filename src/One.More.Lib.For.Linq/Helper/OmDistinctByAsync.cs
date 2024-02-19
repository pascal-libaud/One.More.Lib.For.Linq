namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static async IAsyncEnumerable<T> OmDistinctByAsync<T, U>(this IAsyncEnumerable<T> source, Func<T, U> selector)
    {
        var hash = new HashSet<U>();
        await foreach (var item in source)
        {
            if (hash.Add(selector(item)))
                yield return item;
        }
    }

    public static async IAsyncEnumerable<T> OmDistinctByAsync<T, U>(this IEnumerable<T> source, Func<T, Task<U>> selector)
    {
        var hash = new HashSet<U>();
        foreach (var item in source)
        {
            if (hash.Add(await selector(item)))
                yield return item;
        }
    }

    public static async IAsyncEnumerable<T> OmDistinctByAsync<T, U>(this IAsyncEnumerable<T> source, Func<T, Task<U>> selector)
    {
        var hash = new HashSet<U>();
        await foreach (var item in source)
        {
            if (hash.Add(await selector(item)))
                yield return item;
        }
    }
}