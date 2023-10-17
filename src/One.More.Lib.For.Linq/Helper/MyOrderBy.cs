using System.Collections;

namespace One.More.Lib.For.Linq.Helper;

public interface IMyOrderedEnumerable<out T> : IEnumerable<T> { }

public static partial class LinqHelper
{
    public static IMyOrderedEnumerable<T> MyOrderBy<T, U>(this IEnumerable<T> source, Func<T, U> keySelector)
    {
        return new MyOrderedEnumerable<T, U>(source, keySelector, null);
    }

    public static IMyOrderedEnumerable<T> MyThenBy<T, U>(this IMyOrderedEnumerable<T> source, Func<T, U> keySelector)
    {
        return new MyOrderedEnumerable<T, U>(source, keySelector, source as MyOrderedEnumerable<T>);
    }
}

file abstract class MyOrderedEnumerable<T> : IMyOrderedEnumerable<T>
{
    public abstract int Compare(T x, T y);

    public abstract IEnumerator<T> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

file class MyOrderedEnumerable<T, U> : MyOrderedEnumerable<T>
{
    private readonly Func<T, U> _keySelector;
    private readonly MyOrderedEnumerable<T>? _parent;
    private readonly List<T> orderedList;

    public MyOrderedEnumerable(IEnumerable<T> source, Func<T, U> keySelector, MyOrderedEnumerable<T>? parent)
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