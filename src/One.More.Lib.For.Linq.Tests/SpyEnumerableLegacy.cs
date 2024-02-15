using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class SpyEnumerableLegacy
{
    public bool IsEnumerated => CountEnumeration != 0;
    public bool IsEndReached { get; private set; } = false;
    public int CountEnumeration { get; private set; } = 0;
    public int CountItemEnumerated { get; private set; } = 0;

    // TODO Convertir cette méthode en créant une classe SpyEnumerableAsync
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

public class SpyEnumerableLegacyTest : TestBase
{
    [Fact]
    public async Task SpyEnumerable_should_work_as_expected()
    {
        var spy = new SpyEnumerableLegacy();

        Assert.False(spy.IsEnumerated);
        Assert.False(spy.IsEndReached);
        Assert.Equal(0, spy.CountEnumeration);
        Assert.Equal(0, spy.CountItemEnumerated);

        await foreach (var i in spy.GetValuesAsync())
            if (i == 5)
                break;

        Assert.True(spy.IsEnumerated);
        Assert.False(spy.IsEndReached);
        Assert.Equal(1, spy.CountEnumeration);
        Assert.Equal(6, spy.CountItemEnumerated);

        await foreach (var _ in spy.GetValuesAsync())
        { }

        Assert.True(spy.IsEnumerated);
        Assert.True(spy.IsEndReached);
        Assert.Equal(2, spy.CountEnumeration);
        Assert.Equal(16, spy.CountItemEnumerated);
    }
}