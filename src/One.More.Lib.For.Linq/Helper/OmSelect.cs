using System.Collections;

namespace One.More.Lib.For.Linq.Helper;

public static partial class LinqHelper
{
    public static IEnumerable<U> OmSelect<T, U>(this IEnumerable<T> source, Func<T, U> selector)
    {
        return EnumerationWayStrategy.FocusOn switch
        {
            EnumerationWay.Foreach => source.OmSelect_Foreach(selector),
            EnumerationWay.Enumerator => source.OmSelect_Enumerator(selector),
            _ => source.OmSelect_Foreach(selector)
        };
    }

    public static IEnumerable<U> OmSelect<T, U>(this IEnumerable<T> source, Func<T, int, U> selector)
    {
        int index = 0;
        foreach (var item in source)
            yield return selector(item, index++);
    }

    private static IEnumerable<U> OmSelect_Foreach<T, U>(this IEnumerable<T> source, Func<T, U> selector)
    {
        foreach (var item in source)
            yield return selector(item);
    }

    private static IEnumerable<U> OmSelect_Enumerator<T, U>(this IEnumerable<T> source, Func<T, U> selector)
    {
        return new SelectEnumerable<T, U>(source, selector);
    }
}

file class SelectEnumerable<T, U> : IEnumerable<U>
{
    private readonly IEnumerable<T> _collection;
    private readonly Func<T, U> _selector;

    public SelectEnumerable(IEnumerable<T> collection, Func<T, U> selector)
    {
        _collection = collection;
        _selector = selector;
    }

    public IEnumerator<U> GetEnumerator()
    {
        return new SelectEnumerator<T, U>(_collection.GetEnumerator(), _selector);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

file class SelectEnumerator<T, U> : IEnumerator<U>
{
    private readonly IEnumerator<T> _enumerator;
    private readonly Func<T, U> _selector;

    public SelectEnumerator(IEnumerator<T> enumerator, Func<T, U> selector)
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

    public U Current => _selector(_enumerator.Current);

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}