namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<(T?, U?)> FullOuterJoin<T, U>(this IEnumerable<T> source1, IEnumerable<U> source2, Func<T?, U?, int> comparer)
        where T : class
        where U : class
    {
        using var enumerator1 = source1.GetEnumerator();
        using var enumerator2 = source2.GetEnumerator();

        T? current1 = enumerator1.MoveNext() ? enumerator1.Current : default;
        U? current2 = enumerator2.MoveNext() ? enumerator2.Current : default;

        while (current1 != default || current2 != default)
        {
            var comparison = comparer(current1, current2);

            if (comparison == 0)
            {
                yield return (current1, current2);
                current1 = enumerator1.MoveNext() ? enumerator1.Current : default;
                current2 = enumerator2.MoveNext() ? enumerator2.Current : default;
            }
            else if (comparison < 0)
            {
                yield return (current1, default);
                current1 = enumerator1.MoveNext() ? enumerator1.Current : default;
            }
            else //if (comparison > 0)
            {
                yield return (default, current2);
                current2 = enumerator2.MoveNext() ? enumerator2.Current : default;
            }
        }
    }

    public static IEnumerable<(T?, T?)> FullOuterJoin<T>(this IEnumerable<T> source1, IEnumerable<T> source2, IComparer<T> comparer)
        where T : class
    {
        return source1.FullOuterJoin(source2, comparer.Compare);
    }
}
