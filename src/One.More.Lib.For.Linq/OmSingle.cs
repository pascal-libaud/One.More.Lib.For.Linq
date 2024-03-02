namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static T OmSingle<T>(this IEnumerable<T> source)
    {
        return EnumerationWayStrategy.FocusOn switch
        {
            EnumerationWay.Foreach => source.OmSingle_Foreach(),
            EnumerationWay.Enumerator => source.OmSingle_Enumerator(),
            _ => source.OmSingle_Foreach()
        };
    }

    public static T OmSingle<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        bool found = false;
        T candidate = default!;
        foreach (var item in source)
            if (predicate(item))
                if (!found)
                {
                    found = true;
                    candidate = item;
                }
                else
                    throw new InvalidOperationException("Sequence contains more than one matching element");

        if (!found)
            throw new InvalidOperationException("Sequence contains no matching element");

        return candidate!;
    }

    private static T OmSingle_Enumerator<T>(this IEnumerable<T> source)
    {
        using var enumerator = source.GetEnumerator();
        if (enumerator.MoveNext())
        {
            var candidate = enumerator.Current;

            if (enumerator.MoveNext())
                throw new InvalidOperationException("Sequence contains more than one element");

            return candidate;
        }

        throw new InvalidOperationException("Sequence contains no elements");
    }

    private static T OmSingle_Foreach<T>(this IEnumerable<T> source)
    {
        bool found = false;
        T candidate = default!;
        foreach (var item in source)
            if (!found)
            {
                found = true;
                candidate = item;
            }
            else
                throw new InvalidOperationException("Sequence contains more than one element");

        if (!found)
            throw new InvalidOperationException("Sequence contains no elements");

        return candidate!;
    }
}