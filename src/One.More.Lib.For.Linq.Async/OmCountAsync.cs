namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async Task<int> OmCountAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        int result = 0;
        await foreach (var _ in source.WithCancellation(cancellationToken))
            result++;

        return result;
    }

    public static async Task<int> OmCountAsync<T>(this IAsyncEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        int result = 0;
        await foreach (var t in source.WithCancellation(cancellationToken))
            if (await predicate(t))
                result++;

        return result;
    }

    public static async Task<int> OmCountAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        int result = 0;
        await foreach (var t in source.WithCancellation(cancellationToken))
            if (predicate(t))
                result++;

        return result;
    }

    public static async Task<int> OmCountAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate, CancellationToken cancellationToken = default)
    {
        int result = 0;
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var t in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (await predicate(t))
                result++;
        }

        return result;
    }
}