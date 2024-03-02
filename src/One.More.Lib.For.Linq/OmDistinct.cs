namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmDistinct<T>(this IEnumerable<T> source)
    {
        var hash = new HashSet<T>();
        foreach (var item in source)
        {
            if (hash.Add(item))
                yield return item;
        }
    }
}