namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T> Empty<T>()
    {
        yield break;
    }
}