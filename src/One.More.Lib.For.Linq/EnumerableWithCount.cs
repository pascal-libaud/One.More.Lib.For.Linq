using System.Collections;

namespace One.More.Lib.For.Linq;

internal class EnumerableWithCount<T> : IEnumerable<T>, IWithCount
{
    private readonly IEnumerable<T> _innerEnumerable;
    private readonly Func<int> _count;

    public EnumerableWithCount(IEnumerable<T> innerEnumerable, Func<int> count)
    {
        _innerEnumerable = innerEnumerable;
        _count = count;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _innerEnumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_innerEnumerable).GetEnumerator();
    }

    public int Count => _count();
}