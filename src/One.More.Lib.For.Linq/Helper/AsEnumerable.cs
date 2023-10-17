namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T> AsEnumerable<T>(this T value)
    {
        yield return value;
    }
}