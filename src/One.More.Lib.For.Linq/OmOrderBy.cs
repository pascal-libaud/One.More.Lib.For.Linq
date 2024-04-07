using System.Collections;

namespace One.More.Lib.For.Linq;

public interface IOmOrderedEnumerable<out T> : IEnumerable<T> { }

public static partial class LinqHelper
{
    public static IOmOrderedEnumerable<TSource> OmOrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        if (source is ICollection<TSource> collection)
            return new OmOrderedEnumerableWithCount<TSource, TKey>(source, keySelector, null, () => collection.Count);

        if(source is IWithCount withCount)
            return new OmOrderedEnumerableWithCount<TSource, TKey>(source, keySelector, null, () => withCount.Count);

        return new OmOrderedEnumerable<TSource, TKey>(source, keySelector, null);
    }

    public static IOmOrderedEnumerable<TSource> OmThenBy<TSource, TKey>(this IOmOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        if (source is IWithCount withCount)
            return new OmOrderedEnumerableWithCount<TSource, TKey>(source, keySelector, source as OmOrderedEnumerable<TSource>, () => withCount.Count);

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

file class OmOrderedEnumerable<TSource, TKey> : OmOrderedEnumerable<TSource>
{
    private readonly IEnumerable<TSource> _source;
    private readonly Func<TSource, TKey> _keySelector;
    private readonly OmOrderedEnumerable<TSource>? _parent;
    private List<TSource>? orderedList;
    
    public OmOrderedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, OmOrderedEnumerable<TSource>? parent)
    {
        _source = source;
        _keySelector = keySelector;
        _parent = parent;
    }

    internal override int Compare(TSource x, TSource y)
    {
        int result = 0;
        if (_parent != null)
            result = _parent.Compare(x, y);

        if (result == 0)
            result = Comparer<TKey>.Default.Compare(_keySelector(x), _keySelector(y));

        return result;
    }

    public override IEnumerator<TSource> GetEnumerator()
    {
        if (orderedList == null)
        {
            orderedList = new List<TSource>(_source);
            orderedList.Sort(Compare);
        }
        return orderedList.GetEnumerator();
    }
}

file class OmOrderedEnumerableWithCount<TSource, TKey> : OmOrderedEnumerable<TSource, TKey>, IWithCount
{
    private readonly Func<int> _count;

    public OmOrderedEnumerableWithCount(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, OmOrderedEnumerable<TSource>? parent, Func<int> count)
        : base(source, keySelector, parent)
    {
        _count = count;
    }

    public int Count => _count();
}