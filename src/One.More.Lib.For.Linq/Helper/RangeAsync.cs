namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static async IAsyncEnumerable<T> RangeAsync<T>(T count) where T : INumber<T>
    {
        await Task.Yield();
        for (T i = T.Zero; i < count; i++)
            yield return i;
    }

    public static async IAsyncEnumerable<T> RangeAsync<T>(T start, T count) where T : INumber<T>
    {
        await Task.Yield();
        var end = start + count;
        for (T i = start; i < end; i++)
            yield return i;
    }

    public static async IAsyncEnumerable<T?> RangeNullableAsync<T>(T count) where T : INumber<T>
    {
        // TODO: Can be simplified by using RangeAsync
        await Task.Yield();
        for (T i = T.Zero; i < count; i++)
            yield return i;
    }

    public static async IAsyncEnumerable<T?> RangeNullableAsync<T>(T start, T count) where T : struct, INumber<T>
    {
        // TODO: Can be simplified by using RangeAsync
        await Task.Yield();
        var end = start + count;
        for (T i = start; i < end; i++)
            yield return i;
    }
}