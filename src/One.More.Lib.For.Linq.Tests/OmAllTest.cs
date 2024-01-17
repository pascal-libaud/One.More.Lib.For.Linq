using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmAllTest
{
    [Fact]
    internal void OmAll_should_return_true_when_empty()
    {
        Assert.True(LinqHelper.Empty<int>().OmAll(x => x == 10));
    }

    [Fact]
    internal void OmAll_should_not_enumerate_all_when_one_false()
    {
        var spy = new EnumerableSpy();

        Assert.False(spy.GetValues().OmAll(x => x < 8));
        Assert.False(spy.IsEndReached);
    }
}