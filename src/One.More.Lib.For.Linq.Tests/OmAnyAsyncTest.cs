namespace One.More.Lib.For.Linq.Tests;

public class OmAnyAsyncTest : TestBase
{
    [Fact]
    public async Task OmAnyAsync_should_return_false_when_no_item_matches()
    {
        Assert.False(await LinqAsyncHelper.RangeAsync(0, 10).OmAnyAsync(x => x > 10));
    }

    [Fact]
    public async Task OmAnyAsync_should_return_true_when_at_least_one_item_matches()
    {
        Assert.True(await LinqAsyncHelper.RangeAsync(0, 10).OmAnyAsync(x => x == 2));
    }

    [Fact]
    public async Task OmAnyAsync_should_return_false_when_empty()
    {
        Assert.False(await LinqAsyncHelper.EmptyAsync<int>().OmAnyAsync(x => x == 10));
        Assert.False(await LinqAsyncHelper.EmptyAsync<int>().OmAnyAsync(x => (x == 10).ToTask()));
    }

    [Fact]
    public async Task OmAnyAsync_should_not_enumerate_all_when_one_true()
    {
        var OmAny = async (IAsyncEnumerable<int> x) => await x.OmAnyAsync(z => z == 8);
        await OmAny.Should_not_enumerate_all_when();
    }

    [Fact]
    public async Task OmAnyAsync_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await LinqAsyncHelper.RangeAsync(10).OmAnyAsync(x => x != 4, token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task OmAnyAsync_overload_should_pass_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await LinqAsyncHelper.RangeAsync(10).OmAnyAsync(x => (x != 4).ToTask(), token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task OmAnyAsync_overload_should_cancel_with_its_cancellation_token()
    {
        var token = new CancellationTokenSource();
        await token.CancelAsync();

        var func = async () => await LinqHelper.Range(10).OmAnyAsync(x => (x != 4).ToTask(), token.Token);
        await func.Should().ThrowAsync<OperationCanceledException>();
    }
}