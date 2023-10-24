﻿namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<T> Range<T>(T count) where T : INumber<T>
    {
        for (T i = T.Zero; i < count; i++)
            yield return i;
    }

    public static IEnumerable<T> Range<T>(T start, T count) where T : INumber<T>
    {
        var end = start + count;
        for (T i = start; i < end; i++)
            yield return i;
    }

    public static IEnumerable<T?> RangeNullable<T>(T count) where T : INumber<T>
    {
        // TODO: Can be simplified by using Range
        for (T i = T.Zero; i < count; i++)
            yield return i;
    }

    public static IEnumerable<T?> RangeNullable<T>(T start, T count) where T : struct, INumber<T>
    {
        // TODO: Can be simplified by using Range
        var end = start + count;
        for (T i = start; i < end; i++)
            yield return i;
    }
}