namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static bool OmAll<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
            if (!predicate(item))
                return false;

        return true;
    }
}