using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class MyAllAsyncTest
{
    [Fact]
    public async Task MyAllAsync_should_return_true_when_empty()
    {
        Assert.True(await LinqHelper.EmptyAsync<int>().MyAllAsync(x => x == 10));
        Assert.True(await LinqHelper.EmptyAsync<int>().MyAllAsync(x => (x == 10).ToTask()));
    }

    [Fact]
    public async Task MyAllAsync_should_not_enumerate_all_when_one_false()
    {
        var spy = new EnumerableSpy();

        Assert.False(await spy.GetValuesAsync().MyAllAsync(x => x < 8));
        Assert.False(spy.IsEndReached);
    }
}