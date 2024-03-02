namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> Repeat<T>(T item, int count)
    {
        for (int i = 0; i < count; i++)
            yield return item;
    }
}