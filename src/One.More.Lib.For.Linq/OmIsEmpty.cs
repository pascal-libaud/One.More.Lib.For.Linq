namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static bool OmIsEmpty<T>(this IEnumerable<T> source)
    {
        foreach (var _ in source)
            return false;

        return true;
    }
}