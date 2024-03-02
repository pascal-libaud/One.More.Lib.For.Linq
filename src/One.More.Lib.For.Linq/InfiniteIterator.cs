namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> InfiniteIterator<T>() where T : INumber<T>
    {
        return InfiniteIterator(T.Zero);
    }

    public static IEnumerable<T> InfiniteIterator<T>(T start) where T : INumber<T>
    {
        while (true)
            yield return start++;
    }
}