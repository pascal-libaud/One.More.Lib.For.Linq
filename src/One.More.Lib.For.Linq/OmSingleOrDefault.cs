namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static T? OmSingleOrDefault<T>(this IEnumerable<T> source)
    {
        return EnumerationWayStrategy.FocusOn switch
        {
            EnumerationWay.Foreach => source.OmSingleOrDefault_Foreach(),
            EnumerationWay.Enumerator => source.OmSingleOrDefault_Enumerator(),
            _ => source.OmSingleOrDefault_Foreach()
        };
    }

    public static T? OmSingleOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
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
            return default;

        return candidate!;
    }

    private static T? OmSingleOrDefault_Enumerator<T>(this IEnumerable<T> source)
    {
        using var enumerator = source.GetEnumerator();
        if (enumerator.MoveNext())
        {
            var candidate = enumerator.Current;

            if (enumerator.MoveNext())
                throw new InvalidOperationException("Sequence contains more than one element");

            return candidate;
        }

        return default;
    }

    private static T? OmSingleOrDefault_Foreach<T>(this IEnumerable<T> source)
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
            return default;

        return candidate!;
    }
}