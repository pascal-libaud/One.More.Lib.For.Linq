using System.Collections;

namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<TResult> OmSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        if (source is ICollection<TSource> collection)
            return new SelectCollection<TSource, TResult>(collection, selector);

        return InternalStrategy.Selected switch
        {
            EnumerationWay.Foreach => source.OmSelect_Foreach(selector),
            EnumerationWay.Enumerator => source.OmSelect_Enumerator(selector),
            _ => source.OmSelect_Foreach(selector)
        };
    }

    public static IEnumerable<TResult> OmSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
    {
        int index = 0;
        foreach (var item in source)
            yield return selector(item, index++);
    }

    private static IEnumerable<TResult> OmSelect_Foreach<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        foreach (var item in source)
            yield return selector(item);
    }

    private static IEnumerable<TResult> OmSelect_Enumerator<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        return new SelectEnumerable<TSource, TResult>(source, selector);
    }
}

file class SelectCollection<TSource, TResult> : IEnumerable<TResult>, IWithCount
{
    private readonly ICollection<TSource> _collection;
    private readonly Func<TSource, TResult> _selector;

    public SelectCollection(ICollection<TSource> collection, Func<TSource, TResult> selector)
    {
        _collection = collection;
        _selector = selector;
    }

    public IEnumerator<TResult> GetEnumerator()
    {
        return new SelectEnumerator<TSource, TResult>(_collection.GetEnumerator(), _selector);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => _collection.Count;
}

file class SelectEnumerable<TSource, TResult> : IEnumerable<TResult>
{
    private readonly IEnumerable<TSource> _collection;
    private readonly Func<TSource, TResult> _selector;

    public SelectEnumerable(IEnumerable<TSource> collection, Func<TSource, TResult> selector)
    {
        _collection = collection;
        _selector = selector;
    }

    public IEnumerator<TResult> GetEnumerator()
    {
        return new SelectEnumerator<TSource, TResult>(_collection.GetEnumerator(), _selector);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

file class SelectEnumerator<TSource, TResult> : IEnumerator<TResult>
{
    private readonly IEnumerator<TSource> _enumerator;
    private readonly Func<TSource, TResult> _selector;

    public SelectEnumerator(IEnumerator<TSource> enumerator, Func<TSource, TResult> selector)
    {
        _enumerator = enumerator;
        _selector = selector;
    }

    public bool MoveNext()
    {
        return _enumerator.MoveNext();
    }

    public void Reset()
    {
        _enumerator.MoveNext();
    }

    public TResult Current => _selector(_enumerator.Current);

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}