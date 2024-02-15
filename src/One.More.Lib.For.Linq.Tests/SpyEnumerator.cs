using System.Collections;

namespace One.More.Lib.For.Linq.Tests;

public class SpyEnumerator<T> : IEnumerator<T>
{
    private bool _firstCallMade = false;
    private readonly ISpy _spied;
    private readonly IEnumerator<T> _enumerator;

    public SpyEnumerator(ISpy spied, IEnumerator<T> enumerator)
    {
        _spied = spied;
        _enumerator = enumerator;
    }

    public bool MoveNext()
    {
        if (!_firstCallMade)
        {
            _spied.CountEnumeration++;
            _firstCallMade = true;
        }

        var result = _enumerator.MoveNext();

        if (result)
            _spied.CountItemEnumerated++;
        else
            _spied.IsEndReached = true;

        return result;
    }

    public void Reset()
    {
        _firstCallMade = false;
        _enumerator.Reset();
    }

    public T Current => _enumerator.Current;

    object IEnumerator.Current => ((IEnumerator)_enumerator).Current;

    public void Dispose()
    {
        _enumerator.Dispose();
    }
}