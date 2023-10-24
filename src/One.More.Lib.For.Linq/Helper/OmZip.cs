namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<(T, U)> OmZip<T, U>(this IEnumerable<T> source1, IEnumerable<U> source2)
    {
        return source1.OmZip(source2, (t, u) => (t, u));
    }

    public static IEnumerable<V> OmZip<T, U, V>(this IEnumerable<T> source1, IEnumerable<U> source2, Func<T, U, V> selector)
    {
        using var enumerator1 = source1.GetEnumerator();
        using var enumerator2 = source2.GetEnumerator();

        while (enumerator1.MoveNext() && enumerator2.MoveNext())
            yield return selector(enumerator1.Current, enumerator2.Current);
    }

    public static IEnumerable<V> OmZipFull<T, U, V>(this IEnumerable<T> source1, IEnumerable<U> source2, Func<T?, U?, V> selector)
    {
        using var enumerator1 = source1.GetEnumerator();
        using var enumerator2 = source2.GetEnumerator();

        while (true)
        {
            bool hasValue1 = enumerator1.MoveNext();
            var value1 = hasValue1 ? enumerator1.Current : default;

            bool hasValue2 = enumerator2.MoveNext();
            var value2 = hasValue2 ? enumerator2.Current : default;

            if (hasValue1 || hasValue2)
                yield return selector(value1, value2);
            else
                break;
        }
    }
}