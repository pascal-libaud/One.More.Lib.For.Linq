using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmZipTest : TestBase
{
    [Fact]
    public void OmZip_should_not_make_stack_overflow()
    {
        var spy = new SpyEnumerable();

        _ = spy.GetValues().OmZip(LinqHelper.Range(10)).OmTake(3).OmToList();
        Assert.False(spy.IsEndReached);
    }

    [Fact]
    public void OmZip_should_end_when_shortest_source_is_finished()
    {
        Assert.Equal(3, LinqHelper.Range(3).OmZip(LinqHelper.Range(20)).OmCount());
        Assert.Equal(3, LinqHelper.Range(20).OmZip(LinqHelper.Range(3)).OmCount());
    }
}