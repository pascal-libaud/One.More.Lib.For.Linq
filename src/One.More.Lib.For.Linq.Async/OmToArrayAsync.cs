namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async Task<T[]> OmToArrayAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        return (await source.OmToListAsync(cancellationToken)).ToArray();
    }
}