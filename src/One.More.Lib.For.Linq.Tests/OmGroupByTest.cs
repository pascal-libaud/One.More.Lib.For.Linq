using System.Linq;
using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmGroupByTest : TestBase
{
    [Fact]
    public void OmGroupBy_should_not_enumerate_early()
    {
        var spy = new SpyEnumerable();

        var source = spy.GetValues().GroupBy(x => x.ToString());
        Assert.False(spy.IsEnumerated);

        _ = source.OmTake(5).OmToList();
        Assert.True(spy.IsEnumerated);
    }

    [Fact]
    public void OmGroupBy_enumerate_all_when_first_demanded()
    {
        var spy = new SpyEnumerable();

        var source = spy.GetValues().OmGroupBy(x => x.ToString());
        _ = source.First();

        Assert.True(spy.IsEndReached);
    }
}