namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async IAsyncEnumerable<T> InfiniteIteratorAsync<T>([EnumeratorCancellation] CancellationToken cancellation = default) where T : INumber<T>
    {
        T increment = T.Zero;
        while (true)
        {
            await Task.Yield();
            cancellation.ThrowIfCancellationRequested();
            yield return increment++;
        }
    }

    public static async IAsyncEnumerable<T> InfiniteIteratorAsync<T>(T start, [EnumeratorCancellation] CancellationToken cancellation = default) where T : INumber<T>
    {
        while (true)
        {
            await Task.Yield();
            cancellation.ThrowIfCancellationRequested();
            yield return start++;
        }
    }
}