namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static T OmFirst<T>(this IEnumerable<T> source)
    {
        return InternalStrategy.Selected switch
        {
            EnumerationWay.Foreach => source.OmFirst_Foreach(),
            EnumerationWay.Enumerator => source.OmFirst_Enumerator(),
            _ => source.OmFirst_Foreach()
        };
    }

    public static T OmFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
            if (predicate(item))
                return item;

        throw new InvalidOperationException("Sequence contains no matching element");
    }

    private static T OmFirst_Enumerator<T>(this IEnumerable<T> source)
    {
        using var enumerator = source.GetEnumerator();
        if (enumerator.MoveNext())
            return enumerator.Current;

        throw new InvalidOperationException("Sequence contains no elements");
    }

    private static T OmFirst_Foreach<T>(this IEnumerable<T> source)
    {
        foreach (var item in source)
            return item;

        throw new InvalidOperationException("Sequence contains no elements");
    }
}