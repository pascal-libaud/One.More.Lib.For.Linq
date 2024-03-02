namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static T? OmFirstOrDefault<T>(this IEnumerable<T> source)
    {
        return EnumerationWayStrategy.FocusOn switch
        {
            EnumerationWay.Foreach => source.OmFirstOrDefault_Foreach(),
            EnumerationWay.Enumerator => source.OmFirstOrDefault_Enumerator(),
            _ => source.OmFirstOrDefault_Foreach()
        };
    }

    public static T? OmFirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
            if (predicate(item))
                return item;

        return default;
    }

    private static T? OmFirstOrDefault_Enumerator<T>(this IEnumerable<T> source)
    {
        using var enumerator = source.GetEnumerator();
        if(enumerator.MoveNext())
            return enumerator.Current;

        return default;
    }

    private static T? OmFirstOrDefault_Foreach<T>(this IEnumerable<T> source)
    {
        foreach (var item in source)
            return item;

        return default;
    }
}