namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async IAsyncEnumerable<T> OmPrependAsync<T>(this IAsyncEnumerable<T> source, T item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        yield return item;

        await foreach (var t in source.WithCancellation(cancellationToken))
            yield return t;
    }

    public static async IAsyncEnumerable<T> OmPrependAsync<T>(this IEnumerable<T> source, Task<T> item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        yield return await item;

        foreach (var t in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return t;
        }
    }

    public static async IAsyncEnumerable<T> OmPrependAsync<T>(this IAsyncEnumerable<T> source, Task<T> item, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        yield return await item;

        await foreach (var t in source.WithCancellation(cancellationToken))
            yield return t;
    }
}