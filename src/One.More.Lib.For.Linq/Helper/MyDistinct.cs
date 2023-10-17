﻿namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T> MyDistinct<T>(this IEnumerable<T> source)
    {
        var hash = new HashSet<T>();
        foreach (var item in source)
        {
            if (hash.Add(item))
                yield return item;
        }
    }
}