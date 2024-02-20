﻿namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TResult>(this IAsyncEnumerable<T> source, Func<T, IEnumerable<TResult>> selector)
    {
        await foreach (var item in source)
            foreach (var result in selector(item))
                yield return result;
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TResult>(this IEnumerable<T> source, Func<T, Task<IEnumerable<TResult>>> selector)
    {
        foreach (var item in source)
            foreach (var result in await selector(item))
                yield return result;
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TResult>(this IAsyncEnumerable<T> source, Func<T, Task<IEnumerable<TResult>>> selector)
    {
        await foreach (var item in source)
            foreach (var result in await selector(item))
                yield return result;
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TResult>(this IEnumerable<T> source, Func<T, IAsyncEnumerable<TResult>> selector)
    {
        foreach (var item in source)
            await foreach (var result in selector(item))
                yield return result;
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TResult>(this IAsyncEnumerable<T> source, Func<T, IAsyncEnumerable<TResult>> selector)
    {
        await foreach (var item in source)
            await foreach (var result in selector(item))
                yield return result;
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector)
    {
        await foreach (var item in source)
            foreach (var result in collectionSelector(item))
                yield return resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, Task<IEnumerable<TCollection>>> collectionSelector, Func<T, TCollection, TResult> resultSelector)
    {
        foreach (var item in source)
            foreach (var result in await collectionSelector(item))
                yield return resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector)
    {
        foreach (var item in source)
            await foreach (var result in collectionSelector(item))
                yield return resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector)
    {
        await foreach (var item in source)
            await foreach (var result in collectionSelector(item))
                yield return resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector)
    {
        foreach (var item in source)
            foreach (var result in collectionSelector(item))
                yield return await resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector)
    {
        await foreach (var item in source)
            foreach (var result in collectionSelector(item))
                yield return await resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, Task<IEnumerable<TCollection>>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector)
    {
        foreach (var item in source)
            foreach (var result in await collectionSelector(item))
                yield return await resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector)
    {
        foreach (var item in source)
            await foreach (var result in collectionSelector(item))
                yield return await resultSelector(item, result);
    }

    public static async IAsyncEnumerable<TResult> OmSelectManyAsync<T, TCollection, TResult>(this IAsyncEnumerable<T> source, Func<T, IAsyncEnumerable<TCollection>> collectionSelector, Func<T, TCollection, Task<TResult>> resultSelector)
    {
        await foreach (var item in source)
            await foreach (var result in collectionSelector(item))
                yield return await resultSelector(item, result);
    }

}