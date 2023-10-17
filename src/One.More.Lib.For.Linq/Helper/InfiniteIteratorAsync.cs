namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static async IAsyncEnumerable<T> InfiniteIteratorAsync<T>() where T : INumber<T>
    {
        T increment = T.Zero;
        while (true)
        {
            await Task.Yield();
            yield return increment++;
        }
    }
}