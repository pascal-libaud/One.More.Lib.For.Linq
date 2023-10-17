namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T> Repeat<T>(T item, int count)
    {
        for (int i = 0; i < count; i++)
            yield return item;
    }
}