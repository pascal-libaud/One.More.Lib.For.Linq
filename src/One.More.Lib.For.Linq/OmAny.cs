namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static bool OmAny<T>(this IEnumerable<T> source)
    {
        foreach (var _ in source)
                return true;
        
        return false;
    }

    public static bool OmAny<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
            if (predicate(item))
                return true;

        return false;
    }
}