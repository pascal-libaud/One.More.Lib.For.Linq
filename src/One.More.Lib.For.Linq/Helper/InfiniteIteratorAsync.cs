namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IAsyncEnumerable<T> InfiniteIteratorAsync<T>() where T : INumber<T>
    {
        return InfiniteIteratorAsync(T.Zero);
    }

    public static async IAsyncEnumerable<T> InfiniteIteratorAsync<T>(T start) where T : INumber<T>
    {
        while (true)
        {
            await Task.Yield();
            yield return start++;
        }
    }

}