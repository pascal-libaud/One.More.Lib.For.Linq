namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static T OmAggregate<T>(this IEnumerable<T> source, Func<T, T, T> func)
    {
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
            throw new InvalidOperationException("The sequence contains no element");

        T current = enumerator.Current;

        while (enumerator.MoveNext())
            current = func(current, enumerator.Current);

        return current;
    }

    public static TAccumulate OmAggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
    {
        var current = seed;

        foreach (var item in source)
            current = func(current, item);

        return current;
    }

    public static TResult OmAggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
    {
        var current = seed;

        foreach (var item in source)
            current = func(current, item);

        return resultSelector(current);
    }
}