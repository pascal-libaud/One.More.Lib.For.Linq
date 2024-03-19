namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static Task<T?> OmFirstOrDefaultAsync<T>(this IAsyncEnumerable<T> source)
    {
        return InternalAsyncStrategy.Selected switch
        {
            AsyncEnumerationWay.Foreach => source.OmFirstOrDefaultAsync_Foreach(),
            AsyncEnumerationWay.Enumerator => source.OmFirstOrDefaultAsync_Enumerator(),
            _ => source.OmFirstOrDefaultAsync_Foreach()
        };
    }

    public static async Task<T?> OmFirstOrDefaultAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate)
    {
        await foreach (var item in source)
            if (predicate(item))
                return item;

        return default;
    }

    public static async Task<T?> OmFirstOrDefaultAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        foreach (var item in source)
            if (await predicate(item))
                return item;

        return default;
    }

    public static async Task<T?> OmFirstOrDefaultAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        await foreach (var item in source)
            if (await predicate(item))
                return item;

        return default;
    }

    private static async Task<T?> OmFirstOrDefaultAsync_Enumerator<T>(this IAsyncEnumerable<T> source)
    {
        await using var enumerator = source.GetAsyncEnumerator();
        if(await enumerator.MoveNextAsync())
            return enumerator.Current;

        return default;
    }

    private static async Task<T?> OmFirstOrDefaultAsync_Foreach<T>(this IAsyncEnumerable<T> source)
    {
        await foreach (var item in source)
            return item;

        return default;
    }
}