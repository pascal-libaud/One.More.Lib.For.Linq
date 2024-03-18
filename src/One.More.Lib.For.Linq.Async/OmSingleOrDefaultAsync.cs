namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static Task<T?> OmSingleOrDefaultAsync<T>(this IAsyncEnumerable<T> source)
    {
        return InternalAsyncStrategy.Selected switch
        {
            AsyncEnumerationWay.Foreach => source.OmSingleOrDefaultAsync_Foreach(),
            AsyncEnumerationWay.Enumerator => source.OmSingleOrDefaultAsync_Enumerator(),
            _ => source.OmSingleOrDefaultAsync_Foreach()
        };
    }

    public static async Task<T?> OmSingleOrDefaultAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate)
    {
        bool found = false;
        T candidate = default!;
        await foreach (var item in source)
            if (predicate(item))
                if (!found)
                {
                    found = true;
                    candidate = item;
                }
                else
                    throw new InvalidOperationException("Sequence contains more than one matching element");

        if (!found)
            return default;

        return candidate!;
    }

    public static async Task<T?> OmSingleOrDefaultAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        bool found = false;
        T candidate = default!;
        foreach (var item in source)
            if (await predicate(item))
                if (!found)
                {
                    found = true;
                    candidate = item;
                }
                else
                    throw new InvalidOperationException("Sequence contains more than one matching element");

        if (!found)
            return default;

        return candidate!;
    }

    public static async Task<T?> OmSingleOrDefaultAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate)
    {
        bool found = false;
        T candidate = default!;
        await foreach (var item in source)
            if (await predicate(item))
                if (!found)
                {
                    found = true;
                    candidate = item;
                }
                else
                    throw new InvalidOperationException("Sequence contains more than one matching element");

        if (!found)
            return default;

        return candidate!;
    }
    
    private static async Task<T?> OmSingleOrDefaultAsync_Enumerator<T>(this IAsyncEnumerable<T> source)
    {
        await using var enumerator = source.GetAsyncEnumerator();
        if (await enumerator.MoveNextAsync())
        {
            var candidate = enumerator.Current;

            if (await enumerator.MoveNextAsync())
                throw new InvalidOperationException("Sequence contains more than one element");

            return candidate;
        }

        return default;
    }

    private static async Task<T?> OmSingleOrDefaultAsync_Foreach<T>(this IAsyncEnumerable<T> source)
    {
        bool found = false;
        T candidate = default!;
        await foreach (var item in source)
            if (!found)
            {
                found = true;
                candidate = item;
            }
            else
                throw new InvalidOperationException("Sequence contains more than one element");

        if (!found)
            return default;

        return candidate!;
    }
}