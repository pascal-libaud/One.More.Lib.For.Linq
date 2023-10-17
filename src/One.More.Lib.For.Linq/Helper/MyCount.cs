namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static int MyCount<T>(this IEnumerable<T> source)
    {
        int result = 0;
        foreach (var _ in source)
            result++;

        return result;
    }

    public static int MyCount<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        int result = 0;
        foreach (var t in source)
            if (predicate(t))
                result++;

        return result;
    }
}