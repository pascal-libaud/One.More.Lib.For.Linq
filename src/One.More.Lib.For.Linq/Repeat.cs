namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> Repeat<T>(T item, int count)
    {
        return new EnumerableWithCount<T>(InnerRepeat(item, count), () => count);
    }

    private static IEnumerable<T> InnerRepeat<T>(T item, int count)
    {
        for (int i = 0; i < count; i++)
            yield return item;
    }
}