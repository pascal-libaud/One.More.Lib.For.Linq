namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> Empty<T>()
    {
        return new EnumerableWithCount<T>(InnerEmpty<T>(), () => 0);
    }

    private static IEnumerable<T> InnerEmpty<T>()
    {
        yield break;
    }
}