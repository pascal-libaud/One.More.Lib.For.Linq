namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static async IAsyncEnumerable<T> EmptyAsync<T>()
    {
        await Task.Yield();
        yield break;
    }
}