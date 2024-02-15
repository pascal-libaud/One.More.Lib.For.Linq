﻿namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static async IAsyncEnumerable<T> OmPrependAsync<T>(this IAsyncEnumerable<T> source, T item)
    {
        yield return item;

        await foreach (var t in source)
            yield return t;
    }

    public static async IAsyncEnumerable<T> OmPrependAsync<T>(this IEnumerable<T> source, Task<T> item)
    {
        yield return await item;

        foreach (var t in source)
            yield return t;
    }

    public static async IAsyncEnumerable<T> OmPrependAsync<T>(this IAsyncEnumerable<T> source, Task<T> item)
    {
        yield return await item;

        await foreach (var t in source)
            yield return t;
    }
}