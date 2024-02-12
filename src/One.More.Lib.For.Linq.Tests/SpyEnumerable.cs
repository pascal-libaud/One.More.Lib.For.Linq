using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class SpyEnumerable
{
    public bool IsEnumerated => CountEnumeration != 0;
    public bool IsEndReached { get; private set; } = false;
    public int CountEnumeration { get; private set; } = 0;
    public int CountItemEnumerated { get; private set; } = 0;

    public IEnumerable<int> GetValues()
    {
        CountEnumeration++;
        foreach (var item in LinqHelper.Range(10))
        {
            CountItemEnumerated++;
            yield return item;
        }

        IsEndReached = true;
    }

    public async IAsyncEnumerable<int> GetValuesAsync()
    {
        CountEnumeration++;
        await foreach (var item in LinqHelper.RangeAsync(10))
        {
            CountItemEnumerated++;
            yield return item;
        }

        IsEndReached = true;
    }
}

public class SpyEnumerableTest : TestBase
{
    [Fact]
    public void SpyEnumerable_should_work_as_expected()
    {
        var spy = new SpyEnumerable();

        Assert.False(spy.IsEnumerated);
        Assert.False(spy.IsEndReached);
        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);

        foreach (var i in spy.GetValues())
            if (i == 5)
                break;

        Assert.True(spy.IsEnumerated);
        Assert.False(spy.IsEndReached);
        Assert.Equal(1, spy.CountEnumeration);
        Assert.Equal(6, spy.CountItemEnumerated);

        foreach (var _ in spy.GetValues())
        { }

        Assert.True(spy.IsEnumerated);
        Assert.True(spy.IsEndReached);
        Assert.Equal(2, spy.CountEnumeration);
        Assert.Equal(16, spy.CountItemEnumerated);
    }
}