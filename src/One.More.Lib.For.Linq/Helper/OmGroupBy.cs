using System.Collections;

namespace One.More.Lib.For.Linq.Helper;

public interface IOmGroup<out TKey, out TValue> : IEnumerable<TValue>
{
    TKey Key { get; }
}

public static partial class LinqHelper
{
    public static IEnumerable<IOmGroup<TKey, TValue>> OmGroupBy<TValue, TKey>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector)
        where TKey : notnull
    {
        return OmGroup(source, keySelector, null);
    }

    public static IEnumerable<IOmGroup<TKey, TValue>> OmGroup<TValue, TKey>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IEqualityComparer<TKey>? equalityComparer)
        where TKey : notnull
    {
        return OmGroupBy(source, keySelector, x => x, equalityComparer);
    }

    public static IEnumerable<IOmGroup<TKey, TResult>> OmGroupBy<TValue, TKey, TResult>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TResult> select)
        where TKey : notnull
    {
        return OmGroupBy(source, keySelector, select, null);
    }

    public static IEnumerable<IOmGroup<TKey, TResult>> OmGroupBy<TValue, TKey, TResult>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TResult> select, IEqualityComparer<TKey>? equalityComparer)
        where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> dictionary = new(equalityComparer);

        foreach (var item in source)
        {
            var key = keySelector(item);
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(item);
            else
                dictionary.Add(key, new List<TValue> { item });
        }

        foreach (var kv in dictionary.OmSelect(k => new OmGroup<TKey, TResult>(k.Key, k.Value.OmSelect(v => select(v)))))
            yield return kv;
    }
}

file class OmGroup<TKey, TValue> : IOmGroup<TKey, TValue>
{
    private readonly IEnumerable<TValue> _values;

    public OmGroup(TKey key, IEnumerable<TValue> values)
    {
        _values = values;
        Key = key;
    }

    public TKey Key { get; }

    public IEnumerator<TValue> GetEnumerator()
    {
        return _values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}