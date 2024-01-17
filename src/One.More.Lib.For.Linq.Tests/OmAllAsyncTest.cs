using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmAllAsyncTest
{
    [Fact]
    internal async Task OmAllAsync_should_return_true_when_empty()
    {
        Assert.True(await LinqHelper.EmptyAsync<int>().OmAllAsync(x => x == 10));
        Assert.True(await LinqHelper.EmptyAsync<int>().OmAllAsync(x => (x == 10).ToTask()));
    }

    [Fact]
    internal async Task OmAllAsync_should_not_enumerate_all_when_one_false()
    {
        var spy = new EnumerableSpy();

        Assert.False(await spy.GetValuesAsync().OmAllAsync(x => x < 8));
        Assert.False(spy.IsEndReached);
    }
}