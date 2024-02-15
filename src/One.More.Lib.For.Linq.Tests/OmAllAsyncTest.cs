using One.More.Lib.For.Linq.Helper;

namespace One.More.Lib.For.Linq.Tests;

public class OmAllAsyncTest : TestBase
{
    [Fact]
    public async Task OmAllAsync_should_return_true_when_empty()
    {
        Assert.True(await LinqHelper.EmptyAsync<int>().OmAllAsync(x => x == 10));
        Assert.True(await LinqHelper.EmptyAsync<int>().OmAllAsync(x => (x == 10).ToTask()));
    }

    [Fact]
    public async Task OmAllAsync_should_not_enumerate_all_when_one_false()
    {
        var spy = new SpyEnumerableLegacy();

        Assert.False(await spy.GetValuesAsync().OmAllAsync(x => x < 8));
        Assert.False(spy.IsEndReached);

        var omAll = async (IAsyncEnumerable<int> x) => await x.OmAllAsync(z => z < 8);
        await omAll.Should_not_enumerate_all_when();
    }
}