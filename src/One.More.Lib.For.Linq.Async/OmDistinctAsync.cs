namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static IAsyncEnumerable<T> OmDistinctAsync<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken = default)
    {
        return InternalAsyncStrategy.Selected switch
        {
            AsyncEnumerationWay.Foreach => OmDistinctAsync_Foreach(source, cancellationToken),
            AsyncEnumerationWay.Enumerator => OmDistinctAsync_Enumerator(source, cancellationToken),
            _ => OmDistinctAsync_Foreach(source, cancellationToken)
        };
    }

    private static async IAsyncEnumerable<T> OmDistinctAsync_Foreach<T>(this IAsyncEnumerable<T> source, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var hash = new HashSet<T>();

        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            if (hash.Add(item))
                yield return item;
        }
    }

    private static async IAsyncEnumerable<T> OmDistinctAsync_Enumerator<T>(this IAsyncEnumerable<T> source, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var hash = new HashSet<T>();

        await using var enumerator = source.GetAsyncEnumerator(cancellationToken);
        while (await enumerator.MoveNextAsync())
        {
            if (hash.Add(enumerator.Current))
                yield return enumerator.Current;
        }
    }
}