﻿namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmAppend<T>(this IEnumerable<T> source, T item)
    {
        foreach(var t in source)
            yield return t;

        yield return item;
    }
}