namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static Task<T> OmSingleAsync<T>(this IAsyncEnumerable<T> source)
    {
        return InternalAsyncStrategy.Selected switch
        {
            AsyncEnumerationWay.Foreach => source.OmSingleAsync_Foreach(),
            AsyncEnumerationWay.Enumerator => source.OmSingleAsync_Enumerator(),
            _ => source.OmSingleAsync_Foreach()
        };
    }

    public static async Task<T> OmSingleAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate)
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
            throw new InvalidOperationException("Sequence contains no matching element");

        return candidate!;
    }

    public static async Task<T> OmSingleAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
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
            throw new InvalidOperationException("Sequence contains no matching element");

        return candidate!;
    }

    public static async Task<T> OmSingleAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate)
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
            throw new InvalidOperationException("Sequence contains no matching element");

        return candidate!;
    }

    private static async Task<T> OmSingleAsync_Enumerator<T>(this IAsyncEnumerable<T> source)
    {
        await using var enumerator = source.GetAsyncEnumerator();
        if (await enumerator.MoveNextAsync())
        {
            var candidate = enumerator.Current;

            if (await enumerator.MoveNextAsync())
                throw new InvalidOperationException("Sequence contains more than one element");

            return candidate;
        }

        throw new InvalidOperationException("Sequence contains no elements");
    }

    private static async Task<T> OmSingleAsync_Foreach<T>(this IAsyncEnumerable<T> source)
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
            throw new InvalidOperationException("Sequence contains no elements");

        return candidate!;
    }
}