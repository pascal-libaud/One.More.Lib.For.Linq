namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static List<T> OmToList<T>(this IEnumerable<T> source)
    {
        return new List<T>(source);
    }
}
