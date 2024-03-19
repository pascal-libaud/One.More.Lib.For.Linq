namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async IAsyncEnumerable<TResult> OmCastAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TSource : notnull
    {
        await foreach (var item in source.WithCancellation(cancellationToken))
            yield return (TResult)(object)item;
    }
}