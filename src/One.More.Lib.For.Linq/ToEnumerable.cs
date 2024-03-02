namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> ToEnumerable<T>(this T value)
    {
        yield return value;
    }
}