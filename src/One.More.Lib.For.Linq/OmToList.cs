namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static List<T> OmToList<T>(this IEnumerable<T> source)
    {
        return new List<T>(source);
    }
}
