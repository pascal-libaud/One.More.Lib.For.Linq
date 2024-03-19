namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async IAsyncEnumerable<TResult> OmOfTypeAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            if (item is TResult result)
                yield return result;
    }
}