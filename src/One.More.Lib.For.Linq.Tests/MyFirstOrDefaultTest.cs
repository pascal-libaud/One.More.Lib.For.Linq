using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class MyFirstOrDefaultTest
{
    [Fact]
    public void MyFirstOrDefault_should_not_enumerate_all_when_item_found()
    {
       var spy = new EnumerableSpy();
        _ = spy.GetValues().MyFirstOrDefault(x => x == 5);
        Assert.Equal(6, spy.CountItemEnumerated);
    }

    [Fact]
    public void MyFirstOrDefault_should_return_default_when_no_item_found()
    {
        var result = LinqHelper.RangeNullable(0, 10).MyFirstOrDefault(x => x == 20);
        Assert.Null(result);
    }
}