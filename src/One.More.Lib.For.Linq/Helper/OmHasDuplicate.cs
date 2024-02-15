namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static bool OmHasDuplicate<T>(this IEnumerable<T> source)
    {
        var hash = new HashSet<T>();
        foreach (var item in source)
        {
            if (!hash.Add(item))
                return true;
        }

        return false;
    }

    public static bool OmHasDuplicate<T, U>(this IEnumerable<T> source, Func<T, U> selector)
    {
        var hash = new HashSet<U>();
        foreach (var item in source)
        {
            if (!hash.Add(selector(item)))
                return true;
        }

        return false;
    }
}