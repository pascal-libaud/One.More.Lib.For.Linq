namespace One.More.Lib.For.Linq.Tests;

public class OmAllAsyncTest : TestBase
{
    [Fact]
    public async Task OmAllAsync_should_return_true_when_all_item_match()
    {
        Assert.True(await LinqAsyncHelper.RangeAsync(0, 10).OmAllAsync(x => x < 10));
    }

    [Fact]
    public async Task OmAllAsync_should_return_false_when_at_least_one_item_does_not_match()
    {
        Assert.False(await LinqAsyncHelper.RangeAsync(0, 10).OmAllAsync(x => x != 2));
    }

    [Fact]
    public async Task OmAllAsync_should_return_true_when_empty()
    {
        Assert.True(await LinqAsyncHelper.EmptyAsync<int>().OmAllAsync(x => x == 10));
        Assert.True(await LinqAsyncHelper.EmptyAsync<int>().OmAllAsync(x => (x == 10).ToTask()));
    }

    [Fact]
    public async Task OmAllAsync_should_not_enumerate_all_when_one_false()
    {
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