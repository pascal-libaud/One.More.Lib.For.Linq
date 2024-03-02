using System.Collections;

namespace One.More.Lib.For.Linq;

public interface IOmOrderedEnumerable<out T> : IEnumerable<T> { }

public static partial class LinqHelper
{
    public static IOmOrderedEnumerable<T> OmOrderBy<T, U>(this IEnumerable<T> source, Func<T, U> keySelector)
    {
        return new OmOrderedEnumerable<T, U>(source, keySelector, null);
    }

    public static IOmOrderedEnumerable<T> OmThenBy<T, U>(this IOmOrderedEnumerable<T> source, Func<T, U> keySelector)
    {
        return new OmOrderedEnumerable<T, U>(source, keySelector, source as OmOrderedEnumerable<T>);
    }
}

file abstract class OmOrderedEnumerable<T> : IOmOrderedEnumerable<T>
{
    public abstract int Compare(T x, T y);

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

    public override int Compare(T x, T y)
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