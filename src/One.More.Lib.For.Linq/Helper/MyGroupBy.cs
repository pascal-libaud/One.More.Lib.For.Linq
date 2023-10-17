using System.Collections;

namespace One.More.Lib.For.Linq.Helper;

public interface IMyGroup<out TKey, out TValue> : IEnumerable<TValue>
{
    TKey Key { get; }
}

public static partial class LinqHelper
{
    public static IEnumerable<IMyGroup<TKey, TValue>> MyGroupBy<TValue, TKey>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector)
        where TKey : notnull
    {
        return MyGroup(source, keySelector, null);
    }

    public static IEnumerable<IMyGroup<TKey, TValue>> MyGroup<TValue, TKey>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IEqualityComparer<TKey>? equalityComparer)
        where TKey : notnull
    {
        return MyGroupBy(source, keySelector, x => x, equalityComparer);
    }

    public static IEnumerable<IMyGroup<TKey, TResult>> MyGroupBy<TValue, TKey, TResult>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TResult> select)
        where TKey : notnull
    {
        return MyGroupBy(source, keySelector, select, null);
    }

    public static IEnumerable<IMyGroup<TKey, TResult>> MyGroupBy<TValue, TKey, TResult>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, Func<TValue, TResult> select, IEqualityComparer<TKey>? equalityComparer)
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

        return dictionary.MySelect(k => new MyGroup<TKey, TResult>(k.Key, k.Value.MySelect(v => select(v)).MyToList()));
    }
}

file class MyGroup<TKey, TValue> : IMyGroup<TKey, TValue>
{
    private readonly List<TValue> _values;

    public MyGroup(TKey key, List<TValue> values)
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