namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<(int Index, T Item)> OmIndex<T>(this IEnumerable<T> source)
    {
        return source.OmSelect((item, index) => (index, item));
    }
}