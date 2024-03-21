namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static Task<T> OmFirstAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        return InternalAsyncStrategy.Selected switch
        {
            AsyncEnumerationWay.Foreach => source.OmFirstAsync_Foreach(cancellationToken),
            AsyncEnumerationWay.Enumerator => source.OmFirstAsync_Enumerator(cancellationToken),
            _ => source.OmFirstAsync_Foreach(cancellationToken)
        };
    }

    public static async Task<T> OmFirstAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            if (predicate(item))
                return item;

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    public static async Task<T> OmFirstAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (await predicate(item))
                return item;
        }

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    public static async Task<T> OmFirstAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            if (await predicate(item))
                return item;

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    private static async Task<T> OmFirstAsync_Enumerator<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        await using var enumerator = source.GetAsyncEnumerator(cancellationToken);
        if (await enumerator.MoveNextAsync())
            return enumerator.Current;

        throw new InvalidOperationException("Sequence contains no elements");
    }

    private static async Task<T> OmFirstAsync_Foreach<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            return item;

        throw new InvalidOperationException("Sequence contains no elements");
    }
}