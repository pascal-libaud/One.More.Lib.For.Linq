namespace One.More.Lib.For.Linq.Tests;

public class OmAllAsyncTest : TestBase
{
    [Fact]
    public async Task OmAllAsync_should_return_true_when_empty()
    {
        Assert.True(await LinqAsyncHelper.EmptyAsync<int>().OmAllAsync(x => x == 10));
        Assert.True(await LinqAsyncHelper.EmptyAsync<int>().OmAllAsync(x => (x == 10).ToTask()));
    }

    [Fact]
    public async Task OmAllAsync_should_not_enumerate_all_when_one_false()
    {
        var spy = SpyAsyncEnumerable.GetValuesAsync();

        Assert.False(await spy.OmAllAsync(x => x < 8));
        Assert.False(spy.IsEndReached);

        var omAll = async (IAsyncEnumerable<int> x) => await x.OmAllAsync(z => z < 8);
        await omAll.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task OmAllAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await LinqAsyncHelper.RangeAsync(10).OmAllAsync(x => x == 4, token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    } 
    
    [Fact]
    public async Task OmAllAsync_overload_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await LinqAsyncHelper.RangeAsync(10).OmAllAsync(x =>  (x == 4).ToTask(), token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task OmAllAsync_overload_should_cancel_with_its_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await LinqHelper.Range(10).OmAllAsync(x => (x == 4).ToTask(), token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}