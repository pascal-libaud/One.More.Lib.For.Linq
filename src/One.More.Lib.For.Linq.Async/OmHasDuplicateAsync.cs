namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async Task<bool> OmHasDuplicateAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        var hash = new HashSet<T>();
        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            if (!hash.Add(item))
                return true;
        }

        return false;
    }

    public static async Task<bool> OmHasDuplicate<T, U>(this IAsyncEnumerable<T> source, Func<T, U> selector, CancellationToken cancellationToken = default)
    {
        var hash = new HashSet<U>();
        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            if (!hash.Add(selector(item)))
                return true;
        }

        return false;
    }

    public static async Task<bool> OmHasDuplicate<T, U>(this IEnumerable<T> source, Func<T, Task<U>> selector, CancellationToken cancellationToken = default)
    {
        var hash = new HashSet<U>();
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!hash.Add(await selector(item)))
                return true;
        }

        return false;
    }

    public static async Task<bool> OmHasDuplicate<T, U>(this IAsyncEnumerable<T> source, Func<T, Task<U>> selector, CancellationToken cancellationToken = default)
    {
        var hash = new HashSet<U>();
        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            if (!hash.Add(await selector(item)))
                return true;
        }

        return false;
    }
}