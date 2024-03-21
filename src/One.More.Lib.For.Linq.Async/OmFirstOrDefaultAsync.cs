namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static Task<T?> OmFirstOrDefaultAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        return InternalAsyncStrategy.Selected switch
        {
            AsyncEnumerationWay.Foreach => source.OmFirstOrDefaultAsync_Foreach(cancellationToken),
            AsyncEnumerationWay.Enumerator => source.OmFirstOrDefaultAsync_Enumerator(cancellationToken),
            _ => source.OmFirstOrDefaultAsync_Foreach(cancellationToken)
        };
    }

    public static async Task<T?> OmFirstOrDefaultAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            if (predicate(item))
                return item;

        return default;
    }

    public static async Task<T?> OmFirstOrDefaultAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (await predicate(item))
                return item;
        }

        return default;
    }

    public static async Task<T?> OmFirstOrDefaultAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            if (await predicate(item))
                return item;

        return default;
    }

    private static async Task<T?> OmFirstOrDefaultAsync_Enumerator<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        await using var enumerator = source.GetAsyncEnumerator(cancellationToken);
        if (await enumerator.MoveNextAsync())
            return enumerator.Current;

        return default;
    }

    private static async Task<T?> OmFirstOrDefaultAsync_Foreach<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            return item;

        return default;
    }
}