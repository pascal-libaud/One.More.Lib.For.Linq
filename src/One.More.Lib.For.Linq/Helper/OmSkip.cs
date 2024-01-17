namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmSkip<T>(this IEnumerable<T> source, int count)
    {
        return EnumerationWayStrategy.FocusOn switch
        {
            EnumerationWay.Foreach => OmSkip_Foreach(source, count),
            EnumerationWay.Enumerator => OmSkip_Enumerator(source, count),
            _ => OmSkip_Foreach(source, count)
        };
    }

    private static IEnumerable<T> OmSkip_Foreach<T>(this IEnumerable<T> source, int count)
    {
        foreach (var item in source)
        {
            if (count > 0)
                count--;
            else
                yield return item;
        }
    }

    private static IEnumerable<T> OmSkip_Enumerator<T>(this IEnumerable<T> source, int count)
    {
        using var enumerator = source.GetEnumerator();
        while (count-- > 0)
            if (!enumerator.MoveNext())
                yield break;

        while (enumerator.MoveNext())
            yield return enumerator.Current;
    }
}