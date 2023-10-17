using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class EnumerableSpy
{
    public bool IsEnumerated { get; private set; } = false;
    public bool IsEndReached { get; private set; } = false;
    public int CountEnumeration { get; private set; } = 0;
    public int CountItemEnumerated { get; private set; } = 0;

    public IEnumerable<int> GetValues()
    {
        IsEnumerated = true;
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
        IsEnumerated = true;
        CountEnumeration++;
        await foreach (var item in LinqHelper.RangeAsync(10))
        {
            CountItemEnumerated++;
            yield return item;
        }

        IsEndReached = true;
    }
}

public class EnumerableSpyTest
{
    [Fact]
    public void EnumerableSpy_should_works_as_expected()
    {
        var spy = new EnumerableSpy();

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