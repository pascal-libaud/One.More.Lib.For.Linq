using System.Collections;
using One.More.Lib.For.Linq.Helper;
using One.More.Lib.For.Linq.Tests;

public static class SpyEnumerable
{
    public static SpyEnumerable<int> GetValues()
    {
        return new SpyEnumerable<int>(LinqHelper.Range(10));
    }
}

public class SpyEnumerable<T> : IEnumerable<T>, ISpyEnumerable<T>
{
    private readonly IEnumerable<T> _enumerable;

    public SpyEnumerable(IEnumerable<T> enumerable)
    {
        _enumerable = enumerable;
    }

    public bool IsEnumerated => CountEnumeration != 0;
    public bool IsEndReached { get; set; } = false;
    public int CountEnumeration { get; set; } = 0;
    public int CountItemEnumerated { get; set; } = 0;

    public IEnumerator<T> GetEnumerator()
    {
        return new SpyEnumerator<T>(this, _enumerable.GetEnumerator());
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class SpyEnumerableTest : TestBase
{
    [Fact]
    public void SpyEnumerable_should_work_as_expected()
    {
        var spy = new SpyEnumerable<int>(LinqHelper.Range(10));

        Assert.False(spy.IsEnumerated);
        Assert.False(spy.IsEndReached);
        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);

        foreach (var i in spy)
            if (i == 5)
                break;

        Assert.True(spy.IsEnumerated);
        Assert.False(spy.IsEndReached);
        Assert.Equal(1, spy.CountEnumeration);
        Assert.Equal(6, spy.CountItemEnumerated);

        foreach (var _ in spy)
        { }

        Assert.True(spy.IsEnumerated);
        Assert.True(spy.IsEndReached);
        Assert.Equal(2, spy.CountEnumeration);
        Assert.Equal(16, spy.CountItemEnumerated);
    }
}