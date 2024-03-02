namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> Empty<T>()
    {
        yield break;
    }
}