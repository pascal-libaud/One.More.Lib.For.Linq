﻿using System.Collections;

namespace One.More.Lib.For.Linq;

public interface IOmOrderedEnumerable<out T> : IEnumerable<T> { }

public static partial class LinqHelper
{
    public static IOmOrderedEnumerable<TSource> OmOrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        return new OmOrderedEnumerable<TSource, TKey>(source, keySelector, null);
    }

    public static IOmOrderedEnumerable<TSource> OmThenBy<TSource, TKey>(this IOmOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        return new OmOrderedEnumerable<TSource, TKey>(source, keySelector, source as OmOrderedEnumerable<TSource>);
    }
}

file abstract class OmOrderedEnumerable<T> : IOmOrderedEnumerable<T>
{
    internal abstract int Compare(T x, T y);

    public abstract IEnumerator<T> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

file class OmOrderedEnumerable<T, U> : OmOrderedEnumerable<T>
{
    private readonly Func<T, U> _keySelector;
    private readonly OmOrderedEnumerable<T>? _parent;
    private readonly List<T> orderedList;

    public OmOrderedEnumerable(IEnumerable<T> source, Func<T, U> keySelector, OmOrderedEnumerable<T>? parent)
    {
        _keySelector = keySelector;
        _parent = parent;

        orderedList = new List<T>(source);
        orderedList.Sort(Compare);
    }

    internal override int Compare(T x, T y)
    {
        int result = 0;
        if (_parent != null)
            result = _parent.Compare(x, y);

        if (result == 0)
            result = Comparer<U>.Default.Compare(_keySelector(x), _keySelector(y));

        return result;
    }

    public override IEnumerator<T> GetEnumerator()
    {
        return orderedList.GetEnumerator();
    }
}