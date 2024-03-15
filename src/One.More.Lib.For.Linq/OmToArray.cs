namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static T[] OmToArray<T>(this IEnumerable<T> source)
    {
        return source.OmToList().ToArray();
    }
}