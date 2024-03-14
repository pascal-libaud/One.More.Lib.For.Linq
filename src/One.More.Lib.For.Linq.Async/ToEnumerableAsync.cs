namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async IAsyncEnumerable<T> ToEnumerableAsync<T>(this T value)
    {
        await Task.Yield();
        yield return value;
    }
}