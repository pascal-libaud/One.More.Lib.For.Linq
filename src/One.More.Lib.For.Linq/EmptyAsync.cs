namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static async IAsyncEnumerable<T> EmptyAsync<T>()
    {
        await Task.Yield();
        yield break;
    }
}