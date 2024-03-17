namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async IAsyncEnumerable<T> OmAppendAsync<T>(this IAsyncEnumerable<T> source, T item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var t in source.WithCancellation(cancellationToken))
            yield return t;

        yield return item;
    }

    public static async IAsyncEnumerable<T> OmAppendAsync<T>(this IEnumerable<T> source, Task<T> item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var t in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return t;
        }

        yield return await item;
    }

    public static async IAsyncEnumerable<T> OmAppendAsync<T>(this IAsyncEnumerable<T> source, Task<T> item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var t in source.WithCancellation(cancellationToken))
            yield return t;

        yield return await item;
    }
}