namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async Task<bool> OmIsEmptyAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        await foreach (var _ in source.WithCancellation(cancellationToken))
            return false;

        return true;
    }
}