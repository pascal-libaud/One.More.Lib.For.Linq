namespace One.More.Lib.For.Linq.Async;

public static partial class LinqAsyncHelper
{
    public static async IAsyncEnumerable<T> EmptyAsync<T>()
    {
        await Task.Yield();
        yield break;
    }
}