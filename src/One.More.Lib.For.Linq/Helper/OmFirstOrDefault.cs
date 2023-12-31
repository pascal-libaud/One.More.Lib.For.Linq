﻿namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static T? OmFirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
            if (predicate(item))
                return item;

        return default;
    }
}