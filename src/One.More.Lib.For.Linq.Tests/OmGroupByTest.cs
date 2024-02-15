using System.Linq;
using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmGroupByTest : TestBase
{
    [Fact]
    public void OmGroupBy_should_not_enumerate_early()
    {
        var omGroupBy = (IEnumerable<int> x) => x.OmGroupBy(y => y.ToString());
        omGroupBy.Should_not_enumerate_early();
    }

    [Fact]
    public void OmGroupBy_enumerate_all_when_first_demanded()
    {
        var spy = SpyEnumerable.GetValues();

        var source = spy.OmGroupBy(x => x.ToString());
        _ = source.First();

        Assert.True(spy.IsEndReached);
    }
}