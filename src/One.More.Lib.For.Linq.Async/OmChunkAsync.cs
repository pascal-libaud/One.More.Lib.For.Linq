﻿namespace One.More.Lib.For.Linq.Async; 

public static partial class LinqAsyncHelper
{
    public static async IAsyncEnumerable<T[]> OmChunkAsync<T>(this IAsyncEnumerable<T> source, int size, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var list = new List<T>();
        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            list.Add(item);
            if (list.Count == size)
            {
                yield return list.ToArray();
                list.Clear();
            }
        }

        if (list.Count > 0)
            yield return list.ToArray();
    }
}