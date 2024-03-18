namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static Task<T> OmFirstAsync<T>(this IAsyncEnumerable<T> source)
    {
        return InternalAsyncStrategy.Selected switch
        {
            AsyncEnumerationWay.Foreach => source.OmFirstAsync_Foreach(),
            AsyncEnumerationWay.Enumerator => source.OmFirstAsync_Enumerator(),
            _ => source.OmFirstAsync_Foreach()
        };
    }

    public static async Task<T> OmFirstAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate)
    {
        await foreach (var item in source)
            if (predicate(item))
                return item;

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    public static async Task<T> OmFirstAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        foreach (var item in source)
            if (await predicate(item))
                return item;

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    public static async Task<T> OmFirstAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        await foreach (var item in source)
            if (await predicate(item))
                return item;

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    private static async Task<T> OmFirstAsync_Enumerator<T>(this IAsyncEnumerable<T> source)
    {
        await using var enumerator = source.GetAsyncEnumerator();
        if (await enumerator.MoveNextAsync())
            return enumerator.Current;

        throw new InvalidOperationException("Sequence contains no elements");
    }

    private static async Task<T> OmFirstAsync_Foreach<T>(this IAsyncEnumerable<T> source)
    {
        await foreach (var item in source)
            return item;

        throw new InvalidOperationException("Sequence contains no elements");
    }
}