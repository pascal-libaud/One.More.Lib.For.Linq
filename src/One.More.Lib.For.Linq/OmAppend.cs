using System.Collections;

namespace One.More.Lib.For.Linq;

public static partial class LinqHelper
{
    public static IEnumerable<T> OmAppend<T>(this IEnumerable<T> source, T item)
    {
        if (source is ICollection<T> collection)
            return new AppendCollection<T>(collection, item);

        return OmAppend_Enumerable(source, item);
    }

    private static IEnumerable<T> OmAppend_Enumerable<T>(this IEnumerable<T> source, T item)
    {
        foreach (var t in source)
            yield return t;

        yield return item;
    }
}

file class AppendCollection<T> : IEnumerable<T>, IWithCount
{
    private readonly ICollection<T> _collection;
    private readonly T _item;

    public AppendCollection(ICollection<T> collection, T item)
    {
        _collection = collection;
        _item = item;
    }

    public IEnumerator<T> GetEnumerator() => new AppendEnumerator<T>(_collection.GetEnumerator(), _item);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => _collection.Count + 1;
}

file class AppendEnumerator<T> : IEnumerator<T>
{
    private readonly IEnumerator<T> _enumerator;
    private readonly T _item;
    private bool _isEnumeratorEndReached = false;

    public AppendEnumerator(IEnumerator<T> enumerator, T item)
    {
        _enumerator = enumerator;
        _item = item;
    }

    public bool MoveNext()
    {
        if (!_isEnumeratorEndReached)
        {
            if (!_enumerator.MoveNext())
                _isEnumeratorEndReached = true;

            return true;
        }

        return false;
    }

    public void Reset()
    {
        _isEnumeratorEndReached = false;
        _enumerator.Reset();
    }

    public T Current => !_isEnumeratorEndReached ? _enumerator.Current : _item;

    object IEnumerator.Current => ((IEnumerator)_enumerator).Current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}