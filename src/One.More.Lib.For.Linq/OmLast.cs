namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static T OmLast<T>(this IEnumerable<T> source)
    {
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
            throw new InvalidOperationException("Sequence contains no elements");

        while (enumerator.MoveNext()) { }

        return enumerator.Current;
    }

    public static T OmLast<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        bool isFound = false;
        T candidate = default!;

        foreach (var item in source)
            if (predicate(item))
            {
                isFound = true;
                candidate = item;
            }

        if (isFound)
            return candidate!;

        throw new InvalidOperationException("Sequence contains no matching element");
    }
}