namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async IAsyncEnumerable<T> OmAppendAsync<T>(this IAsyncEnumerable<T> source, T item)
    {
        await foreach (var t in source)
            yield return t;

        yield return item;
    }

    public static async IAsyncEnumerable<T> OmAppendAsync<T>(this IEnumerable<T> source, Task<T> item)
    {
        foreach (var t in source)
            yield return t;

        yield return await item;
    }

    public static async IAsyncEnumerable<T> OmAppendAsync<T>(this IAsyncEnumerable<T> source, Task<T> item)
    {
        await foreach (var t in source)
            yield return t;

        yield return await item;
    }
}