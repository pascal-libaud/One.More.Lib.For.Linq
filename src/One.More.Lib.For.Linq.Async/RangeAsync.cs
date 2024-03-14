namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async IAsyncEnumerable<T> RangeAsync<T>(T count, [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : INumber<T>
    {
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();

        for (T i = T.Zero; i < count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }

    public static async IAsyncEnumerable<T> RangeAsync<T>(T start, T count, [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : INumber<T>
    {
        await Task.Yield();
        var end = start + count;
        cancellationToken.ThrowIfCancellationRequested();

        for (T i = start; i < end; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }

    public static async IAsyncEnumerable<T?> RangeNullableAsync<T>(T count, [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : INumber<T>
    {
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();

        for (T i = T.Zero; i < count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }

    public static async IAsyncEnumerable<T?> RangeNullableAsync<T>(T start, T count, [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : INumber<T>
    {
        await Task.Yield();
        var end = start + count;
        cancellationToken.ThrowIfCancellationRequested();

        for (T i = start; i < end; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return i;
        }
    }
}