namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<TResult> OmSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        if (source is ICollection<TSource> collection)
            return new EnumerableWithCount<TResult>(source.InnerOmSelect(selector), () => collection.Count);

        return InnerOmSelect(source, selector);
    }

    public static IEnumerable<TResult> OmSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
    {
        int index = 0;
        foreach (var item in source)
            yield return selector(item, index++);
    }
    
    private static IEnumerable<TResult> InnerOmSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        return InternalStrategy.Selected switch
        {
            EnumerationWay.Foreach => source.OmSelect_Foreach(selector),
            EnumerationWay.Enumerator => source.OmSelect_Enumerator(selector),
            _ => source.OmSelect_Foreach(selector)
        };
    }

    private static IEnumerable<TResult> OmSelect_Foreach<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        foreach (var item in source)
            yield return selector(item);
    }

    private static IEnumerable<TResult> OmSelect_Enumerator<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        using var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
            yield return selector(enumerator.Current);
    }
}