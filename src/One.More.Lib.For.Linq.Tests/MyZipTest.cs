using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class MyZipTest
{
    [Fact]
    public void MyZip_should_not_make_stack_overflow()
    {
        var spy = new EnumerableSpy();

        var _ = spy.GetValues().MyZip(LinqHelper.Range(10)).MyTake(3).MyToList();
        Assert.False(spy.IsEndReached);
    }

    [Fact]
    public void MyZip_should_end_when_shortest_source_is_finished()
    {
        Assert.Equal(3, LinqHelper.Range(3).MyZip(LinqHelper.Range(20)).MyCount());
        Assert.Equal(3, LinqHelper.Range(20).MyZip(LinqHelper.Range(3)).MyCount());
    }
}